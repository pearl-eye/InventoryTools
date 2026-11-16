using System;
using LuminaSupplemental.Excel.Model;

namespace InventoryTools.Localizers;

public class RelicWeaponTypeLocalizer : ILocalizer<RelicWeaponType>
{
    public string Format(RelicWeaponType relicWeaponType)
    {
        switch (relicWeaponType)
        {
            case RelicWeaponType.Unknown:
                return "未知";
            case RelicWeaponType.ZodiacBase:
                return "初始";
            case RelicWeaponType.ZodiacZenith:
                return "天极";
            case RelicWeaponType.ZodiacAtma:
                return "天命";
            case RelicWeaponType.ZodiacAnimus:
                return "天书";
            case RelicWeaponType.ZodiacNovus:
                return "新星";
            case RelicWeaponType.ZodiacNexus:
                return "镇魂";
            case RelicWeaponType.ZodiacZodiac:
                return "黄道";
            case RelicWeaponType.ZodiacZeta:
                return "本我";
            case RelicWeaponType.AnimaAnimated:
                return "元灵";
            case RelicWeaponType.AnimaAwoken:
                return "觉醒";
            case RelicWeaponType.AnimaAnima:
                return "元灵";
            case RelicWeaponType.AnimaHyperconductive:
                return "超导";
            case RelicWeaponType.AnimaReconditioned:
                return "改良";
            case RelicWeaponType.AnimaSharpened:
                return "锋利";
            case RelicWeaponType.AnimaComplete:
                return "完成";
            case RelicWeaponType.AnimaLux:
                return "凝光";
            case RelicWeaponType.EurekanAntiquated:
                return "古旧";
            case RelicWeaponType.EurekanBase:
                return "初始";
            case RelicWeaponType.EurekanBase1:
                return "初始+1";
            case RelicWeaponType.EurekanBase2:
                return "初始+2";
            case RelicWeaponType.EurekanAnemos:
                return "常风";
            case RelicWeaponType.EurekanPagos:
                return "涌冰";
            case RelicWeaponType.EurekanPagos1:
                return "涌冰+1";
            case RelicWeaponType.EurekanElemental:
                return "元素";
            case RelicWeaponType.EurekanElemental1:
                return "元素+1";
            case RelicWeaponType.EurekanElemental2:
                return "元素+2";
            case RelicWeaponType.EurekanPyros:
                return "火焰";
            case RelicWeaponType.EurekanHydatos:
                return "丰水";
            case RelicWeaponType.EurekanHydatos1:
                return "丰水+1";
            case RelicWeaponType.EurekanBaseEureka:
                return "初始";
            case RelicWeaponType.EurekanEureka:
                return "优雷卡";
            case RelicWeaponType.EurekanPhyseos:
                return "最终";
            case RelicWeaponType.ResistanceResistance:
                return "义军";
            case RelicWeaponType.ResistanceAugmentedResistance:
                return "改良型义军";
            case RelicWeaponType.ResistanceRecollection:
                return "记忆";
            case RelicWeaponType.ResistanceLawsOrder:
                return "裁判";
            case RelicWeaponType.ResistanceAugmentedLawsOrder:
                return "改良型裁判";
            case RelicWeaponType.ResistanceBlades:
                return "女王之刃";
            case RelicWeaponType.MandervilleManderville:
                return "曼德维尔";
            case RelicWeaponType.MandervilleAmazing:
                return "惊异";
            case RelicWeaponType.MandervilleMajestic:
                return "威严";
            case RelicWeaponType.MandervilleMandervillous:
                return  "盈满";
            case RelicWeaponType.PhantomPenumbrae:
                return "半影";
            case RelicWeaponType.PhantomUmbrae:
                return "本影";
            case RelicWeaponType.PhantomObscurum:
                return "黯影";
        }

        return relicWeaponType.ToString();
    }
}