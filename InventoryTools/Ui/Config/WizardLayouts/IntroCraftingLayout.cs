using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.WizardLayouts;

public class IntroCraftingLayout : ContentLayout
{
    public override PageLayout Build()
    {
        return Page("intro/crafting", "制作",
            Paragraph("制作清单会把想制作的物品拆分成全部中间材料和原材料，再与已持有物品核对。"),
            Section("提供的内容",
                Bullet("完整材料树，包含各级中间材料。"),
                Bullet("已持有的材料，以及保管它们的角色或雇员。"),
                Bullet("缺少材料的购买或采集地点。"),
                Bullet("随着材料获取自动更新进度。")),
            Section("开始浏览",
                OpenWindow<CraftsWindow>("打开制作窗口"))
        );
    }
}
