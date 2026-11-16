using InventoryTools.EquipmentSuggest;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.WizardLayouts;

public class IntroWindowsLayout : ContentLayout
{
    public override PageLayout Build()
    {
        return Page("intro/windows", "其他工具",
            Paragraph("除清单、制作和图鉴外，插件还提供两个计算工具，可从任意 Allagan Tools 窗口底部的菜单按钮打开。"),
            Section("陆行鸟染色计算器",
                Paragraph("选择想要的陆行鸟羽色，计算器会给出应喂食的水果及顺序。确认方案后，还可记录每次喂食进度。"),
                OpenWindow<ChocoboColourWindow>("打开陆行鸟染色计算器")),
            Section("装备推荐",
                Paragraph("选择职业后，从所有角色和雇员持有的装备中查找提升项，综合考虑物品品级和该职业所需属性。"),
                OpenWindow<EquipmentSuggestWindow>("打开装备推荐")),
            Section("两点提示",
                Paragraph("右键点击物品或表格行通常会显示更多操作。在插件任意位置点击物品图标，都可打开完整信息页。"))
        );
    }
}
