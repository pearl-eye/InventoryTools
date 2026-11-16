using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Logic.Settings.Abstract.Generic;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class AcquisitionTrackerLoginDelaySetting : GenericIntegerSetting
{
    public AcquisitionTrackerLoginDelaySetting(ILogger<AcquisitionTrackerLoginDelaySetting> logger, ImGuiService imGuiService) : base("AcquisitionTrackerLoginDelay", "登录后延迟", "物品获取追踪器会在登录时首次扫描角色的背包。游戏会分多次更新背包数据，因此扫描器需要稍作等待。如果刚登录时制作清单中的物品会被误判为已完成，请增大此值。", 5, "1.12.0.8", logger, imGuiService)
    {
    }
}