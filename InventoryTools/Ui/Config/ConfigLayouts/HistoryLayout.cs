using InventoryTools.Logic.Settings;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.ConfigLayouts;

public class HistoryLayout : ConfigLayout
{
    public override PageLayout Build()
    {
        return Page("history", "历史记录",
            Paragraph("记录背包中物品的获得、移除和移动，便于回顾变化。历史清单会读取这些记录。"),
            Paragraph("未启用此项时，历史类型的清单无法工作。"),
            Section("追踪",
                Setting<HistoryEnabledSetting>("记录背包变化"),
                EnabledBy<HistoryEnabledSetting>(
                    Setting<HistoryTrackEventsSetting>("要记录的事件")))
        );
    }
}
