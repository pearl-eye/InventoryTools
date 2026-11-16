using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.WizardLayouts;

public class IntroListsLayout : ContentLayout
{
    public override PageLayout Build()
    {
        return Page("intro/lists", "清单",
            Paragraph("插件的多数功能都基于清单。清单是保存下来的物品搜索条件，可用于高亮搜索结果、整理物品或持续追踪。"),
            Section("三种清单",
                Bullet("搜索清单：在各背包中查找物品。"),
                Bullet("整理清单：查找物品，并规划应移至的位置。"),
                Bullet("游戏物品清单：搜索游戏中的全部物品。")),
            Section("开始浏览",
                Paragraph("可在物品窗口管理清单，也可安装示例清单。"),
                OpenWindow<FiltersWindow>("打开物品窗口"))
        );
    }
}
