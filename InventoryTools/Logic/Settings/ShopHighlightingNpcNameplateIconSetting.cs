using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Logic.Settings.Abstract.Generic;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class ShopHighlightingNpcNameplateIconSetting : GenericBooleanSetting
{
    public ShopHighlightingNpcNameplateIconSetting(ILogger<ShopHighlightingNpcNameplateIconSetting> logger, ImGuiService imGuiService)
        : base(
            "ShopHighlightingNpcNameplateIcon",
            "商店高亮 - 姓名牌图标",
            "高亮商店物品时，在场景中对应NPC的姓名牌上显示小型商人图标。",
            true,
            "15.0.4",
            logger,
            imGuiService)
    {
    }
}
