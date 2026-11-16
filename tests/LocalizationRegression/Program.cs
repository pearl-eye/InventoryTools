using InventoryTools.Extensions;
using InventoryTools.Lists;
using InventoryTools.Logic;
using InventoryTools.Logic.Filters;

var checks = 0;
void Equal(string actual, string expected, string message)
{
    if (actual != expected)
        throw new InvalidOperationException($"{message}: expected '{expected}', got '{actual}'.");
    checks++;
}

var legacyDefaults = new (string Name, FilterType Type, string Expected)[]
{
    ("All", FilterType.SearchFilter, "全部"),
    ("Character", FilterType.SearchFilter, "角色"),
    ("Favourites", FilterType.GameItemFilter, "收藏"),
    ("Free Company", FilterType.SearchFilter, "部队"),
    ("Full Item Catalog", FilterType.GameItemFilter, "完整物品目录"),
    ("History", FilterType.HistoryFilter, "历史记录"),
    ("Housing", FilterType.SearchFilter, "房屋"),
    ("Retainers", FilterType.SearchFilter, "雇员"),
    ("Craft List", FilterType.CraftFilter, "制作清单"),
    ("100 gil or less", FilterType.SearchFilter, "100金币及以下"),
    ("Duplicate Items", FilterType.SortingFilter, "重复物品"),
    ("Duplicated items", FilterType.SortingFilter, "重复物品"),
    ("Material clean-up", FilterType.SortingFilter, "整理材料")
};
foreach (var item in legacyDefaults)
    Equal(ListNameLocalizer.Format(item.Name, item.Type), item.Expected, "Legacy caption");

Equal(ListNameLocalizer.Format("Default Craft List", FilterType.CraftFilter, true), "默认制作清单", "Default craft template");
Equal(ListNameLocalizer.Format("Default Craft List", FilterType.CraftFilter, false), "Default Craft List", "Do not claim a custom list is the default template");
foreach (var name in new[] { "My materials", "Retainers for crafting", "All jobs", "History 2026", "我的自定义清单", "ALL", "", "Craft List 2" })
    foreach (var type in Enum.GetValues<FilterType>())
        Equal(ListNameLocalizer.Format(name, type), name, "Custom names must be exact passthroughs");
Equal(ListNameLocalizer.Format(null, FilterType.SearchFilter), "", "Missing name");
foreach (var item in legacyDefaults)
    Equal(ListNameLocalizer.Format(item.Name, FilterType.CuratedList), item.Name, "Names in another list type must not be translated");

string IdSuffix(string label) => label[(label.LastIndexOf("###", StringComparison.Ordinal) + 3)..];
var firstLabel = ListNameLocalizer.FormatImGuiLabel("制作清单", "first");
var secondLabel = ListNameLocalizer.FormatImGuiLabel("制作清单", "second");
if (IdSuffix(firstLabel) == IdSuffix(secondLabel))
    throw new InvalidOperationException("Two lists with the same visible name must have distinct widget identities.");
checks++;
Equal(IdSuffix(firstLabel), "list_first", "Widget identity uses the stable list key");
Equal(IdSuffix(ListNameLocalizer.FormatImGuiLabel("Craft List", "first")), IdSuffix(firstLabel), "Translating a label must not alter its stable identity");
Equal(IdSuffix(ListNameLocalizer.FormatImGuiLabel("自定义###标题", "first")), IdSuffix(firstLabel), "Embedded user label identifiers must not override the list key");

foreach (var category in Enum.GetValues<FilterCategory>())
{
    var label = category.FormattedName();
    if (!label.Any(c => c is >= '\u3400' and <= '\u9fff'))
        throw new InvalidOperationException($"Visible filter category is not localized: {category} -> {label}");
    checks++;
}
Console.WriteLine($"Passed {checks} localization checks: legacy labels, custom-name preservation and every filter category.");
