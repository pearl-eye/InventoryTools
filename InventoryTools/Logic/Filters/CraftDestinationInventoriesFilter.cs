using System.Collections.Generic;
using CriticalCommonLib.Models;
using InventoryTools.Logic.Editors;
using InventoryTools.Logic.Filters.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters;

public class CraftDestinationInventoriesFilter : InventoryScopeFilter
{
    public CraftDestinationInventoriesFilter(InventoryScopePicker scopePicker, ILogger<CraftDestinationInventoriesFilter> logger, ImGuiService imGuiService) : base(scopePicker, logger, imGuiService)
    {
    }

    public override string Key { get; set; } = "CraftDestinationInventories";
    public override string Name { get; set; } = "取回目标背包";

    public override string HelpText { get; set; } =
        "将“取回来源背包”中的材料整理到哪些背包？";

    public override FilterCategory FilterCategory { get; set; } = FilterCategory.Inventories;

    public override List<InventorySearchScope>? DefaultValue { get; set; } = null;

    public override FilterType AvailableIn { get; set; } = FilterType.CraftFilter;
    public override List<InventorySearchScope>? GenerateDefaultScope()
    {
        return new List<InventorySearchScope>()
        {
            new InventorySearchScope() { ActiveCharacter = true, Categories = [InventoryCategory.CharacterBags] }
        };
    }

    public override int Order { get; set; } = -2;
}