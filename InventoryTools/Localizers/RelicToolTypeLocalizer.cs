using System;
using LuminaSupplemental.Excel.Model;

namespace InventoryTools.Localizers;

public class RelicToolTypeLocalizer : ILocalizer<RelicToolType>
{
    public string Format(RelicToolType relicToolType)
    {
        switch (relicToolType)
        {
            case RelicToolType.Unknown:
                return "未知";
            case RelicToolType.MastercraftBase:
                return "工匠";
            case RelicToolType.MastercraftSupra:
                return "上品";
            case RelicToolType.MastercraftLucis:
                return "华光";
            case RelicToolType.SkysteelBase:
                return "天钢";
            case RelicToolType.SkysteelBase1:
                return "天钢+1";
            case RelicToolType.SkysteelDragonsung:
                return "龙诗";
            case RelicToolType.SkysteelAugmentedDragonsung:
                return "改良型龙诗";
            case RelicToolType.SkysteelSkysung:
                return "天诗";
            case RelicToolType.SkysteelSkybuilders:
                return "天工";
            case RelicToolType.Resplendent:
                return "辉煌";
            case RelicToolType.SplendorousBase:
                return "卓越";
            case RelicToolType.SplendorousAugmented:
                return "改良型卓越";
            case RelicToolType.SplendorousCrystalline:
                return "水晶";
            case RelicToolType.SplendorousChoraZoiCrystalline:
                return "乔菈水晶";
            case RelicToolType.SplendorousBrilliant:
                return "乔菈卓绝";
            case RelicToolType.SplendorousVrandticVisionary:
                return "诺弗兰特远见";
            case RelicToolType.SplendorousLodestar:
                return "领航星";
            case RelicToolType.CosmicPrototype01:
                return "原型v0.1";
            case RelicToolType.CosmicPrototype02:
                return "原型v0.2";
            case RelicToolType.CosmicPrototype03:
                return "原型v0.3";
            case RelicToolType.CosmicPrototype04:
                return "原型v0.4";
            case RelicToolType.CosmicPrototype05:
                return "原型v0.5";
            case RelicToolType.CosmicPrototype06:
                return "原型v0.6";
            case RelicToolType.CosmicPrototype07:
                return "原型v0.7";
            case RelicToolType.CosmicPrototype08:
                return "原型v0.8";
            case RelicToolType.CosmicCosmic:
                return "宇宙";
            case RelicToolType.CosmicCosmic11:
                return "宇宙v1.1";
            case RelicToolType.CosmicCosmic12:
                return "宇宙v1.2";
            case RelicToolType.CosmicCosmic13:
                return "宇宙v1.3";
            case RelicToolType.CosmicCosmic14:
                return "宇宙v1.4";
            case RelicToolType.CosmicStellar:
                return "太空";
            case RelicToolType.CosmicStellar11:
                return "太空v1.1";
            case RelicToolType.CosmicStellar12:
                return "太空v1.2";
            case RelicToolType.CosmicHypertools:
                return "超空间工具";
        }

        return relicToolType.ToString();
    }
}