using System;
using System.Collections.Generic;
using System.Linq;
using AllaganLib.GameSheets.Model;
using CriticalCommonLib.Enums;
using CriticalCommonLib.Models;

namespace CriticalCommonLib.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<TEnum> GetFlags<TEnum>(this TEnum enumValue)
            where TEnum : Enum
        {
            return EnumUtil.GetFlags<TEnum>().Where(ev => enumValue.HasFlag(ev));
        }

        public static string FormattedName(this CharacterSex characterSex)
        {
            switch (characterSex)
            {
                case CharacterSex.Both:
                    return "两者";
                case CharacterSex.Either:
                    return "任一";
                case CharacterSex.Female:
                    return "女性";
                case CharacterSex.Male:
                    return "男性";
                case CharacterSex.FemaleOnly:
                    return "仅女性";
                case CharacterSex.MaleOnly:
                    return "仅男性";
                case CharacterSex.NotApplicable:
                    return "不适用";
            }

            return "未知";
        }
        public static string FormattedName(this CharacterRace characterRace)
        {
            switch (characterRace)
            {
                case CharacterRace.Any:
                    return "任意";
                case CharacterRace.Hyur:
                    return "人族";
                case CharacterRace.Elezen:
                    return "精灵族";
                case CharacterRace.Lalafell:
                    return "拉拉菲尔族";
                case CharacterRace.Miqote:
                    return "猫魅族";
                case CharacterRace.Roegadyn:
                    return "鲁加族";
                case CharacterRace.Viera:
                    return "维埃拉族";
                case CharacterRace.AuRa:
                    return "敖龙族";
                case CharacterRace.None:
                    return "无";
            }

            return "不适用";
        }

        public static List<InventoryType> GetTypes(this InventoryCategory category)
        {
            switch (category)
            {
                case InventoryCategory.CharacterBags:
                    return new List<InventoryType>()
                        {InventoryType.Bag0, InventoryType.Bag1, InventoryType.Bag2, InventoryType.Bag3};
                case InventoryCategory.RetainerBags:
                    return new List<InventoryType>()
                        {InventoryType.RetainerBag0, InventoryType.RetainerBag1, InventoryType.RetainerBag2, InventoryType.RetainerBag3, InventoryType.RetainerBag4, InventoryType.RetainerBag5, InventoryType.RetainerBag6};
                case InventoryCategory.Armoire:
                    return new List<InventoryType>()
                        {InventoryType.Armoire};
                case InventoryCategory.Crystals:
                    return new List<InventoryType>()
                        {InventoryType.Crystal,InventoryType.RetainerCrystal, InventoryType.FreeCompanyCrystal};
                case InventoryCategory.Currency:
                    return new List<InventoryType>()
                        {InventoryType.Currency,InventoryType.FreeCompanyGil, InventoryType.RetainerGil, InventoryType.FreeCompanyCurrency};
                case InventoryCategory.CharacterEquipped:
                    return new List<InventoryType>()
                        {InventoryType.GearSet0};
                case InventoryCategory.CharacterArmoryChest:
                    return new List<InventoryType>()
                        {InventoryType.ArmoryBody, InventoryType.ArmoryEar, InventoryType.ArmoryFeet, InventoryType.ArmoryHand, InventoryType.ArmoryHead, InventoryType.ArmoryLegs, InventoryType.ArmoryMain, InventoryType.ArmoryNeck, InventoryType.ArmoryOff, InventoryType.ArmoryRing, InventoryType.ArmorySoulCrystal, InventoryType.ArmoryWaist, InventoryType.ArmoryWrist};
                case InventoryCategory.GlamourChest:
                    return new List<InventoryType>()
                        {InventoryType.GlamourChest};
                case InventoryCategory.RetainerEquipped:
                    return new List<InventoryType>()
                        {InventoryType.RetainerEquippedGear};
                case InventoryCategory.RetainerMarket:
                    return new List<InventoryType>()
                        {InventoryType.RetainerMarket};
                case InventoryCategory.CharacterSaddleBags:
                    return new List<InventoryType>()
                        {InventoryType.SaddleBag0,InventoryType.SaddleBag1};
                case InventoryCategory.CharacterPremiumSaddleBags:
                    return new List<InventoryType>()
                        {InventoryType.PremiumSaddleBag0,InventoryType.PremiumSaddleBag1};
                case InventoryCategory.FreeCompanyBags:
                    return new List<InventoryType>()
                        {InventoryType.FreeCompanyBag0,InventoryType.FreeCompanyBag1,InventoryType.FreeCompanyBag2,InventoryType.FreeCompanyBag3,InventoryType.FreeCompanyBag4,InventoryType.FreeCompanyBag5,InventoryType.FreeCompanyBag6,InventoryType.FreeCompanyBag7,InventoryType.FreeCompanyBag8,InventoryType.FreeCompanyBag9,InventoryType.FreeCompanyBag10};
                case InventoryCategory.HousingInteriorItems:
                    return new List<InventoryType>()
                        {
                            InventoryType.HousingInteriorPlacedItems1, InventoryType.HousingInteriorPlacedItems2,
                            InventoryType.HousingInteriorPlacedItems3, InventoryType.HousingInteriorPlacedItems4,
                            InventoryType.HousingInteriorPlacedItems5, InventoryType.HousingInteriorPlacedItems6,
                            InventoryType.HousingInteriorPlacedItems7, InventoryType.HousingInteriorPlacedItems8,
                            InventoryType.HousingInteriorPlacedItems9, InventoryType.HousingInteriorPlacedItems10,
                            InventoryType.HousingInteriorPlacedItems11, InventoryType.HousingInteriorPlacedItems12,
                        };
                case InventoryCategory.HousingInteriorStoreroom:
                    return new List<InventoryType>()
                        {
                            InventoryType.HousingInteriorStoreroom1, InventoryType.HousingInteriorStoreroom2,
                            InventoryType.HousingInteriorStoreroom3, InventoryType.HousingInteriorStoreroom4,
                            InventoryType.HousingInteriorStoreroom5, InventoryType.HousingInteriorStoreroom6,
                            InventoryType.HousingInteriorStoreroom7, InventoryType.HousingInteriorStoreroom8,
                            InventoryType.HousingInteriorStoreroom9, InventoryType.HousingInteriorStoreroom10,
                            InventoryType.HousingInteriorStoreroom11,
                        };
                case InventoryCategory.HousingInteriorAppearance:
                    return new List<InventoryType>()
                        {
                            InventoryType.HousingInteriorAppearance
                        };
                case InventoryCategory.HousingExteriorStoreroom:
                    return new List<InventoryType>()
                        {
                            InventoryType.HousingExteriorStoreroom, InventoryType.HousingExteriorStoreroom2,
                        };
                case InventoryCategory.HousingExteriorItems:
                    return new List<InventoryType>()
                        {
                            InventoryType.HousingExteriorPlacedItems, InventoryType.HousingExteriorPlacedItems2,
                        };
                case InventoryCategory.HousingExteriorAppearance:
                    return new List<InventoryType>()
                        {
                            InventoryType.HousingExteriorAppearance
                        };
            }

            return new List<InventoryType>();
        }

        public static bool IsRetainerCategory(this InventoryCategory category)
        {
            return category is InventoryCategory.RetainerBags or InventoryCategory.RetainerEquipped or InventoryCategory
                .RetainerMarket or InventoryCategory.Crystals or InventoryCategory.Currency;
        }

        public static bool IsFreeCompanyCategory(this InventoryCategory category)
        {
            return category is InventoryCategory.FreeCompanyBags or InventoryCategory.Crystals or InventoryCategory.Currency;
        }

        public static bool IsHousingCategory(this InventoryCategory category)
        {
            return category is InventoryCategory.HousingExteriorAppearance or InventoryCategory.HousingExteriorItems or InventoryCategory.HousingExteriorStoreroom or InventoryCategory.HousingInteriorAppearance or InventoryCategory.HousingInteriorItems or InventoryCategory.HousingInteriorStoreroom;
        }

        public static bool IsCharacterCategory(this InventoryCategory category)
        {
            return !IsRetainerCategory(category) && !IsFreeCompanyCategory(category) && !IsHousingCategory(category) || category == InventoryCategory.Crystals || category == InventoryCategory.Currency;
        }

        public static InventoryCategory ToInventoryCategory(this InventoryType type)
        {
            switch (type)
            {
                case InventoryType.Armoire:
                    return InventoryCategory.Armoire;
                case InventoryType.Bag0 :
                    return InventoryCategory.CharacterBags;
                case InventoryType.Bag1 :
                    return InventoryCategory.CharacterBags;
                case InventoryType.Bag2 :
                    return InventoryCategory.CharacterBags;
                case InventoryType.Bag3 :
                    return InventoryCategory.CharacterBags;
                case InventoryType.ArmoryBody :
                    return InventoryCategory.CharacterArmoryChest;
                case InventoryType.ArmoryEar :
                    return InventoryCategory.CharacterArmoryChest;
                case InventoryType.ArmoryFeet :
                    return InventoryCategory.CharacterArmoryChest;
                case InventoryType.ArmoryHand :
                    return InventoryCategory.CharacterArmoryChest;
                case InventoryType.ArmoryHead :
                    return InventoryCategory.CharacterArmoryChest;
                case InventoryType.ArmoryLegs :
                    return InventoryCategory.CharacterArmoryChest;
                case InventoryType.ArmoryMain :
                    return InventoryCategory.CharacterArmoryChest;
                case InventoryType.ArmoryNeck :
                    return InventoryCategory.CharacterArmoryChest;
                case InventoryType.ArmoryOff :
                    return InventoryCategory.CharacterArmoryChest;
                case InventoryType.ArmoryRing :
                    return InventoryCategory.CharacterArmoryChest;
                case InventoryType.ArmoryWaist :
                    return InventoryCategory.CharacterArmoryChest;
                case InventoryType.ArmoryWrist :
                    return InventoryCategory.CharacterArmoryChest;
                case InventoryType.ArmorySoulCrystal :
                    return InventoryCategory.CharacterArmoryChest;
                case InventoryType.RetainerMarket :
                    return InventoryCategory.RetainerMarket;
                case InventoryType.RetainerEquippedGear :
                    return InventoryCategory.RetainerEquipped;
                case InventoryType.GlamourChest :
                    return InventoryCategory.GlamourChest;
                case InventoryType.RetainerBag0 :
                    return InventoryCategory.RetainerBags;
                case InventoryType.RetainerBag1 :
                    return InventoryCategory.RetainerBags;
                case InventoryType.RetainerBag2 :
                    return InventoryCategory.RetainerBags;
                case InventoryType.RetainerBag3 :
                    return InventoryCategory.RetainerBags;
                case InventoryType.RetainerBag4 :
                    return InventoryCategory.RetainerBags;
                case InventoryType.RetainerBag5 :
                    return InventoryCategory.RetainerBags;
                case InventoryType.SaddleBag0 :
                    return InventoryCategory.CharacterSaddleBags;
                case InventoryType.SaddleBag1 :
                    return InventoryCategory.CharacterSaddleBags;
                case InventoryType.PremiumSaddleBag0 :
                    return InventoryCategory.CharacterPremiumSaddleBags;
                case InventoryType.PremiumSaddleBag1 :
                    return InventoryCategory.CharacterPremiumSaddleBags;
                case InventoryType.FreeCompanyBag0 :
                    return InventoryCategory.FreeCompanyBags;
                case InventoryType.FreeCompanyBag1 :
                    return InventoryCategory.FreeCompanyBags;
                case InventoryType.FreeCompanyBag2 :
                    return InventoryCategory.FreeCompanyBags;
                case InventoryType.FreeCompanyBag3 :
                    return InventoryCategory.FreeCompanyBags;
                case InventoryType.FreeCompanyBag4 :
                    return InventoryCategory.FreeCompanyBags;
                case InventoryType.FreeCompanyBag5 :
                    return InventoryCategory.FreeCompanyBags;
                case InventoryType.FreeCompanyBag6 :
                    return InventoryCategory.FreeCompanyBags;
                case InventoryType.FreeCompanyBag7 :
                    return InventoryCategory.FreeCompanyBags;
                case InventoryType.FreeCompanyBag8 :
                    return InventoryCategory.FreeCompanyBags;
                case InventoryType.FreeCompanyBag9 :
                    return InventoryCategory.FreeCompanyBags;
                case InventoryType.FreeCompanyBag10 :
                    return InventoryCategory.FreeCompanyBags;
                case InventoryType.FreeCompanyCrystal :
                    return InventoryCategory.FreeCompanyBags;
                case InventoryType.HousingInteriorStoreroom1 :
                case InventoryType.HousingInteriorStoreroom2 :
                case InventoryType.HousingInteriorStoreroom3 :
                case InventoryType.HousingInteriorStoreroom4 :
                case InventoryType.HousingInteriorStoreroom5 :
                case InventoryType.HousingInteriorStoreroom6 :
                case InventoryType.HousingInteriorStoreroom7 :
                case InventoryType.HousingInteriorStoreroom8 :
                case InventoryType.HousingInteriorStoreroom9 :
                case InventoryType.HousingInteriorStoreroom10 :
                case InventoryType.HousingInteriorStoreroom11 :
                    return InventoryCategory.HousingInteriorStoreroom;
                case InventoryType.HousingExteriorAppearance :
                    return InventoryCategory.HousingExteriorAppearance;
                case InventoryType.HousingExteriorStoreroom :
                case InventoryType.HousingExteriorStoreroom2 :
                    return InventoryCategory.HousingExteriorStoreroom;
                case InventoryType.HousingExteriorPlacedItems :
                case InventoryType.HousingExteriorPlacedItems2 :
                    return InventoryCategory.HousingExteriorItems;
                case InventoryType.HousingInteriorPlacedItems1 :
                case InventoryType.HousingInteriorPlacedItems2 :
                case InventoryType.HousingInteriorPlacedItems3 :
                case InventoryType.HousingInteriorPlacedItems4 :
                case InventoryType.HousingInteriorPlacedItems5 :
                case InventoryType.HousingInteriorPlacedItems6 :
                case InventoryType.HousingInteriorPlacedItems7 :
                case InventoryType.HousingInteriorPlacedItems8 :
                case InventoryType.HousingInteriorPlacedItems9 :
                case InventoryType.HousingInteriorPlacedItems10 :
                case InventoryType.HousingInteriorPlacedItems11 :
                case InventoryType.HousingInteriorPlacedItems12 :
                    return InventoryCategory.HousingInteriorItems;
                case InventoryType.HousingInteriorAppearance :
                    return InventoryCategory.HousingInteriorAppearance;
                case InventoryType.RetainerGil :
                    return InventoryCategory.Currency;
                case InventoryType.Currency :
                    return InventoryCategory.Currency;
                case InventoryType.FreeCompanyGil :
                    return InventoryCategory.FreeCompanyBags;
                case InventoryType.FreeCompanyCurrency :
                    return InventoryCategory.FreeCompanyBags;
                case InventoryType.Crystal :
                    return InventoryCategory.Crystals;
                case InventoryType.RetainerCrystal :
                    return InventoryCategory.Crystals;
            }
            return InventoryCategory.Other;
        }
        public static string FormattedName(this InventoryCategory category)
        {
            switch (category)
            {
                case InventoryCategory.CharacterBags:
                    return "背包";
                case InventoryCategory.CharacterSaddleBags:
                    return "陆行鸟鞍囊";
                case InventoryCategory.CharacterPremiumSaddleBags:
                    return "陆行鸟鞍囊（额外）";
                case InventoryCategory.FreeCompanyBags:
                    return "部队储物柜";
                case InventoryCategory.CharacterArmoryChest:
                    return "兵装库";
                case InventoryCategory.GlamourChest:
                    return "投影台";
                case InventoryCategory.CharacterEquipped:
                    return "已装备";
                case InventoryCategory.Armoire:
                    return "收藏柜";
                case InventoryCategory.RetainerBags:
                    return "背包";
                case InventoryCategory.RetainerMarket:
                    return "市场";
                case InventoryCategory.Currency:
                    return "货币";
                case InventoryCategory.Crystals:
                    return "水晶";
                case InventoryCategory.HousingExteriorAppearance:
                    return "房屋外装";
                case InventoryCategory.HousingExteriorItems:
                    return "庭具";
                case InventoryCategory.HousingExteriorStoreroom:
                    return "庭具仓库";
                case InventoryCategory.HousingInteriorAppearance:
                    return "房屋内装";
                case InventoryCategory.HousingInteriorItems:
                    return "家具";
                case InventoryCategory.HousingInteriorStoreroom:
                    return "家具仓库";
                case InventoryCategory.RetainerEquipped:
                    return "已装备";
            }

            return category.ToString();
        }

        public static string FormattedDetailedName(this InventoryCategory category)
        {
            switch (category)
            {
                case InventoryCategory.CharacterBags:
                    return "角色背包";
                case InventoryCategory.CharacterSaddleBags:
                    return "陆行鸟鞍囊";
                case InventoryCategory.CharacterPremiumSaddleBags:
                    return "陆行鸟鞍囊（额外）";
                case InventoryCategory.FreeCompanyBags:
                    return "部队储物柜";
                case InventoryCategory.CharacterArmoryChest:
                    return "兵装库";
                case InventoryCategory.GlamourChest:
                    return "投影台";
                case InventoryCategory.CharacterEquipped:
                    return "角色装备";
                case InventoryCategory.Armoire:
                    return "收藏柜";
                case InventoryCategory.RetainerBags:
                    return "雇员背包";
                case InventoryCategory.RetainerMarket:
                    return "雇员出售栏";
                case InventoryCategory.Currency:
                    return "货币";
                case InventoryCategory.Crystals:
                    return "水晶";
                case InventoryCategory.HousingExteriorAppearance:
                    return "房屋外装";
                case InventoryCategory.HousingExteriorItems:
                    return "庭具";
                case InventoryCategory.HousingExteriorStoreroom:
                    return "庭具仓库";
                case InventoryCategory.HousingInteriorAppearance:
                    return "房屋内装";
                case InventoryCategory.HousingInteriorItems:
                    return "家具";
                case InventoryCategory.HousingInteriorStoreroom:
                    return "家具仓库";
                case InventoryCategory.RetainerEquipped:
                    return "雇员装备";
            }

            return category.ToString();
        }

        public static string FormattedName(this InventoryType type)
        {
            switch (type)
            {
                case InventoryType.Bag0:
                    return "背包 - 1";
                case InventoryType.Bag1:
                    return "背包 - 2";
                case InventoryType.Bag2:
                    return "背包 - 3";
                case InventoryType.Bag3:
                    return "背包 - 4";
                case InventoryType.GearSet0:
                    return "装备套装 - 1";
                case InventoryType.GearSet1:
                    return "装备套装 - 2";
                case InventoryType.Currency:
                    return "货币";
                case InventoryType.Crystal:
                    return "水晶";
                case InventoryType.Mail:
                    return "邮件";
                case InventoryType.KeyItem:
                    return "任务道具";
                case InventoryType.HandIn:
                    return "上交物品";
                case InventoryType.DamagedGear:
                    return "损坏装备";
                case InventoryType.UNKNOWN_2008:
                    break;
                case InventoryType.Examine:
                    return "查看";
                case InventoryType.Armoire:
                    return "收藏柜";
                case InventoryType.GlamourChest:
                    return "投影台";
                case InventoryType.FreeCompanyCurrency:
                    return "部队货币";
                case InventoryType.ArmoryOff:
                    return "兵装库 - 副手";
                case InventoryType.ArmoryHead:
                    return "兵装库 - 头部";
                case InventoryType.ArmoryBody:
                    return "兵装库 - 身体";
                case InventoryType.ArmoryHand:
                    return "兵装库 - 手部";
                case InventoryType.ArmoryWaist:
                    return "兵装库 - 腰部";
                case InventoryType.ArmoryLegs:
                    return "兵装库 - 腿部";
                case InventoryType.ArmoryFeet:
                    return "兵装库 - 脚部";
                case InventoryType.ArmoryEar:
                    return "兵装库 - 耳部";
                case InventoryType.ArmoryNeck:
                    return "兵装库 - 颈部";
                case InventoryType.ArmoryWrist:
                    return "兵装库 - 腕部";
                case InventoryType.ArmoryRing:
                    return "兵装库 - 戒指";
                case InventoryType.ArmorySoulCrystal:
                    return "兵装库 - 灵魂水晶";
                case InventoryType.ArmoryMain:
                    return "兵装库 - 主手";
                case InventoryType.SaddleBag0:
                    return "陆行鸟鞍囊 - 1";
                case InventoryType.SaddleBag1:
                    return "陆行鸟鞍囊 - 2";
                case InventoryType.PremiumSaddleBag0:
                    return "陆行鸟鞍囊（额外） - 1";
                case InventoryType.PremiumSaddleBag1:
                    return "陆行鸟鞍囊（额外） - 2";
                case InventoryType.RetainerBag0:
                    return "雇员背包 - 1";
                case InventoryType.RetainerBag1:
                    return "雇员背包 - 2";
                case InventoryType.RetainerBag2:
                    return "雇员背包 - 3";
                case InventoryType.RetainerBag3:
                    return "雇员背包 - 4";
                case InventoryType.RetainerBag4:
                    return "雇员背包 - 5";
                case InventoryType.RetainerBag5:
                    return "雇员背包 - 6";
                case InventoryType.RetainerBag6:
                    return "雇员背包 - 7";
                case InventoryType.RetainerEquippedGear:
                    return "雇员装备";
                case InventoryType.RetainerGil:
                    return "雇员金币";
                case InventoryType.RetainerCrystal:
                    return "雇员水晶";
                case InventoryType.RetainerMarket:
                    return "雇员出售栏";
                case InventoryType.FreeCompanyBag0:
                    return "部队储物柜 - 1";
                case InventoryType.FreeCompanyBag1:
                    return "部队储物柜 - 2";
                case InventoryType.FreeCompanyBag2:
                    return "部队储物柜 - 3";
                case InventoryType.FreeCompanyBag3:
                    return "部队储物柜 - 4";
                case InventoryType.FreeCompanyBag4:
                    return "部队储物柜 - 5";
                case InventoryType.FreeCompanyBag5:
                    return "部队储物柜 - 6";
                case InventoryType.FreeCompanyBag6:
                    return "部队储物柜 - 7";
                case InventoryType.FreeCompanyBag7:
                    return "部队储物柜 - 8";
                case InventoryType.FreeCompanyBag8:
                    return "部队储物柜 - 9";
                case InventoryType.FreeCompanyBag9:
                    return "部队储物柜 - 10";
                case InventoryType.FreeCompanyBag10:
                    return "部队储物柜 - 11";
                case InventoryType.FreeCompanyGil:
                    return "部队储物柜 - 金币";
                case InventoryType.FreeCompanyCrystal:
                    return "部队储物柜 - 水晶";
                case InventoryType.HousingInteriorAppearance:
                    return "房屋 - 内装";
                case InventoryType.HousingInteriorPlacedItems1:
                    return "房屋 - 已放置家具 - 1";
                case InventoryType.HousingInteriorPlacedItems2:
                    return "房屋 - 已放置家具 - 2";
                case InventoryType.HousingInteriorPlacedItems3:
                    return "房屋 - 已放置家具 - 3";
                case InventoryType.HousingInteriorPlacedItems4:
                    return "房屋 - 已放置家具 - 4";
                case InventoryType.HousingInteriorPlacedItems5:
                    return "房屋 - 已放置家具 - 5";
                case InventoryType.HousingInteriorPlacedItems6:
                    return "房屋 - 已放置家具 - 6";
                case InventoryType.HousingInteriorPlacedItems7:
                    return "房屋 - 已放置家具 - 7";
                case InventoryType.HousingInteriorPlacedItems8:
                    return "房屋 - 已放置家具 - 8";
                case InventoryType.HousingInteriorPlacedItems9:
                    return "房屋 - 已放置家具 - 9";
                case InventoryType.HousingInteriorPlacedItems10:
                    return "房屋 - 已放置家具 - 10";
                case InventoryType.HousingInteriorPlacedItems11:
                    return "房屋 - 已放置家具 - 11";
                case InventoryType.HousingInteriorPlacedItems12:
                    return "房屋 - 已放置家具 - 12";
                case InventoryType.HousingInteriorStoreroom1:
                    return "房屋 - 家具仓库 - 1";
                case InventoryType.HousingInteriorStoreroom2:
                    return "房屋 - 家具仓库 - 2";
                case InventoryType.HousingInteriorStoreroom3:
                    return "房屋 - 家具仓库 - 3";
                case InventoryType.HousingInteriorStoreroom4:
                    return "房屋 - 家具仓库 - 4";
                case InventoryType.HousingInteriorStoreroom5:
                    return "房屋 - 家具仓库 - 5";
                case InventoryType.HousingInteriorStoreroom6:
                    return "房屋 - 家具仓库 - 6";
                case InventoryType.HousingInteriorStoreroom7:
                    return "房屋 - 家具仓库 - 7";
                case InventoryType.HousingInteriorStoreroom8:
                    return "房屋 - 家具仓库 - 8";
                case InventoryType.HousingInteriorStoreroom9:
                    return "房屋 - 家具仓库 - 9";
                case InventoryType.HousingInteriorStoreroom10:
                    return "房屋 - 家具仓库 - 10";
                case InventoryType.HousingInteriorStoreroom11:
                    return "房屋 - 家具仓库 - 11";
                case InventoryType.HousingExteriorAppearance:
                    return "房屋 - 外装";
                case InventoryType.HousingExteriorPlacedItems:
                    return "房屋 - 已放置庭具";
                case InventoryType.HousingExteriorPlacedItems2:
                    return "房屋 - 已放置庭具 - 2";
                case InventoryType.HousingExteriorStoreroom:
                    return "房屋 - 庭具仓库";
                case InventoryType.HousingExteriorStoreroom2:
                    return "房屋 - 庭具仓库 - 2";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return type.ToString();
        }
        public static string FormattedName(this InventoryCategory? category)
        {
            if (category.HasValue)
            {
                return FormattedName(category.Value);
            }

            return "未知";
        }
        public static string FormattedName(this InventoryChangeReason reason)
        {
            switch (reason)
            {
                case InventoryChangeReason.Added:
                    return "新增";
                case InventoryChangeReason.Removed:
                    return "移除";
                case InventoryChangeReason.Moved:
                    return "移动";
                case InventoryChangeReason.ConditionChanged:
                    return "耐久度变更";
                case InventoryChangeReason.FlagsChanged:
                    return "物品品质变更";
                case InventoryChangeReason.GlamourChanged:
                    return "投影变更";
                case InventoryChangeReason.MateriaChanged:
                    return "魔晶石变更";
                case InventoryChangeReason.QuantityChanged:
                    return "数量变更";
                case InventoryChangeReason.SpiritbondChanged:
                    return "精炼度变更";
                case InventoryChangeReason.StainChanged:
                    return "染色变更";
                case InventoryChangeReason.ItemIdChanged:
                    return "物品变更";
                case InventoryChangeReason.Transferred:
                    return "转移";
                case InventoryChangeReason.MarketPriceChanged:
                    return "市场价格变更";
                case InventoryChangeReason.GearsetsChanged:
                    return "套装变更";
            }
            return "未知";
        }
        public static string FormattedName(this CharacterType characterType)
        {
            switch (characterType)
            {
                case CharacterType.Character:
                    return "角色";
                case CharacterType.Housing:
                    return "房产";
                case CharacterType.Retainer:
                    return "雇员";
                case CharacterType.FreeCompanyChest:
                    return "部队储物柜";
            }
            return "未知";
        }
        public static bool IsApplicable(this InventoryCategory inventoryCategory, CharacterType characterType)
        {
            switch (characterType)
            {
                case CharacterType.Character:
                    return IsCharacterCategory(inventoryCategory);
                case CharacterType.Retainer:
                    return IsRetainerCategory(inventoryCategory);
                case CharacterType.FreeCompanyChest:
                    return IsFreeCompanyCategory(inventoryCategory);
                case CharacterType.Housing:
                    return IsHousingCategory(inventoryCategory);
            }

            return true;
        }
    }
}