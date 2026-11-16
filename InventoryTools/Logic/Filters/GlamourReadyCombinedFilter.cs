using System;
using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Models;
using InventoryTools.Logic.GenericFilters;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters;

public class GlamourReadyCombinedFilter : GenericBooleanFilter
{
    public GlamourReadyCombinedFilter(ILogger<GlamourReadyCombinedFilter> logger, ImGuiService imGuiService) : base("grCombined", "已合并的幻化套装",
        "此物品是否已在投影台中合并？", FilterCategory.Basic,
        item => item.SortedCategory == InventoryCategory.GlamourChest && item.GlamourId != 0, null, logger, imGuiService)
    {
    }
}