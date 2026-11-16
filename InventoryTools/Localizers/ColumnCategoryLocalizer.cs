using InventoryTools.Logic.Columns.Abstract;

namespace InventoryTools.Localizers;

public static class ColumnCategoryLocalizer
{
    public static string Format(ColumnCategory category) => category switch
    {
        ColumnCategory.Unsorted => "未分类",
        ColumnCategory.Basic => "基本",
        ColumnCategory.Stats => "属性",
        ColumnCategory.Desynthesis => "分解",
        ColumnCategory.Inventory => "背包",
        ColumnCategory.Crafting => "制作",
        ColumnCategory.Market => "市场",
        ColumnCategory.Tools => "工具",
        ColumnCategory.Debug => "调试",
        ColumnCategory.History => "历史记录",
        ColumnCategory.Buttons => "操作按钮",
        _ => "其他"
    };
}
