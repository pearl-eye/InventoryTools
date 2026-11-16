using InventoryTools.Logic.Settings;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.ConfigLayouts;

public class GeneralLayout : ConfigLayout
{
    public override PageLayout Build()
    {
        return Page("general", "常规",
            Section("保存",
                Paragraph("Allagan Tools 会保存自己的背包记录。以下选项控制记录写入磁盘的频率。"),
                Setting<AutoSaveSetting>("自动保存"),
                EnabledBy<AutoSaveSetting>(
                    Setting<AutoSaveTimeSetting>("保存间隔")),
                Setting<PersistDataSetting>()),
            Section("功能集成",
                Paragraph("从插件窗口外打开 Allagan Tools 的方式。"),
                Setting<AddTitleMenuButtonSetting>("在标题菜单中添加按钮"),
                Setting<CompendiumWotsitSetting>("在 Wotsit 中列出图鉴窗口")),
            Section("功能集成",
                Paragraph("从插件窗口外打开 Allagan Tools 的方式。"),
                Setting<ActiveCraftListSetting>())
        );
    }
}