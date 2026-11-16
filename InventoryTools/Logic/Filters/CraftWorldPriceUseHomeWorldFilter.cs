using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Models;

using InventoryTools.Logic.Filters.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters;

public class CraftWorldPriceUseHomeWorldFilter : BooleanFilter
{
    public CraftWorldPriceUseHomeWorldFilter(ILogger<CraftWorldPriceUseHomeWorldFilter> logger, ImGuiService imGuiService) : base(logger, imGuiService)
    {
    }

    public override string Key { get; set; } = "CraftWorldPriceUseHomeWorld";
    public override string Name { get; set; } = "使用原始服务器？";
    public override string HelpText { get; set; } = "是否从当前角色的原始服务器获取价格？";
    public override FilterCategory FilterCategory { get; set; } = FilterCategory.WorldPricePreference;
    public override FilterType AvailableIn { get; set; } = FilterType.CraftFilter;
    public override bool? DefaultValue { get; set; } = true;
    public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
    {
        return null;
    }

    public override bool? FilterItem(FilterConfiguration configuration, ItemRow item)
    {
        return null;
    }
}