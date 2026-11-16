using System.Collections.Generic;
using InventoryTools.Logic.Settings;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Ui.Config;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Logic.Features;

public class HotkeysFeature : Feature
{
    public HotkeysFeature(IEnumerable<ISetting> settings) : base(settings)
    {
    }

    public override PageLayout Build()
    {
        return Page("feature/hotkeys", "快捷键",
            Paragraph("这些快捷键均为可选。某一项留空时，该操作不设置快捷键。"),
            Section("显示或隐藏窗口",
                Setting<HotKeyListsWindowSetting>("清单"),
                Setting<HotkeyCraftWindowSetting>("制作"),
                Setting<HotkeyConfigWindowSetting>("设置"),
                Setting<HotkeyMobWindowSetting>("怪物"),
                Setting<HotkeyDutiesWindowSetting>("副本任务"),
                Setting<HotkeyAirshipWindowSetting>("飞空艇"),
                Setting<HotkeySubmarinesWindowSetting>("潜水艇"),
                Setting<HotkeyRetainerTasksWindowSetting>("雇员探险")),
            Section("鼠标悬停于物品时",
                Paragraph("这些快捷键作用于鼠标下的物品。未悬停于物品时不会执行操作。"),
                Setting<HotkeyMoreInfoSetting>("更多信息"),
                Setting<HotkeyOpenItemLogSetting>("适用于该物品的笔记"),
                Setting<HotkeyOpenCraftingLogSetting>("制作笔记"),
                Setting<HotkeyOpenGatheringLogSetting>("采集笔记"),
                Setting<HotkeyOpenFishingLogSetting>("钓鱼笔记"))
        );
    }
}
