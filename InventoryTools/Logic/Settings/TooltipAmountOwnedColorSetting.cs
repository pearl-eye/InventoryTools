using System.Numerics;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Logic.Settings.Abstract.Generic;
using InventoryTools.Services;
using Lumina.Excel;
using Lumina.Excel.Sheets;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class TooltipAmountOwnedColorSetting : GenericGameColorSetting
{
    public TooltipAmountOwnedColorSetting(ILogger<TooltipAmountOwnedColorSetting> logger, ImGuiService imGuiService, ExcelSheet<UIColor> uiColorSheet) : base("TooltipAmountOwnedColor", "文字颜色", "启用“添加物品位置”时，所添加文字的颜色。", null, "1.11.0.11", logger, imGuiService, uiColorSheet)
    {
        this.DefaultValue = 8;
    }
}


public class TooltipMarketPricingColorSetting : GenericGameColorSetting
{
    public TooltipMarketPricingColorSetting(ILogger<TooltipMarketPricingColorSetting> logger, ImGuiService imGuiService, ExcelSheet<UIColor> uiColorSheet) : base("TooltipMarketPricingColor", "文字颜色", "启用“市场价格”时，所添加文字的颜色。", null, "1.11.0.11", logger, imGuiService, uiColorSheet)
    {
        this.DefaultValue = 559;
    }
}
public class TooltipAmountToRetrieveColorSetting : GenericGameColorSetting
{
    public TooltipAmountToRetrieveColorSetting(ILogger<TooltipAmountToRetrieveColorSetting> logger, ImGuiService imGuiService, ExcelSheet<UIColor> uiColorSheet) : base("TooltipAmountToRetrieveColor", "文字颜色", "启用“需取出数量”时，所添加文字的颜色。", null, "1.11.0.11", logger, imGuiService, uiColorSheet)
    {
        this.DefaultValue = 37;
    }
}
public class TooltipItemUnlockStatusColorSetting : GenericGameColorSetting
{
    public TooltipItemUnlockStatusColorSetting(ILogger<TooltipItemUnlockStatusColorSetting> logger, ImGuiService imGuiService, ExcelSheet<UIColor> uiColorSheet) : base("TooltipItemUnlockStatusColor", "文字颜色", "启用“物品解锁状态”时，所添加文字的颜色。", null, "1.11.0.11", logger, imGuiService, uiColorSheet)
    {
        this.DefaultValue = 555;
    }
}
public class TooltipSourceInformationColorSetting : GenericGameColorSetting
{
    public TooltipSourceInformationColorSetting(ILogger<TooltipSourceInformationColorSetting> logger, ImGuiService imGuiService, ExcelSheet<UIColor> uiColorSheet) : base("TooltipSourceInformationColor", "文字颜色", "启用“来源信息”时，所添加文字的颜色。", null, "1.11.0.11", logger, imGuiService, uiColorSheet)
    {
        this.DefaultValue = 502;
    }
}
public class TooltipUseInformationColorSetting : GenericGameColorSetting
{
    public TooltipUseInformationColorSetting(ILogger<TooltipUseInformationColorSetting> logger, ImGuiService imGuiService, ExcelSheet<UIColor> uiColorSheet) : base("TooltipUseInformationColor", "文字颜色", "启用“用途信息”时，所添加文字的颜色。", null, "1.11.0.11", logger, imGuiService, uiColorSheet)
    {
        this.DefaultValue = 60;
    }
}
public class TooltipIngredientPatchTooltipColorSetting : GenericGameColorSetting
{
    public TooltipIngredientPatchTooltipColorSetting(ILogger<TooltipIngredientPatchTooltipColorSetting> logger, ImGuiService imGuiService, ExcelSheet<UIColor> uiColorSheet) : base("TooltipIngredientPatchTooltipColor", "文字颜色", "启用“材料版本”时，所添加文字的颜色。", null, "1.12.0.12", logger, imGuiService, uiColorSheet)
    {
        this.DefaultValue = 540;
    }
}
public class TooltipGlamourReadySetColorSetting : GenericGameColorSetting
{
    public TooltipGlamourReadySetColorSetting(ILogger<TooltipGlamourReadySetColorSetting> logger, ImGuiService imGuiService, ExcelSheet<UIColor> uiColorSheet) : base("TooltipGlamourReadySetColor", "文字颜色", "启用“整套幻化”时，所添加文字的颜色。", null, "1.12.0.0", logger, imGuiService, uiColorSheet)
    {
        this.DefaultValue = 500;
    }
}

public class TooltipGlamourReadySetAcquiredColorSetting : GenericGameColorSetting
{
    public TooltipGlamourReadySetAcquiredColorSetting(ILogger<TooltipGlamourReadySetAcquiredColorSetting> logger, ImGuiService imGuiService, ExcelSheet<UIColor> uiColorSheet) : base("TooltipGlamourReadySetAcquiredColor", "已获取物品颜色", "详细模式下，已获取物品的显示颜色。", null, "1.13.0.0", logger, imGuiService, uiColorSheet)
    {
        this.DefaultValue = 45;
    }
}
public class TooltipGlamourReadySetNotAcquiredColorSetting : GenericGameColorSetting
{
    public TooltipGlamourReadySetNotAcquiredColorSetting(ILogger<TooltipGlamourReadySetNotAcquiredColorSetting> logger, ImGuiService imGuiService, ExcelSheet<UIColor> uiColorSheet) : base("TooltipGlamourReadySetNotAcquiredColor", "未获取物品颜色", "详细模式下，尚未获取物品的显示颜色。", null, "1.13.0.0", logger, imGuiService, uiColorSheet)
    {
        this.DefaultValue = 17;
    }
}
public class TooltipCofferLootColorSetting : GenericGameColorSetting
{
    public TooltipCofferLootColorSetting(ILogger<TooltipCofferLootColorSetting> logger, ImGuiService imGuiService, ExcelSheet<UIColor> uiColorSheet) : base("TooltipCofferLootColor", "文字颜色", "启用“宝箱内容”时，所添加文字的颜色。", null, "1.12.0.0", logger, imGuiService, uiColorSheet)
    {
        this.DefaultValue = 541;
    }
}
public class TooltipCuratedListsColorSetting : GenericGameColorSetting
{
    public TooltipCuratedListsColorSetting(ILogger<TooltipCuratedListsColorSetting> logger, ImGuiService imGuiService, ExcelSheet<UIColor> uiColorSheet) : base("TooltipCuratedListsColor", "文字颜色", "启用“精选清单”时，所添加文字的颜色。", null, "15.0.12", logger, imGuiService, uiColorSheet)
    {
        this.DefaultValue = 535;
    }
}