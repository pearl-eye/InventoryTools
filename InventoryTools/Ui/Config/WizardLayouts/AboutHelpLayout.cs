using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.WizardLayouts;

public class AboutHelpLayout : ContentLayout
{
    public override PageLayout Build()
    {
        return Page("help/about", "关于",
            Paragraph("作者利用业余时间编写此插件，希望能持续为大家提供更新。"),
            Paragraph("遇到问题时，请通过插件安装器中的反馈按钮提交反馈。"),
            Section("相关链接",
                Link("打开Wiki", "https://github.com/Critical-Impact/InventoryTools/wiki/1.-Overview"),
                Link("报告问题", "https://github.com/Critical-Impact/InventoryTools/issues"))
        );
    }
}
