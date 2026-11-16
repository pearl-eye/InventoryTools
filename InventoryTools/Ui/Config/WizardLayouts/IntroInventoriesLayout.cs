using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.WizardLayouts;

public class IntroInventoriesLayout : ContentLayout
{
    public override PageLayout Build()
    {
        return Page("intro/inventories", "您的物品",
            Paragraph("插件只有在游戏展示过某个背包后才能记录它。这是游戏数据发送方式的限制，无法通过插件设置跳过。"),
            Section("如果缺少内容",
                Paragraph("在游戏中打开一次后，插件就会记录。常见位置包括："),
                Bullet("雇员：使用传唤铃，逐一打开各雇员的背包。"),
                Bullet("部队储物柜：打开需要记录的每一页。"),
                Bullet("幻化台和收藏柜。"),
                Bullet("陆行鸟鞍囊。")),
            Section("记录之后",
                Paragraph("插件会保存这些记录，之后可随时搜索雇员背包，无需再前往传唤铃。"))
        );
    }
}
