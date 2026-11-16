using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Models;

using InventoryTools.Logic.Filters.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters;

public class CraftIsEphemeralFilter : BooleanFilter
{
    public override string Key { get; set; } = "CraftIsEphemeral";
    public override string Name { get; set; } = "临时清单？";

    public override string HelpText { get; set; } =
        "将此制作清单设为临时清单。勾选后，当所有物品均已完成并删除时，列表会自动删除。仅在制作完成时检查。";

    public override FilterCategory FilterCategory { get; set; } = FilterCategory.Settings;
    public override FilterType AvailableIn { get; set; } = FilterType.CraftFilter;
    public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
    {
        return null;
    }

    public override bool? FilterItem(FilterConfiguration configuration, ItemRow item)
    {
        return null;
    }

    public override bool? CurrentValue(FilterConfiguration configuration)
    {
        return configuration.IsEphemeralCraftList;
    }

    public override void UpdateFilterConfiguration(FilterConfiguration configuration, bool? newValue)
    {
        configuration.IsEphemeralCraftList = newValue ?? false;
    }

    private readonly string[] _choices = new []{"是", "否"};

    public override string[] GetChoices()
    {
        return _choices;
    }

    public override bool? DefaultValue { get; set; } = false;

    public CraftIsEphemeralFilter(ILogger<CraftIsEphemeralFilter> logger, ImGuiService imGuiService) : base(logger, imGuiService)
    {
    }
}