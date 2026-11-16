using System.Collections.Generic;
using InventoryTools.Logic.Settings;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Ui.Config;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Logic.Features;

public class LayoutFeature : Feature
{
    public LayoutFeature(IEnumerable<ISetting> settings) : base(settings)
    {
    }

    public override PageLayout Build()
    {
        return Page("feature/layout", "布局",
            Paragraph("物品窗口和制作窗口可以使用顶部标签或侧栏显示清单。清单较多时适合侧栏，较少时适合标签。"),
            Setting<FiltersWindowLayoutSetting>("物品窗口"),
            Setting<CraftWindowLayoutSetting>("制作窗口")
        );
    }
}
