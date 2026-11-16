using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Models;

using InventoryTools.Logic.Filters.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters
{
    public class FilterORFilter : BooleanFilter
    {
        public override string Key { get; set; } = "ORFilter";
        public override string Name { get; set; } = "使用“或”连接筛选条件";

        public override string HelpText { get; set; } =
            "默认使用“且”逐项缩小范围。启用后改为使用“或”，任一筛选条件匹配即可。";

        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Advanced;



        public override bool? CurrentValue(FilterConfiguration configuration)
        {
            return configuration.UseORFiltering;
        }

        public override void UpdateFilterConfiguration(FilterConfiguration configuration, bool? newValue)
        {
            configuration.UseORFiltering = newValue;
        }

        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {
            return null;
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemRow item)
        {
            return null;
        }

        public FilterORFilter(ILogger<FilterORFilter> logger, ImGuiService imGuiService) : base(logger, imGuiService)
        {
        }
    }
}