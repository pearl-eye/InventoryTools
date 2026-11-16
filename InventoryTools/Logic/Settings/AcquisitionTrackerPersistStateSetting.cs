using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Logic.Settings.Abstract.Generic;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class AcquisitionTrackerPersistStateSetting : GenericIntegerSetting
{
    public AcquisitionTrackerPersistStateSetting(ILogger<AcquisitionTrackerPersistStateSetting> logger, ImGuiService imGuiService) : base("AcquisitionTrackerPersistState", "状态保持时间（秒）", "物品获取追踪器会记录制作、采集等当前状态。恢复空闲后，默认继续保持该状态2秒，以应对数据延迟。如果制作或采集的物品未被正确计入，请增大此值。", 2, "1.12.0.8", logger, imGuiService)
    {
    }
}