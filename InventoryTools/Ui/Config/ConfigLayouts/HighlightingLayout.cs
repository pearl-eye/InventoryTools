using InventoryTools.Logic.Settings;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.ConfigLayouts;

public class HighlightingLayout : ConfigLayout
{
    public override PageLayout Build()
    {
        return Page("highlighting", "高亮",
            Paragraph("高亮会为符合清单条件的物品格位和背包分页着色。以下为默认设置，多数选项可在各清单中覆盖。"),
            Section("当前清单",
                Paragraph("通常可通过窗口内的“高亮”按钮或文本指令切换高亮，也可在此开关。"),
                Setting<WindowFilterSetting>(),
                Setting<BackgroundFilterSetting>(),
                Setting<SaveBackgroundFilterSetting>("重新登录后记住选择")
            ),
            Section("高亮时机",
                Setting<HighlightWhenSetting>(),
                Setting<InvertHighlightingSetting>(),
                Setting<InvertTabHighlightingSetting>()),
            Section("颜色",
                Setting<HighlightColourSetting>("符合条件的物品颜色"),
                Setting<TabHighlightColourSetting>("符合条件的分页颜色")),
            Section("目标",
                Paragraph("整理清单会规划物品的目标位置。可同时高亮来源和目标背包，方便查看物品应从哪里移到哪里。"),
                Setting<HighlightDestinationSetting>(),
                EnabledBy<HighlightDestinationSetting>(
                    Setting<HighlightDestinationEmptySetting>(),
                    Setting<InvertDestinationHighlightingSetting>(),
                    Setting<HighlightDestinationColourSetting>("目标位置颜色"))),
            Section("雇员列表",
                Paragraph("雇员持有符合清单条件的物品时，可在传唤铃列表中标记，方便直接找到应打开的雇员。"),
                Setting<ColourRetainerListSetting>("为雇员名字着色"),
                EnabledBy<ColourRetainerListSetting>(
                    Setting<RetainerListColourSetting>("名字颜色")),
                Setting<ShowItemNumberRetainerListSetting>("显示物品数量")),
            Section("商店高亮",
                Paragraph("打开商店时，可高亮符合清单条件的商品，并在场景中标记出售这些商品的商人。"),
                Setting<ShopHighlightingDisableItemsSetting>("淡化不符合条件的物品"),
                Setting<ShopHighlightingNpcSetting>("高亮场景中的商人NPC"),
                EnabledBy<ShopHighlightingNpcSetting>(
                    Setting<ShopHighlightingNpcColorSetting>("高亮颜色"),
                    Setting<ShopHighlightingNpcNameplateIconSetting>("在姓名牌上显示图标")))
        );
    }
}