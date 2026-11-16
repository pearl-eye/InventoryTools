using System.Collections.Generic;
using InventoryTools.Logic.Settings;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Ui.Config;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Logic.Features;

public class ContextMenuFeature : Feature
{
    public ContextMenuFeature(IEnumerable<ISetting> settings) : base(settings)
    {
    }

    public override PageLayout Build()
    {
        return Page("feature/context-menu", "右键菜单",
            Paragraph("插件可以在物品的右键菜单中添加以下选项。"),
            Section("更多信息",
                Setting<ContextMenuMoreInformationSetting>("物品"),
                Setting<ContextMenuMoreInformationNpcsSetting>("NPC"),
                Setting<ContextMenuMoreInformationMonstersSetting>("怪物")),
            Section("打开游戏笔记",
                Setting<ContextMenuOpenCraftingLogSetting>("制作笔记"),
                Setting<ContextMenuOpenGatheringLogSetting>("采集笔记"),
                Setting<ContextMenuOpenFishingLogSetting>("钓鱼笔记")),
            Section("清单",
                Setting<ContextMenuAddToCraftListSetting>("添加至制作清单"),
                Setting<ContextMenuAddToActiveCraftListSetting>("添加至当前制作清单"),
                Setting<ContextMenuAddToCuratedListSetting>("添加至精选清单"),
                Setting<ContextMenuAddToFavouritesSetting>("添加至收藏或移出收藏")),
            Section("其他",
                Setting<ContextMenuCopyNameSetting>("复制物品名称"))
        );
    }
}
