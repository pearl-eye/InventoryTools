using InventoryTools.Logic.Settings;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.ConfigLayouts;

public class CraftOverlayLayout : ConfigLayout
{
    public override PageLayout Build()
    {
        return Page("craft-overlay", "制作悬浮窗",
            Paragraph("紧凑的悬浮面板，会跟随当前制作清单显示进度，无需打开完整窗口即可查看缺少的物品。"),
            Section("显示",
                Setting<CraftOverlayMaxExpandedItemsSetting>("展开时显示的物品数"),
                Setting<CraftOverlayHideSetting>("在副本和过场动画中隐藏")),
            Section("行为",
                Setting<CraftOverlayRememberStateSetting>("重新加载后保持打开"))
        );
    }
}
