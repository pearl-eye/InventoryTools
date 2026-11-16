using System;
using LuminaSupplemental.Excel.Model;

namespace InventoryTools.Localizers;

public class RelicWeaponCategoryLocalizer : ILocalizer<RelicWeaponCategory>
{
    public string Format(RelicWeaponCategory instance)
    {
        switch (instance)
        {
            case RelicWeaponCategory.Zodiac:
                return  "古武·黄道武器";
            case RelicWeaponCategory.Anima:
                return  "元灵武器";
            case RelicWeaponCategory.Eurekan:
                return  "优雷卡武器";
            case RelicWeaponCategory.Resistance:
                return  "义军武器";
            case RelicWeaponCategory.Manderville:
                return  "曼德维尔武器";
            case RelicWeaponCategory.Phantom:
                return  "幻境武器";
        }
        return instance.ToString();
    }
}