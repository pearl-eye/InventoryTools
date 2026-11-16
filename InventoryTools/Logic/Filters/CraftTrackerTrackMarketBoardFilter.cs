using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Models;
using InventoryTools.Logic.Filters.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters;

public class CraftTrackerTrackMarketBoardFilter : BooleanFilter
{
    public CraftTrackerTrackMarketBoardFilter(ILogger<CraftTrackerTrackMarketBoardFilter> logger, ImGuiService imGuiService) : base(logger, imGuiService)
    {
    }

    public override bool? DefaultValue { get; set; } = true;

    public override FilterType AvailableIn { get; set; } = FilterType.CraftFilter;

    public override string Key { get; set; } = "CraftTrackerTrackMarketBoard";
    public override string Name { get; set; } = "追踪市场购买？";

    public override string HelpText { get; set; } =
        "从市场购买与此清单成品匹配的物品时，减少对应的所需数量。需要激活此制作清单。";

    public override FilterCategory FilterCategory { get; set; } = FilterCategory.CompletionTracking;
    public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
    {
        return null;
    }

    public override bool? FilterItem(FilterConfiguration configuration, ItemRow item)
    {
        return null;
    }
}