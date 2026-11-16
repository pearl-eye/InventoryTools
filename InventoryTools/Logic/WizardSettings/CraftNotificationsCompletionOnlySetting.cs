using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Logic.Settings.Abstract.Generic;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.WizardSettings;

public class CraftNotificationsCompletionOnlySetting : GenericBooleanSetting
{
    public override bool AppearsInConfigWindow => false;

    private bool _shouldAdd;

    public CraftNotificationsCompletionOnlySetting(ILogger<CraftNotificationsCompletionOnlySetting> logger,
        ImGuiService imGuiService) : base("CraftNotificationsCompletionOnly",
        "仅在物品数量达标时通知？",
        "仅在物品达到所需数量时发送消息，不逐次报告获取进度。制作清单启用后才会通知。",
        false,
        "15.0.8",
        logger,
        imGuiService)
    {
    }

    public override bool CurrentValue(InventoryToolsConfiguration configuration)
    {
        return _shouldAdd;
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
    {
        _shouldAdd = newValue;
    }
}