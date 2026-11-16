using System.Collections.Generic;
using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Crafting;
using CriticalCommonLib.Models;

using InventoryTools.Logic.Filters.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters;

public class RetainerRetrieveOrderFilter : ChoiceFilter<RetainerRetrieveOrder>
{
    public override RetainerRetrieveOrder CurrentValue(FilterConfiguration configuration)
    {
        if (configuration.FilterType == FilterType.CraftFilter)
        {
            return configuration.CraftList.RetainerRetrieveOrder;
        }

        return RetainerRetrieveOrder.RetrieveFirst;
    }

    public override FilterType AvailableIn { get; set; } = FilterType.CraftFilter;
    public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
    {
        return null;
    }

    public override bool? FilterItem(FilterConfiguration configuration, ItemRow item)
    {
        return null;
    }

    public override void ResetFilter(FilterConfiguration configuration)
    {
        UpdateFilterConfiguration(configuration, RetainerRetrieveOrder.RetrieveFirst);
    }

    public override void UpdateFilterConfiguration(FilterConfiguration configuration, RetainerRetrieveOrder newValue)
    {
        if (configuration.FilterType == FilterType.CraftFilter)
        {
            configuration.CraftList.RetainerRetrieveOrder = newValue;
            configuration.NotifyConfigurationChange();
        }
    }

    public override string Key { get; set; } = "RetainerRetrieveOrder";
    public override string Name { get; set; } = "雇员取回顺序";
    public override string HelpText { get; set; } = "先取回会要求先从雇员处取出已有材料；后取回会先显示需要采集或购买的缺少材料，再安排取回其余材料。";
    public override FilterCategory FilterCategory { get; set; } = FilterCategory.Settings;
    public override RetainerRetrieveOrder DefaultValue { get; set; } = RetainerRetrieveOrder.RetrieveFirst;
    public override List<RetainerRetrieveOrder> GetChoices(FilterConfiguration configuration)
    {
        return new List<RetainerRetrieveOrder>()
        {
            RetainerRetrieveOrder.RetrieveFirst,
            RetainerRetrieveOrder.RetrieveLast
        };
    }

    public override string GetFormattedChoice(FilterConfiguration filterConfiguration, RetainerRetrieveOrder choice)
    {
        switch (choice)
        {
            case RetainerRetrieveOrder.RetrieveFirst:
                return "先取回";
            case RetainerRetrieveOrder.RetrieveLast:
                return "后取回";
        }
        return "未知";
    }

    public RetainerRetrieveOrderFilter(ILogger<RetainerRetrieveOrderFilter> logger, ImGuiService imGuiService) : base(logger, imGuiService)
    {
    }
}