using InventoryTools.Logic.Settings;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.ConfigLayouts;

public class MarketBoardLayout : ConfigLayout
{
    public override PageLayout Build()
    {
        return Page("marketboard", "市场",
            Paragraph("市场价格由 Universalis 提供，需要下载并缓存。"),
            Section("获取价格的服务器",
                Paragraph("以下选项会合并为一份服务器列表。额外服务器将与原始和当前服务器一起查询。"),
                Setting<MarketBoardUseHomeWorldSetting>("原始服务器"),
                Setting<MarketBoardUseActiveWorldSetting>("当前服务器"),
                Setting<MarketBoardExtraWorldsSetting>("其他服务器")),
            Section("下载",
            Paragraph("是否自动下载价格数据？关闭后，必须点击“刷新市场价格”才能获取物品价格。"),
                Setting<AutomaticallyDownloadPricesSetting>("自动下载价格"),
                Setting<MarketRefreshTimeHoursSetting>("价格保留时间（小时）"),
                Setting<MarketBoardSaleCountLimitSetting>("成交历史范围（天）"))
        );
    }
}
