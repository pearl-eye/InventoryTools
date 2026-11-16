using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class TooltipAddCharacterNameSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = false;

        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.TooltipAddCharacterNameOwned;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.TooltipAddCharacterNameOwned = newValue;
        }

        public override string Key { get; set; } = "TooltipCharacterName";
        public override string Name { get; set; } = "添加物品位置（附加角色名）";

        public override string HelpText { get; set; } =
            "鼠标悬停于雇员持有的物品时，是否显示该雇员所属的角色名？";
        
        public override string Version => "1.7.0.0";

        public TooltipAddCharacterNameSetting(ILogger<TooltipAddCharacterNameSetting> logger, ImGuiService imGuiService) : base(logger, imGuiService)
        {
        }
    }
}