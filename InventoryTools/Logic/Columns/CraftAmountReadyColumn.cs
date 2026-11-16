using System.Collections.Generic;
using CriticalCommonLib.Services.Mediator;
using DalaMock.Host.Mediator;
using Dalamud.Interface.Colors;
using Dalamud.Bindings.ImGui;
using InventoryTools.Logic.Columns.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Columns
{
    public class CraftAmountReadyColumn : IntegerColumn
    {
        public CraftAmountReadyColumn(ILogger<CraftAmountReadyColumn> logger, ImGuiService imGuiService) : base(logger, imGuiService)
        {
        }
        public override ColumnCategory ColumnCategory => ColumnCategory.Crafting;
        public override int? CurrentValue(ColumnConfiguration columnConfiguration, SearchResult searchResult)
        {
            if (searchResult.CraftItem == null) return 0;
            if (searchResult.CraftItem.IsOutputItem)
            {
                return 0;
            }
            return (int?) searchResult.CraftItem.QuantityReady;
        }

        public override List<MessageBase>? Draw(FilterConfiguration configuration,
            ColumnConfiguration columnConfiguration,
            SearchResult searchResult, int rowIndex, int columnIndex)
        {
            if (searchResult.CraftItem == null) return null;

            if (searchResult.CraftItem.IsOutputItem)
            {
                ImGui.TableNextColumn();
                return null;
            }
            if(searchResult.CraftItem.QuantityReady >= searchResult.CraftItem.QuantityNeeded)
            {
                ImGui.PushStyleColor(ImGuiCol.Text, ImGuiColors.HealerGreen);
            }

            base.Draw(configuration, columnConfiguration, searchResult, rowIndex, columnIndex);
            if(searchResult.CraftItem.QuantityReady >= searchResult.CraftItem.QuantityNeeded)
            {
                ImGui.PopStyleColor();
            }
            return null;
        }

        public override string Name { get; set; } = "角色背包中的数量";
        public override string RenderName => "背包";
        public override float Width { get; set; } = 60;
        public override bool? CraftOnly => true;
        public override FilterType AvailableIn { get; } = Logic.FilterType.CraftFilter;
        public override string HelpText { get; set; } =
            "所选背包中可用于完成制作的物品数量。";
        public override bool HasFilter { get; set; } = false;
        public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Text;
    }
}