using System.Collections.Generic;
using System.Linq;
using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Extensions;
using CriticalCommonLib.Models;
using CriticalCommonLib.Services;
using InventoryTools.Logic.Editors;
using InventoryTools.Logic.Filters.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters
{
    public class DestinationInventoriesFilter : InventoryScopeFilter
    {
        public DestinationInventoriesFilter(ILogger<DestinationInventoriesFilter> logger, InventoryScopePicker scopePicker, ImGuiService imGuiService) : base(scopePicker, logger, imGuiService)
        {
        }
        public override int LabelSize { get; set; } = 240;
        public override string Key { get; set; } = "DestinationInventories";
        public override string Name { get; set; } = "目标背包";
        public override string HelpText { get; set; } =
            "选择整理物品的目标背包。插件会将来源背包中的物品分配到这些目标背包中。下方可查看匹配范围内的背包。";
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Inventories;
        public override List<InventorySearchScope>? DefaultValue { get; set; } = null;
        public override FilterType AvailableIn { get; set; } = FilterType.SortingFilter;

        public override List<InventorySearchScope>? GenerateDefaultScope()
        {
            return null;
        }
    }
}