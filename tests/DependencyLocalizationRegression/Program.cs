using System.Reflection;
using System.Runtime.Loader;
using System.Security.Cryptography;
using AllaganTools.CnBuild;
using Mono.Cecil;
using Mono.Cecil.Cil;
using FieldAttributes = Mono.Cecil.FieldAttributes;
using MethodAttributes = Mono.Cecil.MethodAttributes;
using ParameterAttributes = Mono.Cecil.ParameterAttributes;
using TypeAttributes = Mono.Cecil.TypeAttributes;

var checks = 0;
var names = new[] { "Seconds", "Minutes", "Hours", "Days", "Months", "Years" };
var labels = new[] { "秒", "分钟", "小时", "天", "月", "年" };
void Check(bool condition, string message)
{
    if (!condition) throw new InvalidOperationException(message);
    checks++;
}
byte[] Bytes(ModuleDefinition module)
{
    using var stream = new MemoryStream();
    module.Write(stream);
    return stream.ToArray();
}
void Reject(Action<ModuleDefinition> drift, string reason, bool patched = false, string version = "2.3.3")
{
    using var fixture = Fixture();
    if (patched) TimeUnitLocalization.Apply(fixture, "2.3.3");
    drift(fixture);
    var before = SHA256.HashData(Bytes(fixture));
    try { TimeUnitLocalization.Apply(fixture, version); }
    catch (InvalidDataException)
    {
        Check(before.SequenceEqual(SHA256.HashData(Bytes(fixture))), "Rejected " + reason + " must not modify the module.");
        return;
    }
    throw new InvalidOperationException("Expected rejection: " + reason);
}

using (var fixture = Fixture())
{
    Check(TimeUnitLocalization.Apply(fixture, "2.3.3") == 4, "Four reviewed display calls must be patched.");
    TimeUnitLocalization.Verify(fixture, "2.3.3");
    var first = Bytes(fixture);
    using var reread = ModuleDefinition.ReadModule(new MemoryStream(first));
    Check(TimeUnitLocalization.Apply(reread, "2.3.3") == 0, "Second application must be a no-op.");
    Check(SHA256.HashData(first).SequenceEqual(SHA256.HashData(Bytes(reread))), "Second application must preserve the DLL hash.");
    var context = new AssemblyLoadContext("time-unit-localization-regression", isCollectible: true);
    try
    {
        var assembly = context.LoadFromStream(new MemoryStream(first));
        var unit = assembly.GetType(TimeUnitLocalization.UnitName, throwOnError: true)!;
        Check(Enum.GetNames(unit).SequenceEqual(names), "The original enum member names must remain unchanged.");
        Check(Enum.GetValues(unit).Cast<object>().Select(Convert.ToInt32).SequenceEqual(Enumerable.Range(0, 6)), "The original enum values must remain unchanged.");
        var widget = assembly.GetType("AllaganLib.Interface.Widgets.TimeSpanPickerWidget", throwOnError: true)!;
        var instance = Activator.CreateInstance(widget);
        var draw = widget.GetMethod("Draw")!;
        for (var value = 0; value < names.Length; value++)
        {
            widget.GetField("Selected")!.SetValue(null, Enum.ToObject(unit, value));
            draw.Invoke(instance, new object?[] { "fixture", null, null, null });
            Check((string)widget.GetField("Preview")!.GetValue(null)! == labels[value], "Time-unit summary must use the Chinese label.");
            Check((string)widget.GetField("UnitLabel")!.GetValue(null)! == labels[value], "Time-unit heading must use the Chinese label.");
            Check(((string[])widget.GetField("Choices")!.GetValue(null)!).SequenceEqual(labels), "Combo choices must use Chinese in original enum order.");
            Check(Convert.ToInt32(widget.GetField("Selected")!.GetValue(null)) == value, "The selected time-unit value must remain unchanged.");
            Check((int)widget.GetField("ChoiceCount")!.GetValue(null)! == 6, "The dropdown item count must remain unchanged.");
        }
        var helper = assembly.GetType(TimeUnitLocalization.HelperName, throwOnError: true)!;
        var format = helper.GetMethod("Format", BindingFlags.Static | BindingFlags.NonPublic)!;
        Check((string?)format.Invoke(null, ["Unknown"]) == "Unknown", "Unknown display values must pass through unchanged.");
        Check(format.Invoke(null, [null]) == null, "Null display values must pass through unchanged.");
    }
    finally { context.Unload(); }
}

Reject(module => module.Types.Single(type => type.FullName == TimeUnitLocalization.UnitName).Fields.Single(field => field.Name == "Days").Constant = 9, "enum value drift");
Reject(module => module.Types.Single(type => type.FullName == TimeUnitLocalization.UnitName).Fields.Single(field => field.Name == "Days").Name = "Day", "enum name drift");
Reject(module => Draw(module).Parameters.RemoveAt(0), "Draw signature drift");
Reject(module => Draw(module).Body.Instructions.Single(instruction => instruction.OpCode.Code == Code.Constrained && instruction.Next!.Next!.Operand is MethodReference method && method.Name == "ToLower").OpCode = OpCodes.Nop, "constrained receiver drift");
Reject(module => Draw(module).Body.Instructions.Last(instruction => instruction.Operand is MethodReference method && method.Name == "ToString").OpCode = OpCodes.Nop, "ToString call count drift");
Reject(module => Draw(module).Body.Instructions.Last(instruction => instruction.Operand is MethodReference method && method.Name == "GetNames").OpCode = OpCodes.Nop, "GetNames call count drift");
Reject(module => Draw(module).Body.Instructions.First(instruction => instruction.OpCode.Code == Code.Ldtoken).Operand = module.TypeSystem.Int32, "GetNames enum type drift");
Reject(_ => { }, "unreviewed dependency version", version: "2.3.4");
Reject(module => Draw(module).Body.Instructions.First(instruction => instruction.Operand is MethodReference method && method.Name == "Format").OpCode = OpCodes.Nop, "partial localization", patched: true);
Reject(module => module.Types.Single(type => type.FullName == TimeUnitLocalization.HelperName).Methods.Single(method => method.Name == "Format").Body.Instructions.First(instruction => instruction.OpCode.Code == Code.Ldstr && (string)instruction.Operand == "天").Operand = "English", "helper implementation drift", patched: true);
Console.WriteLine($"Passed {checks} dependency-localization checks: display output, enum preservation, idempotence and drift rejection.");

MethodDefinition Draw(ModuleDefinition module) => module.Types.SelectMany(type => type.Methods).Single(method => method.FullName == TimeUnitLocalization.DrawName);
ModuleDefinition Fixture()
{
    var module = ModuleDefinition.CreateModule("AllaganLib.Interface.dll", ModuleKind.Dll);
    var unit = new TypeDefinition("AllaganLib.Interface.Widgets", "TimeUnit", TypeAttributes.Public | TypeAttributes.Sealed, module.ImportReference(typeof(Enum)));
    module.Types.Add(unit);
    unit.Fields.Add(new FieldDefinition("value__", FieldAttributes.Public | FieldAttributes.SpecialName | FieldAttributes.RTSpecialName, module.TypeSystem.Int32));
    for (var index = 0; index < names.Length; index++)
        unit.Fields.Add(new FieldDefinition(names[index], FieldAttributes.Public | FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.HasDefault, unit) { Constant = index });
    var widget = new TypeDefinition("AllaganLib.Interface.Widgets", "TimeSpanPickerWidget", TypeAttributes.Public | TypeAttributes.Class, module.TypeSystem.Object);
    module.Types.Add(widget);
    FieldDefinition Field(string name, TypeReference type)
    {
        var field = new FieldDefinition(name, FieldAttributes.Public | FieldAttributes.Static, type);
        widget.Fields.Add(field);
        return field;
    }
    var selected = Field("Selected", unit);
    var preview = Field("Preview", module.TypeSystem.String);
    var label = Field("UnitLabel", module.TypeSystem.String);
    var choices = Field("Choices", new ArrayType(module.TypeSystem.String));
    var count = Field("ChoiceCount", module.TypeSystem.Int32);
    var constructor = new MethodDefinition(".ctor", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName | MethodAttributes.HideBySig, module.TypeSystem.Void);
    widget.Methods.Add(constructor);
    var il = constructor.Body.GetILProcessor();
    il.Emit(OpCodes.Ldarg_0);
    il.Emit(OpCodes.Call, module.ImportReference(typeof(object).GetConstructor(Type.EmptyTypes)!));
    il.Emit(OpCodes.Ret);
    var draw = new MethodDefinition("Draw", MethodAttributes.Public | MethodAttributes.HideBySig, module.TypeSystem.Boolean);
    widget.Methods.Add(draw);
    var nullableUnit = new GenericInstanceType(module.ImportReference(typeof(Nullable<>)));
    nullableUnit.GenericArguments.Add(unit);
    draw.Parameters.Add(new ParameterDefinition("name", ParameterAttributes.None, module.TypeSystem.String));
    draw.Parameters.Add(new ParameterDefinition("unit", ParameterAttributes.None, new ByReferenceType(nullableUnit)));
    draw.Parameters.Add(new ParameterDefinition("value", ParameterAttributes.None, new ByReferenceType(module.ImportReference(typeof(int?)))));
    draw.Parameters.Add(new ParameterDefinition("size", ParameterAttributes.None, module.ImportReference(typeof(int?))));
    draw.Body.InitLocals = true;
    draw.Body.Variables.Add(new VariableDefinition(unit));
    il = draw.Body.GetILProcessor();
    il.Emit(OpCodes.Ldsfld, selected);
    il.Emit(OpCodes.Stloc_0);
    foreach (var sink in new[] { preview, label })
    {
        il.Emit(OpCodes.Ldloca_S, draw.Body.Variables[0]);
        il.Emit(OpCodes.Constrained, unit);
        il.Emit(OpCodes.Callvirt, module.ImportReference(typeof(object).GetMethod(nameof(ToString), Type.EmptyTypes)!));
        if (sink == preview) il.Emit(OpCodes.Callvirt, module.ImportReference(typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes)!));
        il.Emit(OpCodes.Stsfld, sink);
    }
    foreach (var sink in new[] { choices, count })
    {
        il.Emit(OpCodes.Ldtoken, unit);
        il.Emit(OpCodes.Call, module.ImportReference(typeof(Type).GetMethod(nameof(Type.GetTypeFromHandle))!));
        il.Emit(OpCodes.Call, module.ImportReference(typeof(Enum).GetMethod(nameof(Enum.GetNames), [typeof(Type)])!));
        if (sink == count) { il.Emit(OpCodes.Ldlen); il.Emit(OpCodes.Conv_I4); }
        il.Emit(OpCodes.Stsfld, sink);
    }
    il.Emit(OpCodes.Ldc_I4_0);
    il.Emit(OpCodes.Ret);
    return module;
}
