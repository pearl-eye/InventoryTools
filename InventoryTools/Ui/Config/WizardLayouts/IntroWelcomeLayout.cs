using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Ui.Config.WizardLayouts;

public class IntroWelcomeLayout : ContentLayout
{
    public override PageLayout Build()
    {
        return Page("intro/welcome", "概览",
            Paragraph("接下来会简要介绍 Allagan Tools 的三项主要功能，大约需要一分钟。之后也能在帮助窗口中重新查看。"),
            Section("记录各处背包",
                Paragraph("统一搜索角色、雇员、部队、鞍囊和幻化台中的物品。")),
            Section("规划制作",
                Paragraph("将配方逐级拆分，核对已持有材料，并查找其他材料的购买或采集来源。")),
            Section("查询资料",
                Paragraph("查询物品、怪物、副本、NPC、飞空艇等资料，了解来源和用途。"))
        );
    }
}
