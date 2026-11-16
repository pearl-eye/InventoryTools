using InventoryTools.Logic.Settings;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.ConfigLayouts;

public class TooltipsLayout : ConfigLayout
{
    public override PageLayout Build()
    {
        return PageGroup("tooltips", "物品提示",
            Page("tooltips/general", "常规",
                Paragraph("Allagan Tools 可以在游戏物品提示中添加额外信息。只有启用物品提示增强后，本节的各项功能才会生效。"),
                Setting<ShowTooltipsSetting>("启用物品提示增强"),
                EnabledBy<ShowTooltipsSetting>(
                    Setting<ImGuiTooltipModeSetting>("在 Allagan Tools 窗口中显示"),
                    Setting<TooltipDisplayHeaderSetting>("在新增内容前显示插件名称"),
                    Setting<TooltipCategoryWhitelistSetting>("仅对这些物品分类生效"),
                    Setting<TooltipCategoryBlacklistSetting>("改为排除以上分类"))),
            Page("tooltips/appearance", "外观",
                Paragraph("Allagan Tools 新增文字的默认样式。各项功能可单独覆盖颜色。"),
                Setting<TooltipColorSetting>("默认文字颜色"),
                Setting<TooltipHeaderLinesSetting>("上方空行数"),
                Setting<TooltipFooterLinesSetting>("下方空行数")),
            Page("tooltips/locations", "物品位置",
                Paragraph("显示已持有此物品的位置和数量。"),
                Setting<TooltipDisplayAmountOwnedSetting>("显示持有位置"),
                EnabledBy<TooltipDisplayAmountOwnedSetting>(
                    Setting<TooltipLocationScopeLimitSetting>("搜索这些位置"),
                    Setting<TooltipLocationDisplayModeSetting>("显示方式"),
                    Setting<TooltipAmountOwnedSortSetting>("排序"),
                    Setting<ToolTipLocationLimitSetting>("最大结果数"),
                    Setting<TooltipCurrentCharacterSetting>("仅当前角色"),
                    Setting<TooltipAddCharacterNameSetting>("附加角色名"),
                    Setting<TooltipAmountOwnedColorSetting>("文字颜色"))),
            Page("tooltips/retrieve", "需取出数量",
                Paragraph("当前整理清单或制作清单还需要取出多少个此物品。"),
                Setting<TooltipDisplayRetrieveAmountSetting>("显示需取出数量"),
                EnabledBy<TooltipDisplayRetrieveAmountSetting>(
                    Setting<TooltipAmountToRetrieveColorSetting>("文字颜色"))),
            Page("tooltips/market", "市场价格",
                Paragraph("物品在 Universalis 上的价格。两行信息可分别显示。"),
                Setting<TooltipAverageMarketPriceSetting>("显示NQ/HQ均价"),
                Setting<TooltipMinimumMarketPriceSetting>("显示NQ/HQ最低价"),
                Setting<TooltipMarketPricingColorSetting>("文字颜色")),
            Page("tooltips/unlock", "物品解锁状态",
                Paragraph("显示是否已经学习物品，以及哪些角色已经学习。"),
                Setting<TooltipDisplayUnlockSetting>("显示解锁状态"),
                EnabledBy<TooltipDisplayUnlockSetting>(
                    Setting<TooltipDisplayUnlockCharacterSetting>("要检查的角色"),
                    Setting<TooltipDisplayUnlockDisplayModeSetting>("显示方式"),
                    Setting<TooltipDisplayUnlockHideUnlockedSetting>("隐藏已获取的角色"),
                    Setting<TooltipItemUnlockStatusColorSetting>("文字颜色"))),
            Page("tooltips/coffer", "宝箱内容",
                Paragraph("显示宝箱和其他容器可能包含的物品。"),
                Setting<TooltipDisplayCofferLootSetting>("显示宝箱内容"),
                EnabledBy<TooltipDisplayCofferLootSetting>(
                    Setting<TooltipCofferLootScopeSetting>("搜索这些位置"),
                    Setting<TooltipCofferLootColorSetting>("文字颜色"))),
            Page("tooltips/curated", "精选清单",
                Paragraph("显示哪些精选清单已包含此物品。"),
                Setting<TooltipDisplayCuratedListsSetting>("显示精选清单"),
                EnabledBy<TooltipDisplayCuratedListsSetting>(
                    Setting<TooltipCuratedListsSetting>("仅检查这些清单"),
                    Setting<TooltipCuratedListsMatchQualitySetting>("匹配物品品质"),
                    Setting<TooltipCuratedListsColorSetting>("文字颜色"))),
            Page("tooltips/glamour", "整套幻化",
                Paragraph("此物品是否可以补全正在收集的整套幻化。"),
                Setting<TooltipDisplayGlamourReadySetSetting>("显示整套幻化信息"),
                EnabledBy<TooltipDisplayGlamourReadySetSetting>(
                    Setting<TooltipGlamourReadySetScopeSetting>("搜索这些位置"),
                    Setting<TooltipGlamourReadySetDisplayModeSetting>("显示方式"),
                    Setting<TooltipGlamourReadySetColorSetting>("文字颜色"),
                    Setting<TooltipGlamourReadySetAcquiredColorSetting>("已获取物品颜色"),
                    Setting<TooltipGlamourReadySetNotAcquiredColorSetting>("未获取物品颜色"))),
            Page("tooltips/patch", "材料版本",
                Paragraph("制作材料在哪个版本中加入。"),
                Setting<TooltipDisplayIngredientPatchSetting>("显示材料版本"),
                EnabledBy<TooltipDisplayIngredientPatchSetting>(
                    Setting<TooltipIngredientPatchTooltipColorSetting>("文字颜色"))),
            Page("tooltips/sources", "来源信息",
                Paragraph("物品的获取方式。"),
                Setting<TooltipSourceInformationEnabledSetting>("显示来源信息"),
                EnabledBy<TooltipSourceInformationEnabledSetting>(
                    Setting<TooltipSourceInformationModifierSetting>("按住此键显示"),
                    Setting<TooltipSourceInformationColorSetting>("文字颜色"),
                    Scrollable("sourceInformation", 260,
                        Setting<TooltipSourceInformationSetting>()))),
            Page("tooltips/uses", "用途信息",
                Paragraph("物品可制作成什么，或可用于兑换什么。"),
                Setting<TooltipUseInformationEnabledSetting>("显示用途信息"),
                EnabledBy<TooltipUseInformationEnabledSetting>(
                    Setting<TooltipUseInformationModifierSetting>("按住此键显示"),
                    Setting<TooltipUseInformationColorSetting>("文字颜色"),
                    Scrollable("useInformation", 260,
                        Setting<TooltipUseInformationSetting>())))
        );
    }
}