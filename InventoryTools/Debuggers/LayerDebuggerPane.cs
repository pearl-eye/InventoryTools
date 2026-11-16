using AllaganLib.Shared.Interfaces;
using Dalamud.Bindings.ImGui;
using FFXIVClientStructs.FFXIV.Client.LayoutEngine;

namespace InventoryTools.Debuggers;

public class LayerDebuggerPane : IDebugPane
{
    public string Name => "场景层级调试器";
    public unsafe void Draw()
    {
        var activeLayout = LayoutWorld.Instance()->ActiveLayout;
        if (activeLayout != null)
        {
            ImGui.TextUnformatted($"场景 ID：{activeLayout->LevelId}");
            ImGui.TextUnformatted($"ID: {activeLayout->Id}");
            ImGui.TextUnformatted($"类型: {activeLayout->Type}");
            ImGui.TextUnformatted($"资源字符串：{activeLayout->Type}");
            foreach (var resourcePath in activeLayout->ResourcePaths.Strings)
            {
                if (resourcePath.Value != null)
                {
                    ImGui.TextUnformatted($"{resourcePath.Value->DataString}");
                }
            }
            ImGui.TextUnformatted($"场景层级：");
            foreach (var layer in activeLayout->Layers)
            {
                ImGui.TextUnformatted($"{layer.Item1}");
                var pointer = layer.Item2.Value;
                if (pointer != null)
                {
                    ImGui.TextUnformatted($"层级 ID：" + pointer->Id);
                    ImGui.TextUnformatted($"层级组 ID：" + pointer->LayerGroupId);
                    ImGui.TextUnformatted($"节日 ID：" + pointer->FestivalId);
                }
            }
        }
    }
}