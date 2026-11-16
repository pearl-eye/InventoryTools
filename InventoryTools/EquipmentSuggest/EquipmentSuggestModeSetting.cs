using System.Collections.Generic;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Logic.Settings.Abstract.Generic;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.EquipmentSuggest;

public enum EquipmentSuggestMode
{
    Class,
    Tool
}

public class EquipmentSuggestModeSetting : GenericEnumChoiceSetting<EquipmentSuggestMode>
{
    private readonly EquipmentSuggestConfig _config;

    public EquipmentSuggestModeSetting(ILogger<EquipmentSuggestModeSetting> logger, EquipmentSuggestConfig config, ImGuiService imGuiService) : base("EquipmentSuggestMode", "模式", "选择装备推荐模式。“职业”模式按所选职业推荐装备；“工具”模式为一组职业推荐工具。", EquipmentSuggestMode.Class, new (){
        { EquipmentSuggestMode.Class , "职业"}, { EquipmentSuggestMode.Tool , "工具"}}, "1.12.0.10", logger, imGuiService)
    {
        _config = config;
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, EquipmentSuggestMode newValue)
    {
        _config.IsDirty = true;
        base.UpdateFilterConfiguration(configuration, newValue);
    }
}