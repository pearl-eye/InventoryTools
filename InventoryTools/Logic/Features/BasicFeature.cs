using System.Collections.Generic;
using InventoryTools.Logic.Settings;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Ui.Config;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Logic.Features;

public class BasicFeature : Feature
{
    public BasicFeature(IEnumerable<ISetting> settings) : base(settings)
    {
    }

    public override PageLayout Build()
    {
        return Page("feature/basic", "基础设置",
            Paragraph("这些设置作用于整个插件。之后都可在设置窗口中修改。"),
            Setting<AutoSaveSetting>("自动保存背包数据"),
            Setting<HistoryEnabledSetting>("启用物品历史记录"),
            Setting<AddTitleMenuButtonSetting>("在游戏标题菜单中添加按钮")
        );
    }
}