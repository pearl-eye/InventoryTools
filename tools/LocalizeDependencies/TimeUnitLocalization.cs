using Mono.Cecil;
using Mono.Cecil.Cil;

namespace AllaganTools.CnBuild;

// This dependency exposes no display formatter. Keep the original enum and all persisted values intact.
internal static class TimeUnitLocalization
{
    internal const string AssemblyName = "AllaganLib.Interface";
    internal const string SupportedPackageVersion = "2.3.3";
    internal const string HelperName = "AllaganTools.CnBuild.TimeUnitDisplay";
    internal const string UnitName = "AllaganLib.Interface.Widgets.TimeUnit";
    internal const string DrawName = "System.Boolean AllaganLib.Interface.Widgets.TimeSpanPickerWidget::Draw(System.String,System.Nullable`1<AllaganLib.Interface.Widgets.TimeUnit>&,System.Nullable`1<System.Int32>&,System.Nullable`1<System.Int32>)";
    private const string EnumNames = "System.String[] System.Enum::GetNames(System.Type)";
    private const string ObjectToString = "System.String System.Object::ToString()";
    private static readonly string[] Names = ["Seconds", "Minutes", "Hours", "Days", "Months", "Years"];
    private static readonly string[] Labels = ["秒", "分钟", "小时", "天", "月", "年"];

    internal static int Apply(ModuleDefinition module, string packageVersion)
    {
        var state = Inspect(module, packageVersion);
        if (state.Localized)
            return 0;

        var helper = CreateHelper(module, state.OriginalGetNames);
        module.Types.Add(helper);
        var format = helper.Methods.Single(method => method.Name == "Format");
        var getNames = helper.Methods.Single(method => method.Name == "GetNames");
        var processor = state.Draw.Body.GetILProcessor();
        foreach (var call in state.ToStringCalls)
            processor.InsertAfter(call, processor.Create(OpCodes.Call, format));
        foreach (var call in state.GetNamesCalls)
            call.Operand = getNames;
        Verify(module, packageVersion);
        return 4;
    }

    internal static void Verify(ModuleDefinition module, string packageVersion)
    {
        if (!Inspect(module, packageVersion).Localized)
            throw Invalid("the display patch is missing");
    }

    private static PatchState Inspect(ModuleDefinition module, string packageVersion)
    {
        if (module.Assembly.Name.Name != AssemblyName || packageVersion != SupportedPackageVersion)
            throw Invalid("the dependency version is not the reviewed 2.3.3 release");
        var unit = module.Types.SingleOrDefault(type => type.FullName == UnitName);
        if (unit == null || !unit.IsEnum || unit.Fields.Count != Names.Length + 1
            || unit.Fields.SingleOrDefault(field => field.Name == "value__")?.FieldType.FullName != "System.Int32"
            || !unit.Fields.Where(field => field.IsLiteral).Select(field => (field.Name, Value: field.Constant))
                .OrderBy(field => field.Value).SequenceEqual(Names.Select((name, value) => (name, Value: (object)value))))
            throw Invalid("the six TimeUnit names or numeric values changed");
        var candidates = module.Types.SelectMany(type => type.Methods).Where(method => method.FullName == DrawName).ToArray();
        if (candidates.Length != 1 || !candidates[0].HasBody)
            throw Invalid("the complete Draw signature changed");
        var draw = candidates[0];
        var instructions = draw.Body.Instructions;
        var toStringCalls = instructions.Where(instruction => IsCall(instruction, ObjectToString)).ToArray();
        if (toStringCalls.Length != 2 || toStringCalls.Any(call => call.OpCode.Code != Code.Callvirt
            || call.Previous?.OpCode.Code != Code.Constrained || call.Previous.Operand is not TypeReference type || type.FullName != UnitName))
            throw Invalid("the two constrained TimeUnit.ToString call sites changed");
        var helperTypes = module.Types.Where(type => type.FullName == HelperName).ToArray();
        if (helperTypes.Length > 1)
            throw Invalid("duplicate display helper");
        var getNamesCalls = instructions.Where(instruction => IsCall(instruction, EnumNames)
            || IsCall(instruction, "System.String[] " + HelperName + "::GetNames(System.Type)")).ToArray();
        if (getNamesCalls.Length != 2 || getNamesCalls.Any(call => call.OpCode.Code != Code.Call
            || call.Previous?.Operand is not MethodReference fromHandle || fromHandle.FullName != "System.Type System.Type::GetTypeFromHandle(System.RuntimeTypeHandle)"
            || call.Previous.Previous?.OpCode.Code != Code.Ldtoken || call.Previous.Previous.Operand is not TypeReference type || type.FullName != UnitName))
            throw Invalid("the two TimeUnit Enum.GetNames call sites changed");
        var formatName = "System.String " + HelperName + "::Format(System.String)";
        var formatCalls = instructions.Where(instruction => IsCall(instruction, formatName)).ToArray();
        var original = helperTypes.Length == 0 && formatCalls.Length == 0 && getNamesCalls.All(call => IsCall(call, EnumNames));
        var localized = helperTypes.Length == 1 && formatCalls.Length == 2
            && toStringCalls.All(call => call.Next?.OpCode.Code == Code.Call && IsCall(call.Next, formatName))
            && getNamesCalls.All(call => IsCall(call, "System.String[] " + HelperName + "::GetNames(System.Type)"));
        if (!original && !localized)
            throw Invalid("partially localized or unexpected display call sites");
        MethodReference originalGetNames;
        if (localized)
        {
            var helper = helperTypes[0];
            var calls = helper.Methods.Where(method => method.HasBody).SelectMany(method => method.Body.Instructions)
                .Where(instruction => IsCall(instruction, EnumNames)).ToArray();
            if (calls.Length != 1)
                throw Invalid("the display helper's Enum.GetNames implementation changed");
            originalGetNames = (MethodReference)calls[0].Operand;
            VerifyHelper(helper, CreateHelper(module, originalGetNames));
        }
        else
        {
            originalGetNames = (MethodReference)getNamesCalls[0].Operand;
        }
        return new(draw, toStringCalls, getNamesCalls, originalGetNames, localized);
    }

    private static TypeDefinition CreateHelper(ModuleDefinition module, MethodReference originalGetNames)
    {
        var helper = new TypeDefinition("AllaganTools.CnBuild", "TimeUnitDisplay",
            TypeAttributes.NotPublic | TypeAttributes.Abstract | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, module.TypeSystem.Object);
        var format = new MethodDefinition("Format", MethodAttributes.Assembly | MethodAttributes.Static | MethodAttributes.HideBySig, module.TypeSystem.String);
        format.Parameters.Add(new ParameterDefinition("name", ParameterAttributes.None, module.TypeSystem.String));
        helper.Methods.Add(format);
        var equality = new MethodReference("op_Equality", module.TypeSystem.Boolean, module.TypeSystem.String);
        equality.Parameters.Add(new ParameterDefinition(module.TypeSystem.String));
        equality.Parameters.Add(new ParameterDefinition(module.TypeSystem.String));
        var il = format.Body.GetILProcessor();
        for (var index = 0; index < Names.Length; index++)
        {
            var next = il.Create(OpCodes.Nop);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldstr, Names[index]);
            il.Emit(OpCodes.Call, equality);
            il.Emit(OpCodes.Brfalse, next);
            il.Emit(OpCodes.Ldstr, Labels[index]);
            il.Emit(OpCodes.Ret);
            il.Append(next);
        }
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Ret);
        format.Body.MaxStackSize = 2;

        var getNames = new MethodDefinition("GetNames", MethodAttributes.Assembly | MethodAttributes.Static | MethodAttributes.HideBySig, new ArrayType(module.TypeSystem.String));
        getNames.Parameters.Add(new ParameterDefinition("enumType", ParameterAttributes.None, originalGetNames.Parameters[0].ParameterType));
        helper.Methods.Add(getNames);
        getNames.Body.InitLocals = true;
        var names = new VariableDefinition(new ArrayType(module.TypeSystem.String));
        var offset = new VariableDefinition(module.TypeSystem.Int32);
        getNames.Body.Variables.Add(names);
        getNames.Body.Variables.Add(offset);
        il = getNames.Body.GetILProcessor();
        var test = il.Create(OpCodes.Ldloc_1);
        var body = il.Create(OpCodes.Ldloc_0);
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Call, originalGetNames);
        il.Emit(OpCodes.Stloc_0);
        il.Emit(OpCodes.Ldc_I4_0);
        il.Emit(OpCodes.Stloc_1);
        il.Emit(OpCodes.Br, test);
        il.Append(body);
        il.Emit(OpCodes.Ldloc_1);
        il.Emit(OpCodes.Ldloc_0);
        il.Emit(OpCodes.Ldloc_1);
        il.Emit(OpCodes.Ldelem_Ref);
        il.Emit(OpCodes.Call, format);
        il.Emit(OpCodes.Stelem_Ref);
        il.Emit(OpCodes.Ldloc_1);
        il.Emit(OpCodes.Ldc_I4_1);
        il.Emit(OpCodes.Add);
        il.Emit(OpCodes.Stloc_1);
        il.Append(test);
        il.Emit(OpCodes.Ldloc_0);
        il.Emit(OpCodes.Ldlen);
        il.Emit(OpCodes.Conv_I4);
        il.Emit(OpCodes.Blt, body);
        il.Emit(OpCodes.Ldloc_0);
        il.Emit(OpCodes.Ret);
        getNames.Body.MaxStackSize = 4;
        return helper;
    }

    private static void VerifyHelper(TypeDefinition actual, TypeDefinition expected)
    {
        if (actual.Attributes != expected.Attributes || actual.BaseType.FullName != expected.BaseType.FullName
            || actual.Fields.Count != 0 || actual.Properties.Count != 0 || actual.NestedTypes.Count != 0 || actual.Methods.Count != 2)
            throw Invalid("the display helper's shape changed");
        foreach (var template in expected.Methods)
        {
            var matches = actual.Methods.Where(method => method.FullName == template.FullName).ToArray();
            if (matches.Length != 1 || matches[0].Attributes != template.Attributes || !matches[0].HasBody
                || matches[0].Body.InitLocals != template.Body.InitLocals
                || !matches[0].Body.Variables.Select(variable => variable.VariableType.FullName)
                    .SequenceEqual(template.Body.Variables.Select(variable => variable.VariableType.FullName))
                || !Fingerprint(matches[0]).SequenceEqual(Fingerprint(template)))
                throw Invalid("the display helper's implementation changed");
        }
    }

    private static IEnumerable<string> Fingerprint(MethodDefinition method)
    {
        var instructions = method.Body.Instructions;
        foreach (var instruction in instructions)
        {
            var operand = instruction.Operand switch
            {
                Instruction target => instructions.IndexOf(target).ToString(System.Globalization.CultureInfo.InvariantCulture),
                MemberReference member => member.FullName,
                null => "",
                _ => instruction.Operand.ToString()
            };
            yield return instruction.OpCode.Code + ":" + operand;
        }
    }

    private static bool IsCall(Instruction instruction, string fullName) => instruction.OpCode.Code is Code.Call or Code.Callvirt
        && instruction.Operand is MethodReference method && method.FullName == fullName;
    private static InvalidDataException Invalid(string reason) => new("TimeUnit display localization rejected: " + reason + ".");
    private sealed record PatchState(MethodDefinition Draw, Instruction[] ToStringCalls, Instruction[] GetNamesCalls, MethodReference OriginalGetNames, bool Localized);
}
