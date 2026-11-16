using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Models;
using InventoryTools.Logic.GenericFilters;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters;

public class StackSizeFilter : GenericIntegerFilter
{
    public StackSizeFilter(ILogger<StackSizeFilter> logger, ImGuiService imGuiService) : base("StackSize", "堆叠上限", "此物品每组的最大数量。", FilterCategory.Basic, item => (int?)item.Item.Base.StackSize, item => (int?)item.Base.StackSize, logger, imGuiService)
    {
    }
}
