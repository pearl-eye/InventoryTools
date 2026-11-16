using System;
using CriticalCommonLib.Crafting;

namespace CriticalCommonLib.Extensions
{
    public static class CraftRetainerRetrievalExtension
    {
        public static string FormattedName(this CraftRetainerRetrieval craftRetainerRetrieval)
        {
            switch (craftRetainerRetrieval)
            {
                case CraftRetainerRetrieval.Yes:
                    return "是";
                case CraftRetainerRetrieval.No:
                    return "否";
                case CraftRetainerRetrieval.HqOnly:
                    return "仅 HQ";
                case CraftRetainerRetrieval.NqOnly:
                    return "仅普通品质";
                case CraftRetainerRetrieval.CollectableOnly:
                    return "仅收藏品";
            }
            return craftRetainerRetrieval.ToString();
        }
    }
}