using FFXIVClientStructs.FFXIV.Client.Game;

namespace InventoryTools.Extensions;

public static class ItemFlagsExtensions
{
    public static string FormattedName(this InventoryItem.ItemFlags flags)
    {
        return flags switch
        {
            InventoryItem.ItemFlags.None => "普通品质",
            InventoryItem.ItemFlags.HighQuality => "优质品质",
            InventoryItem.ItemFlags.CompanyCrestApplied => "已镶嵌部队队徽",
            InventoryItem.ItemFlags.Relic => "古武",
            InventoryItem.ItemFlags.Collectable => "收藏品",
            _ => "无"
        };
    }
}