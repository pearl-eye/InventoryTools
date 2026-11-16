using InventoryTools.Logic;

namespace InventoryTools.Lists;

/// <summary>Localizes known legacy default names without changing saved list identities.</summary>
public static class ListNameLocalizer
{
    public static string FormatImGuiLabel(string label, string key) => label + "###list_" + key;

    public static string Format(string? name, FilterType type, bool isDefaultCraftList = false)
    {
        return (type, name) switch
        {
            (FilterType.SearchFilter, "All") => "全部",
            (FilterType.SearchFilter, "Character") => "角色",
            (FilterType.GameItemFilter, "Favourites") => "收藏",
            (FilterType.SearchFilter, "Free Company") => "部队",
            (FilterType.GameItemFilter, "Full Item Catalog") => "完整物品目录",
            (FilterType.HistoryFilter, "History") => "历史记录",
            (FilterType.SearchFilter, "Housing") => "房屋",
            (FilterType.SearchFilter, "Retainers") => "雇员",
            (FilterType.CraftFilter, "Default Craft List") when isDefaultCraftList => "默认制作清单",
            (FilterType.CraftFilter, "Craft List") => "制作清单",
            (FilterType.SearchFilter, "100 gil or less") => "100金币及以下",
            (FilterType.SortingFilter, "Duplicate Items" or "Duplicated items") => "重复物品",
            (FilterType.SortingFilter, "Material clean-up") => "整理材料",
            _ => name ?? string.Empty
        };
    }
}
