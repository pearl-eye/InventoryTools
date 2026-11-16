using InventoryTools.Boot;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Logic.Settings.Abstract.Generic;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class PersistDataSetting : GenericBooleanSetting
{
    private readonly BootConfiguration _bootConfiguration;

    public PersistDataSetting(ILogger<PersistDataSetting> logger, BootConfiguration bootConfiguration, ImGuiService imGuiService) : base("PersistData", "持久保存缓存数据", "首次启动时，Allagan Tools 需要计算部分信息，可能耗时5至10秒，具体取决于电脑性能。启用后会在更新之间保留这些数据，加快插件启动。", true, "14.0.5", logger, imGuiService)
    {
        _bootConfiguration = bootConfiguration;
    }

    public override bool CurrentValue(InventoryToolsConfiguration configuration)
    {
        return _bootConfiguration.PersistLuminaCache;
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
    {
        _bootConfiguration.PersistLuminaCache = newValue;
    }
}