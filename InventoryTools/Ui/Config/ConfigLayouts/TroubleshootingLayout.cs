using InventoryTools.Logic.Settings;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.ConfigLayouts;

public class TroubleshootingLayout : ConfigLayout
{
    public override PageLayout Build()
    {
        return Page("troubleshooting", "故障排查",
            Paragraph("仅在遇到特定问题时调整这些时间。默认值适合大多数情况。"),
            Section("物品获取追踪器",
                Paragraph("追踪制作、采集和掉落物品，以自动更新制作清单进度。两个时间均以秒为单位。增大它们可减少慢速连接或繁忙电脑造成的漏记，但更新会更慢。"),
                Setting<AcquisitionTrackerLoginDelaySetting>("登录后等待多久再扫描"),
                Setting<AcquisitionTrackerPersistStateSetting>("停止操作后继续追踪多久"))
        );
    }
}
