using System.Collections.Generic;
using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Models;
using InventoryTools.Logic.Filters.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters;

public enum CraftReportPrefix
{
    Blank = 0,
    PluginName = 1,
    CraftListName = 2
}

public class CraftReportPrefixFilter : ChoiceFilter<CraftReportPrefix>
{
    public override CraftReportPrefix CurrentValue(FilterConfiguration configuration)
    {
        return (CraftReportPrefix)(configuration.GetUintFilter(Key) ?? (uint)DefaultValue);
    }
    
    public override void ResetFilter(FilterConfiguration configuration)
    {
        configuration.UpdateUintFilter(Key, (uint)DefaultValue);
    }
    
    public override void UpdateFilterConfiguration(FilterConfiguration configuration, CraftReportPrefix newValue)
    {
        configuration.UpdateUintFilter(Key, (uint)newValue);
    }
    
    public override string Key { get; set; } = "CraftReportPrefix";
    public override string Name { get; set; } = "通知前缀";
    
    public override string HelpText { get; set; } =
        "为获取进度消息添加前缀。“插件名称”使用[AT]，“制作清单名称”使用此清单的名称。";
    
    public override FilterCategory FilterCategory { get; set; } = FilterCategory.Notifications;
    public override CraftReportPrefix DefaultValue { get; set; } = CraftReportPrefix.Blank;
    public override FilterType AvailableIn { get; set; } = FilterType.CraftFilter;
    
    public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
    {
        return null;
    }
    
    public override bool? FilterItem(FilterConfiguration configuration, ItemRow item)
    {
        return null;
    }
    
    public override List<CraftReportPrefix> GetChoices(FilterConfiguration configuration)
    {
        return
        [
            CraftReportPrefix.Blank,
            CraftReportPrefix.PluginName,
            CraftReportPrefix.CraftListName
        ];
    }
    
    public override string GetFormattedChoice(FilterConfiguration filterConfiguration, CraftReportPrefix choice)
    {
        return choice switch
        {
            CraftReportPrefix.Blank => "无",
            CraftReportPrefix.PluginName => "插件名称",
            CraftReportPrefix.CraftListName => "制作清单名称",
            _ => choice.ToString()
        };
    }
    
    public CraftReportPrefixFilter(ILogger<CraftReportPrefixFilter> logger, ImGuiService imGuiService) : base(logger,
        imGuiService)
    {
    }
}