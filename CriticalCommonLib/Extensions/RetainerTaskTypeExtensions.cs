using AllaganLib.GameSheets.Model;


namespace CriticalCommonLib.Extensions
{
    public static class RetainerTaskTypExtensions
    {
        public static string FormattedName(this RetainerTaskType retainerTaskType)
        {
            switch (retainerTaskType)
            {
                case RetainerTaskType.Botanist:
                    return "园艺";
                case RetainerTaskType.FieldExploration:
                    return "平地探索";
                case RetainerTaskType.HighlandExploration:
                    return "山岳探索";
                case RetainerTaskType.WatersideExploration:
                    return "水边探索";
                case RetainerTaskType.WoodlandExploration:
                    return "林间探索";
                case RetainerTaskType.Fishing:
                    return "钓鱼";
                case RetainerTaskType.Hunting:
                    return "狩猎";
                case RetainerTaskType.Mining:
                    return "采矿";
                case RetainerTaskType.QuickExploration:
                    return "探索委托";
                case RetainerTaskType.Unknown:
                    return "未知";
            }
            return retainerTaskType.ToString();
        }
    }
}