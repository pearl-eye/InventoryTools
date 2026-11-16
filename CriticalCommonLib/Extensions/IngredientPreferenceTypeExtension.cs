using System;
using System.Collections.Generic;
using AllaganLib.GameSheets.Caches;
using CriticalCommonLib.Crafting;

namespace CriticalCommonLib.Extensions;

public static class IngredientPreferenceTypeExtension
{
    public static HashSet<ItemInfoType> ToItemInfoTypes(this IngredientPreferenceType ingredientPreferenceType)
    {
        switch (ingredientPreferenceType)
        {
            case IngredientPreferenceType.None:
                return [];
            case IngredientPreferenceType.Mining:
                return
                [
                    ItemInfoType.Mining,
                    ItemInfoType.HiddenMining,
                    ItemInfoType.EphemeralMining,
                    ItemInfoType.TimedMining,

                    ItemInfoType.Quarrying,
                    ItemInfoType.HiddenQuarrying,
                    ItemInfoType.EphemeralQuarrying,
                    ItemInfoType.TimedQuarrying
                ];
            case IngredientPreferenceType.Botany:
                return
                [
                    ItemInfoType.Logging,
                    ItemInfoType.HiddenLogging,
                    ItemInfoType.EphemeralLogging,
                    ItemInfoType.TimedLogging,

                    ItemInfoType.Harvesting,
                    ItemInfoType.HiddenHarvesting,
                    ItemInfoType.EphemeralHarvesting,
                    ItemInfoType.TimedHarvesting
                ];
            case IngredientPreferenceType.Fishing:
                return
                [
                    ItemInfoType.Fishing
                ];
            case IngredientPreferenceType.SpearFishing:
                return
                [
                    ItemInfoType.Spearfishing
                ];
            case IngredientPreferenceType.Buy:
                return
                [
                    ItemInfoType.SpecialShop,
                    ItemInfoType.CashShop,
                    ItemInfoType.FateShop,
                    ItemInfoType.GilShop,
                    ItemInfoType.FCShop,
                    ItemInfoType.GCShop,
                    ItemInfoType.CalamitySalvagerShop,
                ];
            case IngredientPreferenceType.Crafting:
                return
                [
                    ItemInfoType.CraftRecipe,
                    ItemInfoType.FreeCompanyCraftRecipe,
                ];
            case IngredientPreferenceType.Marketboard:
                return [];
            case IngredientPreferenceType.Item:
                return
                [
                    ItemInfoType.SpecialShop,
                    ItemInfoType.AnimaShop,
                ];
            case IngredientPreferenceType.Venture:
                return
                [
                    ItemInfoType.BotanyVenture,
                    ItemInfoType.CombatVenture,
                    ItemInfoType.FishingVenture,
                    ItemInfoType.MiningVenture,
                ];
            case IngredientPreferenceType.Reduction:
                return
                [
                    ItemInfoType.Reduction,
                ];
            case IngredientPreferenceType.Duty:
                return
                [
                    ItemInfoType.DungeonBossChest,
                    ItemInfoType.DungeonBossDrop,
                    ItemInfoType.DungeonChest,
                    ItemInfoType.DungeonDrop,
                ];
            case IngredientPreferenceType.ResourceInspection:
                return
                [
                    ItemInfoType.SkybuilderInspection,
                ];
            case IngredientPreferenceType.Gardening:
                return
                [
                    ItemInfoType.Gardening,
                ];
            case IngredientPreferenceType.Desynthesis:
                return
                [
                    ItemInfoType.Desynthesis,
                ];
            case IngredientPreferenceType.Mobs:
                return
                [
                    ItemInfoType.Monster,
                ];
            case IngredientPreferenceType.HouseVendor:
                return
                [
                    ItemInfoType.GilShop,
                ];
            case IngredientPreferenceType.ExplorationVenture:
                return
                [
                    ItemInfoType.BotanyExplorationVenture,
                    ItemInfoType.CombatExplorationVenture,
                    ItemInfoType.FishingExplorationVenture,
                    ItemInfoType.MiningExplorationVenture,
                ];
        }

        return [];
    }
    public static string FormattedName(this IngredientPreferenceType ingredientPreferenceType)
    {
        switch (ingredientPreferenceType)
        {
            case IngredientPreferenceType.Botany:
                return "园艺";
            case IngredientPreferenceType.Buy:
                return "从商店购买";
            case IngredientPreferenceType.HouseVendor:
                return "从房屋商人购买";
            case IngredientPreferenceType.Crafting:
                return "制作";
            case IngredientPreferenceType.Fishing:
                return "钓鱼";
            case IngredientPreferenceType.SpearFishing:
                return "刺鱼";
            case IngredientPreferenceType.Item:
                return "物品";
            case IngredientPreferenceType.Marketboard:
                return "市场布告板";
            case IngredientPreferenceType.Mining:
                return "采矿";
            case IngredientPreferenceType.Venture:
                return "雇员探险";
            case IngredientPreferenceType.Duty:
                return "副本任务";
            case IngredientPreferenceType.ExplorationVenture:
                return "雇员探险（探索）";
            case IngredientPreferenceType.Desynthesis:
                return "分解";
            case IngredientPreferenceType.Reduction:
                return "精选";
            case IngredientPreferenceType.ResourceInspection:
                return "资源勘察";
            case IngredientPreferenceType.Gardening:
                return "园艺种植";
            case IngredientPreferenceType.Mobs:
                return "怪物";
            case IngredientPreferenceType.Empty:
                return "无";
        }

        return ingredientPreferenceType.ToString();
    }
}