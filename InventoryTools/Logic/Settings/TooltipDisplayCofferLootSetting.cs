using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class TooltipDisplayCofferLootSetting : BooleanSetting
{
    public override bool DefaultValue { get; set; } = false;

    public override bool CurrentValue(InventoryToolsConfiguration configuration)
    {
        return configuration.TooltipDisplayCofferLoot;
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
    {
        configuration.TooltipDisplayCofferLoot = newValue;
    }

    public override string Key { get; set; } = "TooltipDisplayCofferLoot";
    public override string Name { get; set; } = "宝箱内容信息";

    public override string WizardName { get; } = "宝箱内容信息";

    public override string HelpText { get; set; } =
        "鼠标悬停于宝箱、卡包或其他奖励容器时，显示其中可能获得的物品已有多少。";
    
    public override string Version => "1.12.0.0";

    public TooltipDisplayCofferLootSetting(ILogger<TooltipDisplayCofferLootSetting> logger, ImGuiService imGuiService) : base(logger, imGuiService)
    {
    }
}
