using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class InvertTabHighlightingSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;

        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.InvertTabHighlighting;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.InvertTabHighlighting = newValue;
        }

        public override string Key { get; set; } = "InvertTabHighlighting";
        public override string Name { get; set; } = "反向高亮标签页？";

        public override string HelpText { get; set; } =
            "是否改为高亮不符合清单条件的背包分页？可在清单设置中单独覆盖。";
        
        public override string Version => "1.7.0.0";

        public InvertTabHighlightingSetting(ILogger<InvertTabHighlightingSetting> logger, ImGuiService imGuiService) : base(logger, imGuiService)
        {
        }
    }
}