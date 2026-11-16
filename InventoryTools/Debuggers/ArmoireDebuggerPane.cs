using System;
using AllaganLib.Shared.Interfaces;
using Dalamud.Bindings.ImGui;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.UI;

namespace InventoryTools.Debuggers;

public class ArmoireDebuggerPane : IDebugPane
{
    private readonly IGameGui _gameGui;

    public ArmoireDebuggerPane(IGameGui gameGui)
    {
        _gameGui = gameGui;
    }

    public string Name => "收藏柜";
    public unsafe void Draw()
    {
        var uiState = UIState.Instance();
        if (uiState == null)
        {
            ImGui.Text("未找到界面状态（UIState）。");
        }
        else
        {
            ImGui.Text(uiState->Cabinet.IsCabinetLoaded() ? "收藏柜已加载" : "收藏柜未加载");
        }

        var addon = this._gameGui.GetAddonByName("CabinetWithdraw");
        if (addon != IntPtr.Zero)
        {
            var cabinetWithdraw = (AddonCabinetWithdraw*)addon.Address;
            if (cabinetWithdraw != null)
            {
                ImGui.Text($"已选择职业专用装备：{ (cabinetWithdraw->ArtifactArmorRadioButton->IsChecked ? "是" : "否") }");
                ImGui.Text($"已选择节日装备1：{ (cabinetWithdraw->SeasonalGear1RadioButton->IsChecked ? "是" : "否") }");
                ImGui.Text($"已选择节日装备2：{ (cabinetWithdraw->SeasonalGear2RadioButton->IsChecked ? "是" : "否") }");
                ImGui.Text($"已选择节日装备3：{ (cabinetWithdraw->SeasonalGear3RadioButton->IsChecked ? "是" : "否") }");
                ImGui.Text($"已选择节日装备4：{ (cabinetWithdraw->SeasonalGear4RadioButton->IsChecked ? "是" : "否") }");
                ImGui.Text($"已选择节日装备5：{ (cabinetWithdraw->SeasonalGear5RadioButton->IsChecked ? "是" : "否") }");
                ImGui.Text($"已选择成就奖励：{ (cabinetWithdraw->AchievementsRadioButton->IsChecked ? "是" : "否") }");
                ImGui.Text($"已选择特典道具：{ (cabinetWithdraw->ExclusiveExtrasRadioButton->IsChecked ? "是" : "否") }");
                ImGui.Text($"已选择搜索：{ (cabinetWithdraw->SearchRadioButton->IsChecked ? "是" : "否") }");
            }
        }
    }
}