using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Models;
using InventoryTools.Logic.GenericFilters;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters;

public class IsStackableFilter : GenericBooleanFilter
{
    public IsStackableFilter(ILogger<IsStackableFilter> logger, ImGuiService imGuiService) : base("IsStackable", "是否可堆叠？", "此物品是否可堆叠（每组可存放多于1个）？", FilterCategory.Basic, item => item.Item.Base.StackSize > 1, item => item.Base.StackSize > 1, logger, imGuiService)
    {
    }
}
