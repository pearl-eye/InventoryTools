using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Logic.Settings.Abstract.Generic;
using InventoryTools.Services;
using InventoryTools.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class TooltipDisplayCuratedListsSetting : GenericBooleanSetting
{
    public TooltipDisplayCuratedListsSetting(ILogger<TooltipDisplayCuratedListsSetting> logger,
        ImGuiService imGuiService) : base(
        "TooltipDisplayCuratedLists",
        "精选清单信息",
        "鼠标悬停于物品时，显示哪些精选清单已包含该物品。",
        false,
        "15.0.12",
        logger,
        imGuiService)
    {
    }
}

public class TooltipCuratedListsSetting : MultipleListChoiceSetting
{
    public TooltipCuratedListsSetting(ILogger<TooltipCuratedListsSetting> logger, ImGuiService imGuiService,
        IListService listService) : base(logger, imGuiService, listService)
    {
    }

    public override FilterType? ListFilterType => FilterType.CuratedList;

    public override string Key { get; set; } = "TooltipCuratedLists";
    public override string Name { get; set; } = "要检查的精选清单";

    public override string HelpText { get; set; } =
        "应检查哪些精选清单？未选择时检查全部精选清单。";

    public override string Version => "15.0.12";
}

public class TooltipCuratedListsMatchQualitySetting : GenericBooleanSetting
{
    public TooltipCuratedListsMatchQualitySetting(ILogger<TooltipCuratedListsMatchQualitySetting> logger,
        ImGuiService imGuiService) : base(
        "TooltipCuratedListsMatchQuality",
        "匹配物品品质",
        "悬停物品的品质是否必须与精选清单记录一致？关闭后，NQ物品也会匹配HQ记录，反之亦然。",
        false,
        "15.0.12",
        logger,
        imGuiService)
    {
    }
}