using InventoryTools.Compendium.Windows;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.WizardLayouts;

public class IntroCompendiumLayout : ContentLayout
{
    public override PageLayout Build()
    {
        return Page("intro/compendium", "图鉴",
            Paragraph("图鉴提供可浏览的游戏内容资料，使用插件其他功能所用的相同数据。想了解某个内容是什么、来自哪里或由谁掉落，都可以在这里查找。"),
            Section("包含的内容",
                Bullet("物品、任务和成就。"),
                Bullet("坐骑、宠物和幻化套装。"),
                Bullet("副本、理符任务、友好部族和老主顾交易。"),
                Bullet("古武器及工具、秘籍、传承录和灵魂水晶。"),
                Bullet("NPC、区域，以及飞空艇和潜水艇航线。"),
                Bullet("职业、装备方案、陆行鸟物品等。")),
            Section("开始浏览",
                Paragraph("每条资料都链接到相关内容，可以从物品找到掉落它的副本，再找到出售相关物品的NPC。"),
                OpenWindow<CompendiumTypesWindow>("打开图鉴"),
                Paragraph("也可从物品或制作窗口的“图鉴”菜单打开，或使用 /compendium 指令。"))
        );
    }
}
