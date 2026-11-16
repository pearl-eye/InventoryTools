using System.Collections.Generic;
using AllaganLib.Monitors.Enums;
using AllaganLib.Monitors.Interfaces;
using CriticalCommonLib;
using Dalamud.Bindings.ImGui;
using FFXIVClientStructs.FFXIV.Client.Game;

namespace InventoryTools.Debuggers;

public class AcquisitionDebuggerPane : DebugLogPane
{
    private readonly IAcquisitionMonitorService _acquisitionMonitorService;

    public AcquisitionDebuggerPane(IAcquisitionMonitorService acquisitionMonitorService)
    {
        _acquisitionMonitorService = acquisitionMonitorService;
    }

    public override string Name => "物品获取监视器";

    public override void SubscribeToEvents()
    {
        _acquisitionMonitorService.ItemAcquired += OnItemAcquired;
        RegisterSubscription(() => _acquisitionMonitorService.ItemAcquired -= OnItemAcquired);
    }

    private void OnItemAcquired(
        uint itemId,
        InventoryItem.ItemFlags itemFlags,
        int qtyIncrease,
        AcquisitionReason reason)
    {
        AddLog(
            $"Item acquired: ItemId={itemId}, " +
            $"QtyChange={qtyIncrease}, " +
            $"Flags={itemFlags}, " +
            $"Reason={reason}"
        );
    }

    public override void DrawInfo()
    {
        if (ImGui.CollapsingHeader("设置"))
        {
            var config = _acquisitionMonitorService.Configuration;

            if (config == null)
            {
                ImGui.TextUnformatted("<无配置>");
            }
            else
            {
                // Intentionally generic: avoids assumptions about configuration shape
                Utils.PrintOutObject(config, 0, new List<string>());
            }
        }

        if (ImGui.CollapsingHeader("最近活动"))
        {
            ImGui.TextUnformatted(
                "下方日志面板按时间顺序列出物品获取事件。"
            );
        }
    }
}