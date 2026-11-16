using CriticalCommonLib.Crafting;

namespace CriticalCommonLib.Extensions;

public static class CraftGroupTypeExtension
{
    public static string FormattedName(this CraftGroupType craftGroupType)
    {
        switch (craftGroupType)
        {
            case CraftGroupType.Crystals:
                return "水晶";
            case CraftGroupType.Currency:
                return "货币";
            case CraftGroupType.Output:
                return "成品";
            case CraftGroupType.Precraft:
                return "半成品";
            case CraftGroupType.EverythingElse:
                return "采集/购买";
            case CraftGroupType.Retrieve:
                return "取出";
            case CraftGroupType.HouseVendors:
                return "房屋商人";
        }

        return "未知";
    }
    public static string FormattedName(this PrecraftGroupSetting precraftGroupSetting)
    {
        switch (precraftGroupSetting)
        {
            case PrecraftGroupSetting.ByDepth:
                return "按制作层级";
            case PrecraftGroupSetting.Together:
                return "合并";
            case PrecraftGroupSetting.ByClass:
                return "按职业";
        }

        return "未知";
    }
}