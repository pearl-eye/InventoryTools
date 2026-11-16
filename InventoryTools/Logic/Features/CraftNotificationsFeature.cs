using System.Collections.Generic;
using System.Linq;
using InventoryTools.Logic.Filters;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Logic.WizardSettings;
using InventoryTools.Services.Interfaces;
using InventoryTools.Ui.Config;
using InventoryTools.Ui.Config.Layouts;

namespace InventoryTools.Logic.Features;

public class CraftNotificationsFeature : Feature
{
    private readonly CraftReportCompletionOnlyFilter _completionOnlyFilter;
    private readonly CraftNotificationsCompletionOnlySetting _completionOnlySetting;
    private readonly InventoryToolsConfiguration _configuration;
    private readonly IListService _listService;
    private readonly CraftReportPlaySoundFilter _playSoundFilter;
    private readonly CraftNotificationsPlaySoundSetting _playSoundSetting;
    private readonly CraftReportProgressFilter _progressFilter;
    private readonly CraftNotificationsReportToChatSetting _reportToChatSetting;

    public CraftNotificationsFeature(IEnumerable<ISetting> settings,
        CraftNotificationsReportToChatSetting reportToChatSetting,
        CraftNotificationsPlaySoundSetting playSoundSetting,
        CraftNotificationsCompletionOnlySetting completionOnlySetting,
        CraftReportProgressFilter progressFilter,
        CraftReportPlaySoundFilter playSoundFilter,
        CraftReportCompletionOnlyFilter completionOnlyFilter,
        IListService listService,
        InventoryToolsConfiguration configuration) : base(settings)
    {
        _reportToChatSetting = reportToChatSetting;
        _playSoundSetting = playSoundSetting;
        _completionOnlySetting = completionOnlySetting;
        _progressFilter = progressFilter;
        _playSoundFilter = playSoundFilter;
        _completionOnlyFilter = completionOnlyFilter;
        _listService = listService;
        _configuration = configuration;
    }

    public override PageLayout Build()
    {
        return Page("feature/craft-notifications", "制作通知",
            Paragraph("收集制作清单中的物品时，插件可以通知当前进度。仅在有当前制作清单时发送通知。"),
            Setting<CraftNotificationsReportToChatSetting>("在聊天窗口报告进度"),
            Setting<CraftNotificationsPlaySoundSetting>("完成一个物品时播放提示音"),
            Setting<CraftNotificationsCompletionOnlySetting>("仅报告已完成物品，不报告每次获取"),
            Paragraph("完成向导后，这些设置会应用于现有制作清单和默认制作清单。每份清单随后保留自己的设置，可在该清单中单独修改。")
        );
    }

    public override void OnFinish()
    {
        var reportToChat = _reportToChatSetting.CurrentValue(_configuration);
        var playSound = _playSoundSetting.CurrentValue(_configuration);
        var completionOnly = _completionOnlySetting.CurrentValue(_configuration);

        var lists = _listService.Lists
            .Where(c => c.FilterType == FilterType.CraftFilter)
            .ToList();
        var defaultList = _listService.GetDefaultCraftList();
        if (!lists.Contains(defaultList))
        {
            lists.Add(defaultList);
        }

        foreach (var list in lists)
        {
            _progressFilter.UpdateFilterConfiguration(list, reportToChat);
            _playSoundFilter.UpdateFilterConfiguration(list, playSound);
            _completionOnlyFilter.UpdateFilterConfiguration(list, completionOnly);
        }
    }
}