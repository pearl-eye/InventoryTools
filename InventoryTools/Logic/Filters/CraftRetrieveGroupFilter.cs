using System.Collections.Generic;
using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Crafting;
using CriticalCommonLib.Models;

using InventoryTools.Logic.Filters.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters;

public class CraftRetrieveGroupFilter : ChoiceFilter<RetrieveGroupSetting>
{
    public override RetrieveGroupSetting CurrentValue(FilterConfiguration configuration)
    {
        return configuration.CraftList.RetrieveGroupSetting;
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
        configuration.CraftList.SetRetrieveGroupSetting(DefaultValue);
        configuration.NotifyConfigurationChange();
    }

    public override void UpdateFilterConfiguration(FilterConfiguration configuration, RetrieveGroupSetting newValue)
    {
        configuration.CraftList.SetRetrieveGroupSetting(newValue);
        configuration.NotifyConfigurationChange();
    }

    public override string Key { get; set; } = "CraftRetrieveGroupFilter";
    public override string Name { get; set; } = "待取回物品分组方式";

    public override string HelpText { get; set; } =
        "是否对待取回的物品分组？";

    public override FilterCategory FilterCategory { get; set; } = FilterCategory.Settings;
    public override RetrieveGroupSetting DefaultValue { get; set; } = RetrieveGroupSetting.None;
    public override List<RetrieveGroupSetting> GetChoices(FilterConfiguration configuration)
    {
        return new List<RetrieveGroupSetting>()
        {
            RetrieveGroupSetting.None,
            RetrieveGroupSetting.Together
        };
    }

    public override string GetFormattedChoice(FilterConfiguration filterConfiguration, RetrieveGroupSetting choice)
    {
        return choice switch
        {
            RetrieveGroupSetting.None => "不单独分组",
            RetrieveGroupSetting.Together => "合并待取回物品",
            _ => choice.ToString()
        };
    }

    public CraftRetrieveGroupFilter(ILogger<CraftRetrieveGroupFilter> logger, ImGuiService imGuiService) : base(logger, imGuiService)
    {
    }
}