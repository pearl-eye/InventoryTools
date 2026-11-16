using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class ShowFiltersTabSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.ShowFilterTab;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.ShowFilterTab = newValue;
        }

        public override string Key { get; set; } = "ShowFiltersTab";
        public override string Name { get; set; } = "显示“全部清单”分页？";

        public override string HelpText { get; set; } =
            "是否在主窗口显示“全部清单”分页，将全部可用清单放在一起？";
        
        public override string Version => "1.7.0.0";

        public ShowFiltersTabSetting(ILogger<ShowFiltersTabSetting> logger, ImGuiService imGuiService) : base(logger, imGuiService)
        {
        }
    }
}