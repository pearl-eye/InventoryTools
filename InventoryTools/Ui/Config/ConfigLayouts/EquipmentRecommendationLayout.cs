using InventoryTools.EquipmentSuggest;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.ConfigLayouts;

public class EquipmentRecommendationLayout : ConfigLayout
{
    public override PageLayout Build()
    {
        return Page("equipment-recommendation", "装备推荐",
            Paragraph("将身上装备与其他持有装备比较，推荐可提升的装备。以下是推荐界面打开时使用的默认设置。"),
            Section("默认设置",
                Setting<EquipmentSuggestModeSetting>("推荐依据"),
                Setting<EquipmentSuggestViewModeSetting>("布局"))
        );
    }
}
