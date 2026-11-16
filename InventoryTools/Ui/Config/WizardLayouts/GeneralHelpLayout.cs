using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.WizardLayouts;

public class GeneralHelpLayout : ContentLayout
{
    public override PageLayout Build()
    {
        return Page("help/general", "常规",
            Paragraph("Allagan Tools 主要提供三项功能：记录并显示背包数据、规划制作，以及查询物品信息。"),
            Paragraph("部分功能的设计参考了 Teamcraft 和 Garland Tools。"),
            Section("背包记录",
                Paragraph("插件会尽量保持背包记录更新。部分背包只有打开过一次后才能被缓存。如果没有显示雇员、部队、幻化台等内容，请先在游戏中打开对应界面。"),
                Paragraph("记录物品后，即可创建清单来查找、整理或追踪特定物品。")),
            Section("制作规划",
                Paragraph("制作窗口可以列出想制作的物品，并将其拆分成所需部件和材料，显示缺少什么、已有材料放在哪里，以及缺少的材料从哪里获取或购买。"),
                Paragraph("使用过 Teamcraft 的玩家应该会比较熟悉这种方式。")),
            Section("物品信息",
                Paragraph("插件内置了较完整的物品数据库，提供与 Garland Tools 类似的信息。在插件任意位置点击物品图标，都可以打开该物品的信息窗口。")),
            Section("高亮",
                Paragraph("使用物品清单或制作清单时，可以启用高亮，在游戏中标出相关物品。插件窗口打开时，勾选“高亮”即可。若想通过宏控制，请参考帮助中的指令说明，切换“后台”高亮。")),
            Section("更多",
                Paragraph("这里只是基础介绍，更多说明请查看Wiki。"),
                Link("打开Wiki", "https://github.com/Critical-Impact/InventoryTools/wiki/1.-Overview"))
        );
    }
}
