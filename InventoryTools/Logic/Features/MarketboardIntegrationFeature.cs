using System.Collections.Generic;
using InventoryTools.Logic.Settings;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Ui.Config;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Logic.Features;

public class MarketboardIntegrationFeature : Feature
{
    public MarketboardIntegrationFeature(IEnumerable<ISetting> settings) : base(settings)
    {
    }

    public override PageLayout Build()
    {
        return Page("feature/marketboard", "市场",
            Paragraph("价格由 Universalis 提供。插件会下载价格并保存在本地。启用后自动下载；关闭时，每次需要新价格都必须点击“刷新市场价格”。"),
            Setting<AutomaticallyDownloadPricesSetting>("自动下载价格"),
            Setting<MarketRefreshTimeHoursSetting>("价格保留时间（小时）"),
            Setting<MarketBoardSaleCountLimitSetting>("包含的成交历史天数")
        );
    }
}
