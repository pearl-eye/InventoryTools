using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.WizardLayouts;

public class ListBasicsHelpLayout : ContentLayout
{
    public override PageLayout Build()
    {
        return Page("help/list-basics", "清单基础",
            Paragraph("清单是查看和整理物品的主要方式，目前可创建三种类型。"),
            Section("搜索清单",
                Paragraph("在全部背包中搜索特定物品。只需查找、不需要规划整理时，适合使用此类型。"),
                Paragraph("用途示例："),
                Bullet("寻找制作材料。"),
                Bullet("寻找存放在某处的家具。"),
                Bullet("查看刚获得的物品价值多少。"),
                Bullet("检查某物品是否已存入幻化台或收藏柜。"),
                Bullet("无需前往传唤铃即可查看雇员装备。"),
                Bullet("检查已持有物品中哪些可放入收藏柜。")),
            Section("整理清单",
                Paragraph("在搜索清单的基础上，指定物品应整理到哪里。插件会尽量规划更节省空间的存放方式。"),
                Paragraph("用途示例："),
                Bullet("制作后归置材料，避免多处重复存放。"),
                Bullet("将达到指定物品品级的装备放入陆行鸟鞍囊备用。"),
                Bullet("找出适合存入部队储物柜的物品并整理过去。")),
            Section("游戏物品清单",
                Paragraph("搜索游戏物品目录中的全部物品。"),
                Paragraph("用途示例："),
                Bullet("查找幻化装备。"),
                Bullet("查看尚未获取的坐骑和宠物。"),
                Bullet("追踪游戏中各物品的市场价格。"))
        );
    }
}
