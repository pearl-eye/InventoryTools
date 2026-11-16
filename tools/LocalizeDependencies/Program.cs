using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace AllaganTools.CnBuild;

internal static class Program
{
    public static int Main(string[] args)
    {
        try
        {
            if (args.Length is < 1 or > 2)
            {
                Console.Error.WriteLine("Usage: LocalizeDependencies <plugin-output-directory> [catalog.json]");
                return 2;
            }

            var outputDirectory = Path.GetFullPath(args[0]);
            if (!Directory.Exists(outputDirectory))
                throw new DirectoryNotFoundException($"Plugin output directory does not exist: {outputDirectory}");

            var catalogPath = args.Length == 2 ? Path.GetFullPath(args[1]) : FindCatalog();
            var catalog = JsonSerializer.Deserialize<Catalog>(File.ReadAllText(catalogPath), JsonOptions)
                ?? throw new InvalidDataException("The dependency translation catalog is empty.");
            ValidateCatalog(catalog);
            ApplyCatalog(catalog, outputDirectory);
            return 0;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine($"Dependency localization failed: {exception.Message}");
            return 1;
        }
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
    };

    private static string FindCatalog()
    {
        foreach (var start in new[] { Directory.GetCurrentDirectory(), AppContext.BaseDirectory })
        {
            for (var directory = new DirectoryInfo(start); directory != null; directory = directory.Parent)
            {
                var candidate = Path.Combine(directory.FullName, "localization", "dependencies.json");
                if (File.Exists(candidate))
                    return candidate;
            }
        }

        throw new FileNotFoundException("Could not find localization/dependencies.json. Pass its path as the second argument.");
    }

    private static void ValidateCatalog(Catalog catalog)
    {
        if (catalog.SchemaVersion != 1 || catalog.Assemblies.Count == 0)
            throw new InvalidDataException("Unsupported or empty dependency translation catalog.");

        var assemblies = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var assembly in catalog.Assemblies)
        {
            if (string.IsNullOrWhiteSpace(assembly.Name)
                || assembly.File != assembly.Name + ".dll"
                || Path.GetFileName(assembly.File) != assembly.File
                || !assemblies.Add(assembly.File)
                || assembly.Rules.Count == 0)
                throw new InvalidDataException($"Invalid or duplicate dependency: {assembly.File}");

            var keys = new HashSet<(string Method, string Original)>();
            foreach (var rule in assembly.Rules)
            {
                if (string.IsNullOrWhiteSpace(rule.FullMethodName)
                    || string.IsNullOrEmpty(rule.Original)
                    || string.IsNullOrEmpty(rule.Translation)
                    || rule.Original == rule.Translation
                    || rule.Count <= 0
                    || !keys.Add((rule.FullMethodName, rule.Original)))
                    throw new InvalidDataException($"Invalid or duplicate translation rule in {assembly.File}.");

                var hiddenId = Regex.Match(rule.Original, "#{2,}.*$", RegexOptions.Singleline).Value;
                var translatedHiddenId = Regex.Match(rule.Translation, "#{2,}.*$", RegexOptions.Singleline).Value;
                if (hiddenId != translatedHiddenId)
                    throw new InvalidDataException($"A hidden UI identifier changed in {rule.FullMethodName}.");

                var placeholders = Regex.Matches(rule.Original, @"\{[^{}]*\}").Select(match => match.Value);
                var translatedPlaceholders = Regex.Matches(rule.Translation, @"\{[^{}]*\}").Select(match => match.Value);
                if (!placeholders.SequenceEqual(translatedPlaceholders))
                    throw new InvalidDataException($"A format placeholder changed in {rule.FullMethodName}.");
            }
        }
    }

    private static void ApplyCatalog(Catalog catalog, string outputDirectory)
    {
        using var resolver = new DefaultAssemblyResolver();
        resolver.AddSearchDirectory(outputDirectory);
        resolver.AddSearchDirectory(Path.GetDirectoryName(typeof(object).Assembly.Location)!);
        resolver.AddSearchDirectory(AppContext.BaseDirectory);
        var readerParameters = new ReaderParameters { InMemory = true, AssemblyResolver = resolver };
        var modules = new List<(AssemblyRules Rules, ModuleDefinition Module, int Changes, int Total)>();
        string? stagingDirectory = null;

        try
        {
            // Validate every dependency and every rule before writing any file.
            foreach (var assembly in catalog.Assemblies)
            {
                var path = Path.Combine(outputDirectory, assembly.File);
                var module = ModuleDefinition.ReadModule(path, readerParameters);
                modules.Add((assembly, module, 0, 0));
                if (module.Assembly.Name.Name != assembly.Name || module.Name != assembly.File)
                    throw new InvalidDataException($"Unexpected assembly identity for {assembly.File}.");

                var methods = module.Types.SelectMany(AllTypes).SelectMany(type => type.Methods)
                    .GroupBy(method => method.FullName, StringComparer.Ordinal)
                    .ToDictionary(group => group.Key, group => group.ToArray(), StringComparer.Ordinal);
                var changes = 0;
                var total = 0;
                foreach (var methodRules in assembly.Rules.GroupBy(rule => rule.FullMethodName, StringComparer.Ordinal))
                {
                    if (!methods.TryGetValue(methodRules.Key, out var candidates) || candidates.Length != 1 || !candidates[0].HasBody)
                        throw new InvalidDataException($"Expected exactly one method: {methodRules.Key}");

                    var instructions = candidates[0].Body.Instructions.Where(instruction => instruction.OpCode.Code == Code.Ldstr).ToArray();
                    var counts = instructions.GroupBy(instruction => (string)instruction.Operand, StringComparer.Ordinal)
                        .ToDictionary(group => group.Key, group => group.Count(), StringComparer.Ordinal);
                    var originals = methodRules.ToDictionary(rule => rule.Original, rule => rule.Count, StringComparer.Ordinal);
                    // Different English labels can intentionally share a Chinese translation.
                    // Validate the complete method before changing any of its strings.
                    var translations = methodRules.GroupBy(rule => rule.Translation, StringComparer.Ordinal)
                        .ToDictionary(group => group.Key, group => group.Sum(rule => rule.Count), StringComparer.Ordinal);
                    var isOriginal = originals.All(item => counts.GetValueOrDefault(item.Key) == item.Value)
                        && translations.All(item => counts.GetValueOrDefault(item.Key) == 0);
                    var isLocalized = originals.All(item => counts.GetValueOrDefault(item.Key) == 0)
                        && translations.All(item => counts.GetValueOrDefault(item.Key) == item.Value);
                    if (!isOriginal && !isLocalized)
                        throw new InvalidDataException($"{assembly.File}: text counts changed in {methodRules.Key}. Review the dependency version and catalog; partially localized methods are not accepted.");

                    var expected = originals.Values.Sum();
                    if (isOriginal)
                    {
                        var replacements = methodRules.ToDictionary(rule => rule.Original, rule => rule.Translation, StringComparer.Ordinal);
                        foreach (var instruction in instructions)
                        {
                            if (replacements.TryGetValue((string)instruction.Operand, out var translation))
                                instruction.Operand = translation;
                        }

                        changes += expected;
                    }

                    total += expected;
                }

                if (assembly.Name == TimeUnitLocalization.AssemblyName)
                {
                    changes += TimeUnitLocalization.Apply(module, assembly.PackageVersion);
                    total += 4;
                }

                modules[^1] = (assembly, module, changes, total);
            }

            stagingDirectory = Path.Combine(outputDirectory, ".allagan-cn-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(stagingDirectory);
            foreach (var item in modules.Where(item => item.Changes > 0))
            {
                var stagedFile = Path.Combine(stagingDirectory, item.Rules.File);
                item.Module.Write(stagedFile);
                using var written = ModuleDefinition.ReadModule(stagedFile, readerParameters);
                VerifyWritten(item.Rules, written);
            }

            foreach (var item in modules)
            {
                if (item.Changes > 0)
                    File.Move(Path.Combine(stagingDirectory, item.Rules.File), Path.Combine(outputDirectory, item.Rules.File), overwrite: true);
                Console.WriteLine($"{item.Rules.File}: {item.Total} verified; {item.Changes} localized; {item.Total - item.Changes} already localized.");
            }
        }
        finally
        {
            foreach (var item in modules)
                item.Module.Dispose();
            if (stagingDirectory != null && Directory.Exists(stagingDirectory))
                Directory.Delete(stagingDirectory, recursive: true);
        }
    }

    private static void VerifyWritten(AssemblyRules assembly, ModuleDefinition module)
    {
        if (assembly.Name == TimeUnitLocalization.AssemblyName)
            TimeUnitLocalization.Verify(module, assembly.PackageVersion);
        var methods = module.Types.SelectMany(AllTypes).SelectMany(type => type.Methods)
            .ToDictionary(method => method.FullName, StringComparer.Ordinal);
        foreach (var methodRules in assembly.Rules.GroupBy(rule => rule.FullMethodName, StringComparer.Ordinal))
        {
            var method = methods[methodRules.Key];
            var counts = method.Body.Instructions.Where(instruction => instruction.OpCode.Code == Code.Ldstr)
                .GroupBy(instruction => (string)instruction.Operand, StringComparer.Ordinal)
                .ToDictionary(group => group.Key, group => group.Count(), StringComparer.Ordinal);
            var expectedTranslations = methodRules.GroupBy(rule => rule.Translation, StringComparer.Ordinal)
                .ToDictionary(group => group.Key, group => group.Sum(rule => rule.Count), StringComparer.Ordinal);
            if (methodRules.Any(rule => counts.GetValueOrDefault(rule.Original) != 0)
                || expectedTranslations.Any(item => counts.GetValueOrDefault(item.Key) != item.Value))
                throw new InvalidDataException($"Written translation verification failed in {methodRules.Key}.");
        }
    }

    private static IEnumerable<TypeDefinition> AllTypes(TypeDefinition type)
    {
        yield return type;
        foreach (var nested in type.NestedTypes.SelectMany(AllTypes))
            yield return nested;
    }

    private sealed class Catalog
    {
        public int SchemaVersion { get; set; }
        public List<AssemblyRules> Assemblies { get; set; } = [];
    }

    private sealed class AssemblyRules
    {
        public string Name { get; set; } = "";
        public string File { get; set; } = "";
        public string PackageVersion { get; set; } = "";
        public List<TranslationRule> Rules { get; set; } = [];
    }

    private sealed class TranslationRule
    {
        public string FullMethodName { get; set; } = "";
        public string Original { get; set; } = "";
        public string Translation { get; set; } = "";
        public int Count { get; set; }
    }
}
