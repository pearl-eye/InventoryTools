using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using InventoryTools.Services;

var tests = new (string Name, Action Run)[]
{
    ("valid release notes load", ValidChangelog),
    ("four-part version does not block window dependencies", InvalidVersion),
    ("missing release notes are optional", MissingChangelog),
    ("unreadable release-note path is optional", DirectoryInsteadOfFile),
    ("packaged upstream release notes load", PackagedChangelog),
};
foreach (var test in tests)
{
    test.Run();
    Console.WriteLine($"PASS: {test.Name}");
}
Console.WriteLine($"Passed {tests.Length} changelog regression checks.");

static void Assert(bool condition, string message)
{
    if (!condition) throw new InvalidOperationException(message);
}

static string Release(string version) => $"# Changelog\n\n## [{version}] - 2026-11-16\n\n### Fixed\n- Regression fixture.\n";

static void ValidChangelog()
{
    using var fixture = new Fixture(Release("15.1.1"));
    var service = fixture.CreateService();
    Assert(service.ChangeLogs.Count == 1, "Valid changelog should load its release.");
    Assert(service.ChangeLogs[0].Item1.ToString() == "15.1.1", "Unexpected parsed version.");
    Assert(fixture.Log.Errors.Count == 0, "Valid changelog should not report an error.");
}

static void InvalidVersion()
{
    using var fixture = new Fixture(Release("2.15.0.13"));
    var service = fixture.CreateService();
    Assert(service.ChangeLogs.Count == 0, "Invalid version must leave release notes empty.");
    Assert(fixture.Log.Errors.Any(e => e.Exception is ArgumentException), "The parser exception must be logged.");
}

static void MissingChangelog()
{
    using var fixture = new Fixture();
    var service = fixture.CreateService();
    Assert(service.ChangeLogs.Count == 0, "Missing release notes must leave an empty list.");
    Assert(fixture.Log.Errors.Count > 0, "Missing release notes must be logged.");
}

static void DirectoryInsteadOfFile()
{
    using var fixture = new Fixture();
    Directory.CreateDirectory(Path.Combine(fixture.Root, "CHANGELOG.md"));
    var service = fixture.CreateService();
    Assert(service.ChangeLogs.Count == 0, "An unreadable path must leave an empty list.");
    Assert(fixture.Log.Errors.Count > 0, "The unreadable path must be logged.");
}

static void PackagedChangelog()
{
    var changelog = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "ReleaseCHANGELOG.md"));
    using var fixture = new Fixture(changelog);
    var service = fixture.CreateService();
    Assert(service.ChangeLogs.Count > 100, "The actual release history should load completely.");
    Assert(service.ChangeLogs.Any(log => log.Item1.ToString() == "15.1.1"), "The actual latest release must be present.");
    Assert(fixture.Log.Errors.Count == 0, "The actual packaged changelog must not log parse errors.");
}

sealed class Fixture : IDisposable, IDalamudPluginInterface
{
    public string Root { get; } = Path.Combine(Path.GetTempPath(), "allagan-changelog-tests-" + Guid.NewGuid().ToString("N"));
    public RecordingLog Log { get; } = new();
    public FileInfo AssemblyLocation => new(Path.Combine(Root, "InventoryTools.dll"));

    public Fixture(string? changelog = null)
    {
        Directory.CreateDirectory(Root);
        if (changelog is not null) File.WriteAllText(Path.Combine(Root, "CHANGELOG.md"), changelog);
    }

    public ChangelogService CreateService() => new(this, Log);
    public void Dispose() => Directory.Delete(Root, true);
}

sealed class RecordingLog : IPluginLog
{
    public List<(Exception? Exception, string Message)> Errors { get; } = new();
    public void Error(string message) => Errors.Add((null, message));
    public void Error(Exception exception, string message) => Errors.Add((exception, message));
}
