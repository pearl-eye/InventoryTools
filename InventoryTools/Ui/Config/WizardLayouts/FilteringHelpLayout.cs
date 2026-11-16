using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.WizardLayouts;

public class FilteringHelpLayout : ContentLayout
{
    public override PageLayout Build()
    {
        return Page("help/filtering", "筛选",
            Section("高级搜索语法",
                Paragraph("创建清单或搜索清单结果时，可使用运算符缩小搜索范围。可用运算符取决于搜索的字段。"),
                Bullet("!  - 排除包含输入内容的结果，适用于文字和数字。"),
                Bullet("<  - 小于输入值，适用于数字。"),
                Bullet(">  - 大于输入值，适用于数字。"),
                Bullet(">= - 大于或等于输入值，适用于数字。"),
                Bullet("<= - 小于或等于输入值，适用于数字。"),
                Bullet("=  - 与输入值完全相同，适用于文字和数字。"),
                Bullet("&& 和 || - 分别表示“并且”和“或者”，用于连接多个条件。"))
        );
    }
}
