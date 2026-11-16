using System.Collections.Generic;
using InventoryTools.Logic.Settings;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Ui.Config;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Logic.Features;

public class TooltipsFeature : Feature
{
    public TooltipsFeature(IEnumerable<ISetting> settings) : base(settings)
    {
    }

    public override PageLayout Build()
    {
        return Page("feature/tooltips", "物品提示",
            Paragraph("Allagan Tools 可以在游戏的物品提示中添加额外信息。"),
            Setting<TooltipDisplayAmountOwnedSetting>("已持有物品的位置"),
            Setting<TooltipDisplayRetrieveAmountSetting>("当前制作清单仍需的数量"),
            Setting<TooltipMinimumMarketPriceSetting>("市场价格"),
            Setting<TooltipDisplayUnlockSetting>("是否已学习或解锁物品"),
            Setting<TooltipSourceInformationEnabledSetting>("物品获取来源"),
            Setting<TooltipUseInformationEnabledSetting>("物品用途"),
            Setting<TooltipDisplayIngredientPatchSetting>("材料所属版本"),
            Setting<TooltipDisplayCofferLootSetting>("宝箱可能包含的物品"),
            Setting<TooltipDisplayCuratedListsSetting>("物品是否已加入精选清单"),
            Setting<TooltipDisplayGlamourReadySetSetting>("物品是否能补全整套幻化"),
            Paragraph("各项信息都可在设置窗口的“物品提示”中进一步配置，包括颜色、搜索位置和显示方式。")
        );
    }
}
