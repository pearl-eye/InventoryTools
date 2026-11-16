using System.Collections.Generic;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Logic.Settings.Abstract.Generic;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public enum CraftWindowView
{
    Crafts,
    Tree,
    Configuration
}

public class CraftWindowViewSetting : GenericEnumChoiceSetting<CraftWindowView>
{
    public override bool AppearsInConfigWindow => false;

    public CraftWindowViewSetting(ILogger<CraftWindowViewSetting> logger, ImGuiService imGuiService) : base("CraftWindowView", "制作窗口视图", "制作清单当前显示哪种视图？",CraftWindowView.Crafts, new Dictionary<CraftWindowView, string>(){
        { CraftWindowView.Crafts , "制作"}, { CraftWindowView.Configuration, "设置"},
        { CraftWindowView.Tree, "树状视图"}}, "12.0.20", logger, imGuiService)
    {
    }
}