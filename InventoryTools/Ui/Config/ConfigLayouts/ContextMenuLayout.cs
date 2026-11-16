using InventoryTools.Logic.Settings;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.ConfigLayouts;

public class ContextMenuLayout : ConfigLayout
{
    public override PageLayout Build()
    {
        return Page("context-menu", "右键菜单",
            Paragraph("Allagan Tools 可以在游戏右键菜单中添加选项。每项都会占用一行，可分别开关。"),
            Section("清单",
                Setting<ContextMenuAddToCraftListSetting>("添加至制作清单"),
                Setting<ContextMenuAddToActiveCraftListSetting>("添加至当前制作清单"),
                Setting<ContextMenuAddToCuratedListSetting>("添加至精选清单"),
                Setting<ContextMenuAddToFavouritesSetting>("添加至收藏或移出收藏")),
            Section("更多信息",
                Paragraph("打开右键所选内容的 Allagan Tools 信息窗口。"),
                Setting<ContextMenuMoreInformationSetting>("物品"),
                Setting<ContextMenuMoreInformationNpcsSetting>("NPC"),
                Setting<ContextMenuMoreInformationMonstersSetting>("怪物")),
            Section("在游戏中打开",
                Paragraph("快速打开游戏内的各类笔记。"),
                Setting<ContextMenuOpenCraftingLogSetting>("制作笔记"),
                Setting<ContextMenuOpenGatheringLogSetting>("采集笔记"),
                Setting<ContextMenuOpenFishingLogSetting>("钓鱼笔记")),
            Section("搜索",
                Paragraph("搜索范围也适用于从 Allagan Tools 自身菜单发起的搜索。"),
                Paragraph("此搜索会覆盖插件已记录的所有角色，范围比游戏默认的物品搜索更广。"),
                Setting<ContextMenuItemSearchSetting>("搜索此物品"),
                Paragraph("若需缩小搜索范围，请在下方指定位置。"),
                Setting<ContextMenuItemSearchScopeSetting>("搜索这些位置")),
            Section("其他",
                Setting<ContextMenuCopyNameSetting>("复制物品名称"))
        );
    }
}
