using System;
using AllaganLib.Shared.Interfaces;
using DalaMock.Host.Mediator;
using Dalamud.Bindings.ImGui;
using InventoryTools.Mediator;
using InventoryTools.Services.Interfaces;
using OtterGui.Raii;

namespace InventoryTools.Ui.DebugWindows;

public class ListServiceDebuggerPane : IDebugPane
{
    private Lazy<IListService> _listService;
    private readonly MediatorService _mediatorService;

    public IListService ListService => _listService.Value;

    public ListServiceDebuggerPane(Lazy<IListService> listService, MediatorService mediatorService)
    {
        _listService = listService;
        _mediatorService = mediatorService;
    }

    public string Name => "清单服务";

    public void Draw()
    {
        var activeBackgroundList = ListService.GetActiveBackgroundList();
        var activeUiList = ListService.GetActiveUiList(false);
        var lists = ListService.Lists;
        ImGui.Text($"当前后台清单：{(activeBackgroundList == null ? "无清单" : activeBackgroundList.Name)}");
        ImGui.Text($"当前界面清单：{(activeUiList == null ? "无清单" : activeUiList.Name)}");
        foreach (var list in lists)
        {
            using var id = ImRaii.PushId(list.Key);
            ImGui.Text($"{list.Name}:");
            ImGui.Text($"{(list.Active ? "已启用" : "未启用")}");
            ImGui.SameLine();
            if (ImGui.Button("请求刷新"))
            {
                _mediatorService.Publish(new RequestListUpdateMessage(list));
            }
        }
    }
}
