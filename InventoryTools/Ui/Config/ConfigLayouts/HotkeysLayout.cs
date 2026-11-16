using InventoryTools.Logic.Settings;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.ConfigLayouts;

public class HotkeysLayout : ConfigLayout
{
    public override PageLayout Build()
    {
        return Page("hotkeys", "快捷键",
            Section("显示或隐藏窗口",
                Paragraph("无需悬停于物品，在任意位置都可使用。"),
                Setting<HotKeyListsWindowSetting>("清单"),
                Setting<HotkeyCraftWindowSetting>("制作"),
                Setting<HotkeyConfigWindowSetting>("设置"),
                Setting<HotkeyMobWindowSetting>("怪物"),
                Setting<HotkeyDutiesWindowSetting>("副本任务"),
                Setting<HotkeyAirshipWindowSetting>("飞空艇"),
                Setting<HotkeySubmarinesWindowSetting>("潜水艇"),
                Setting<HotkeyRetainerTasksWindowSetting>("雇员探险")),
            Section("鼠标悬停于物品时",
                Paragraph("这些操作作用于鼠标下的物品，只有悬停于物品时才生效。"),
                Setting<HotkeyMoreInfoSetting>("更多信息"),
                Setting<HotkeyOpenItemLogSetting>("适用的笔记"),
                Setting<HotkeyOpenCraftingLogSetting>("制作笔记"),
                Setting<HotkeyOpenGatheringLogSetting>("采集笔记"),
                Setting<HotkeyOpenFishingLogSetting>("钓鱼笔记"))
        );
    }
}
