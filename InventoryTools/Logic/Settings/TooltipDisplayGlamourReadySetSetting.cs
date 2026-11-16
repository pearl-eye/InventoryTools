using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class TooltipDisplayGlamourReadySetSetting : BooleanSetting
{
    public override bool DefaultValue { get; set; } = false;

    public override bool CurrentValue(InventoryToolsConfiguration configuration)
    {
        return configuration.TooltipDisplayGlamourReadySet;
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
    {
        configuration.TooltipDisplayGlamourReadySet = newValue;
    }

    public override string Key { get; set; } = "TooltipDisplayGlamourReadySet";
    public override string Name { get; set; } = "整套幻化信息";

    public override string WizardName { get; } = "整套幻化信息";

    public override string HelpText { get; set; } =
        "鼠标悬停于物品时，显示它是否属于整套幻化，以及组成物品中有哪些已存入兵装库或收藏柜。";
    
    public override string Version => "1.12.0.0";

    public TooltipDisplayGlamourReadySetSetting(ILogger<TooltipDisplayGlamourReadySetSetting> logger, ImGuiService imGuiService) : base(logger, imGuiService)
    {
    }
}
