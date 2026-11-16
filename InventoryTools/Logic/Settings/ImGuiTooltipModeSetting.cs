using System.Collections.Generic;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Logic.Settings.Abstract.Generic;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public enum ImGuiTooltipMode
{
    Never,
    Icons,
    Everywhere
}

public class ImGuiTooltipModeSetting : GenericEnumChoiceSetting<ImGuiTooltipMode>
{
    public ImGuiTooltipModeSetting(ILogger<ImGuiTooltipModeSetting> logger,
        ImGuiService imGuiService) : base("ImGuiTooltipMode",
        "物品提示显示方式",
        "何时显示物品提示：从不显示、鼠标悬停于物品图标时，或悬停于表格中的任意物品行时？",
        ImGuiTooltipMode.Icons,
        new Dictionary<ImGuiTooltipMode, string>()
        {
            {ImGuiTooltipMode.Never, "从不"},
            {ImGuiTooltipMode.Icons, "仅图标"},
            {ImGuiTooltipMode.Everywhere, "所有位置"},
        },
        "1.11.0.8",
        logger,
        imGuiService)
    {
    }
}