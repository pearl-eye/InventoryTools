using System;
using System.Collections.Generic;
using CriticalCommonLib.Models;
using InventoryTools.Logic.Editors;
using InventoryTools.Logic.Filters;
using InventoryTools.Logic.ItemRenderers;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using InventoryTools.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Features;

public class SampleFilterMaterialCleanup : BooleanSetting, ISampleFilter
{
    private readonly IListService _listService;
    private readonly Func<ItemInfoRenderCategory, GenericHasSourceCategoryFilter> _hasSourceCategoryFactory;
    private readonly FilterConfiguration.Factory _filterConfigFactory;
    private readonly SourceInventoriesFilter _sourceInventoriesFilter;
    private readonly DestinationInventoriesFilter _destinationInventoriesFilter;
    private readonly HighlightWhenFilter _highlightWhenFilter;

    public SampleFilterMaterialCleanup(ILogger<SampleFilterMaterialCleanup> logger, ImGuiService imGuiService,
        IListService listService, Func<ItemInfoRenderCategory, GenericHasSourceCategoryFilter> hasSourceCategoryFactory,
        FilterConfiguration.Factory filterConfigFactory, SourceInventoriesFilter sourceInventoriesFilter,
        DestinationInventoriesFilter destinationInventoriesFilter, HighlightWhenFilter highlightWhenFilter) : base(logger, imGuiService)
    {
        _listService = listService;
        _hasSourceCategoryFactory = hasSourceCategoryFactory;
        _filterConfigFactory = filterConfigFactory;
        _sourceInventoriesFilter = sourceInventoriesFilter;
        _destinationInventoriesFilter = destinationInventoriesFilter;
        _highlightWhenFilter = highlightWhenFilter;
    }
    private bool _shouldAdd;
    public override bool DefaultValue { get; set; }
    public override bool CurrentValue(InventoryToolsConfiguration configuration)
    {
        return _shouldAdd;
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
    {
        _shouldAdd = newValue;
    }

    public override bool AppearsInConfigWindow => false;

    public override string Key { get; set; } = "sample3";
    public override string Name { get; set; } = "整理材料";
    public override string HelpText { get; set; } = "查找角色背包中的可采集物品，并显示应放入哪名雇员的背包。";
    public string SampleDefaultName => "100金币及以下";
    public string SampleDescription =>
        "添加一份清单，帮助快速存放多余材料。自动加入全部材料分类，并在规划存放位置时优先合并已有堆叠。";
    public SampleFilterType SampleFilterType => SampleFilterType.Sample;
    public override string Version => "1.7.0.0";
    public bool ShouldAdd => _shouldAdd;
    public FilterConfiguration AddFilter()
    {
        var sampleFilter = _filterConfigFactory.Invoke();
        sampleFilter.Name = Name;
        sampleFilter.FilterType = FilterType.SortingFilter;
        sampleFilter.DisplayInTabs = true;
        _sourceInventoriesFilter.UpdateFilterConfiguration(sampleFilter, [
            new InventorySearchScope()
            {
                ActiveCharacter = true,
                Categories = [InventoryCategory.CharacterBags]
            }
        ]);
        _destinationInventoriesFilter.UpdateFilterConfiguration(sampleFilter, [
            new InventorySearchScope()
            {
                ActiveCharacter = true,
                Categories = [InventoryCategory.RetainerBags]
            }
        ]);
        _highlightWhenFilter.UpdateFilterConfiguration(sampleFilter, HighlightWhen.Always);

        sampleFilter.FilterItemsInRetainersEnum = FilterItemsRetainerEnum.Yes;
        var gatherFilter = _hasSourceCategoryFactory.Invoke(ItemInfoRenderCategory.Gathering);
        gatherFilter.UpdateFilterConfiguration(sampleFilter, true);
        _listService.AddDefaultColumns(sampleFilter);
        _listService.AddList(sampleFilter);
        return sampleFilter;
    }
}