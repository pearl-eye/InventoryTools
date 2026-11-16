using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class TooltipMinimumMarketPriceSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = false;

        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.TooltipDisplayMarketLowestPrice;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.TooltipDisplayMarketLowestPrice = newValue;
        }

        public override string Key { get; set; } = "TooltipDisplayMBMinimum";
        public override string Name { get; set; } = "添加NQ/HQ市场最低价？";

        public override string WizardName { get; } = "市场价格";

        public override string HelpText { get; set; } =
            "鼠标悬停于物品时，在物品提示中显示NQ和HQ的市场最低价。请确保已启用“自动下载价格”。";

        public override string Version => "1.7.0.0";

        public TooltipMinimumMarketPriceSetting(ILogger<TooltipMinimumMarketPriceSetting> logger, ImGuiService imGuiService) : base(logger, imGuiService)
        {
        }
    }
}