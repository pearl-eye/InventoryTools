using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class TooltipDisplayUnlockSetting : BooleanSetting
{
    public TooltipDisplayUnlockSetting(ILogger<TooltipDisplayUnlockSetting> logger, ImGuiService imGuiService) : base(logger, imGuiService)
    {
    }

    public override bool DefaultValue { get; set; } = false;
    public override bool CurrentValue(InventoryToolsConfiguration configuration)
    {
        return configuration.TooltipDisplayUnlock;
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
    {
        configuration.TooltipDisplayUnlock = newValue;
    }

    public override string Key { get; set; } = "TooltipDisplayUnlockSetting";
    public override string Name { get; set; } = "添加物品解锁状态";
    public override string HelpText { get; set; } = "对于可以学习或获取的物品，显示各角色是否已解锁或获取。可在设置窗口中指定要显示的角色。";
    public override string Version { get; } = "1.11.0.4";
}