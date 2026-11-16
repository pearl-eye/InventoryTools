using System;
using InventoryTools.Logic.Columns.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Columns
{
    public class SellToVendorPriceTotalColumn : GilColumn
    {
        public SellToVendorPriceTotalColumn(ILogger<SellToVendorPriceTotalColumn> logger, ImGuiService imGuiService) : base(logger, imGuiService)
        {
        }
        public override ColumnCategory ColumnCategory => ColumnCategory.Basic;
        public override int? CurrentValue(ColumnConfiguration columnConfiguration, SearchResult searchResult)
        {
            if (searchResult.InventoryItem != null)
            {
                return (int)(searchResult.InventoryItem.SellToVendorPrice * searchResult.Quantity);
            }
            return (int)(searchResult.Item.Base.PriceLow * searchResult.Quantity);
        }
        public override string Name { get; set; } = "商店出售总价";
        public override float Width { get; set; } = 100.0f;
        public override string HelpText { get; set; } = "将此物品全部卖给商人的总价格（单价×数量，单位为金币）。";
        public override bool HasFilter { get; set; } = true;
        public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Text;
    }
}