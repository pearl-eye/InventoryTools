using System.Collections.Generic;
using CriticalCommonLib.Models;
using InventoryTools.Logic.Editors;
using InventoryTools.Logic.Filters.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters;

public class CraftStagingAreaFilter : InventoryScopeFilter
{
    public CraftStagingAreaFilter(InventoryScopePicker scopePicker, ILogger<CraftStagingAreaFilter> logger, ImGuiService imGuiService) : base(scopePicker, logger, imGuiService)
    {
    }

    public override string Key { get; set; } = "CraftStagingArea";
    public override string Name { get; set; } = "备料区";

    public override string HelpText { get; set; } =
        "备料区中的物品视为角色已持有。默认包括当前角色的背包、水晶和货币，也可加入陆行鸟鞍囊等其他背包。";

    public override FilterCategory FilterCategory { get; set; } = FilterCategory.Inventories;

    public override List<InventorySearchScope>? DefaultValue { get; set; } = null;

    public override FilterType AvailableIn { get; set; } = FilterType.CraftFilter;
    public override List<InventorySearchScope>? GenerateDefaultScope()
    {
        return new List<InventorySearchScope>()
        {
            new InventorySearchScope() { ActiveCharacter = true, CharacterTypes = [CharacterType.Character], Categories = [InventoryCategory.CharacterBags, InventoryCategory.Currency, InventoryCategory.Crystals] }
        };
    }

    public override int Order { get; set; } = -1;
}