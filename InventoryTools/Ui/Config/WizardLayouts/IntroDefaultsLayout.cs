using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.WizardLayouts;

public class IntroDefaultsLayout : ContentLayout
{
    public override PageLayout Build()
    {
        return Page("intro/defaults", "默认设置",
            Paragraph("插件默认启用了一组常用功能。"),
            Paragraph("接下来将展示常用功能的设置。"),
            Paragraph("将鼠标移至问号图标上，可查看各选项的说明。")
        );
    }
}