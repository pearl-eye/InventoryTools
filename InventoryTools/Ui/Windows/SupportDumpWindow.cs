using System.Linq;
using System.Numerics;
using CriticalCommonLib.Services;
using CriticalCommonLib.Services.Mediator;
using DalaMock.Host.Mediator;
using DalaMock.Shared.Interfaces;
using Dalamud.Bindings.ImGui;
using InventoryTools.Logic;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Ui
{
    public class SupportDumpWindow : GenericWindow
    {
        private readonly SupportDumpService _supportDumpService;
        private readonly IFileDialogManager _fileDialogManager;
        private readonly IChatUtilities _chatUtilities;

        public SupportDumpWindow(ILogger<SupportDumpWindow> logger, MediatorService mediator, ImGuiService imGuiService, InventoryToolsConfiguration configuration, SupportDumpService supportDumpService, IFileDialogManager fileDialogManager, IChatUtilities chatUtilities, string name = "生成诊断文件") : base(logger, mediator, imGuiService, configuration, name)
        {
            _supportDumpService = supportDumpService;
            _fileDialogManager = fileDialogManager;
            _chatUtilities = chatUtilities;
        }

        public override void Initialize()
        {
            WindowName = "生成诊断文件";
            Key = "supportdump";
        }

        public override bool SaveState => false;
        public override Vector2? DefaultSize { get; } = new Vector2(500, 200);
        public override Vector2? MaxSize { get; } = new Vector2(800, 400);
        public override Vector2? MinSize { get; } = new Vector2(300, 150);
        public override string GenericKey { get; } = "supportdump";
        public override string GenericName { get; } = "生成诊断文件";
        public override bool DestroyOnClose => true;

        public override void DrawWindow()
        {
            ImGui.PushTextWrapPos();
            ImGui.TextWrapped(
                "仅在需要排查问题时使用。此功能会生成包含背包记录、插件设置和日志的 ZIP 文件。若要协助修复问题，请先在“文件”菜单启用详细日志，重现问题后再生成。是否继续？");
            ImGui.PopTextWrapPos();
            ImGui.NewLine();

            if (ImGui.Button("继续"))
            {
                _fileDialogManager.SaveFileDialog("保存诊断文件", "*.zip", "support_dump.zip", ".zip",
                    (success, path) =>
                    {
                        if (success)
                        {
                            GenerateDump(path);
                        }
                    }, null, true);
            }

            ImGui.SameLine();

            if (ImGui.Button("取消"))
            {
                this.IsOpen = false;
            }
        }

        private void GenerateDump(string path)
        {
            var result = _supportDumpService.GenerateDump(path);
            if (result.Success)
            {
                var included = result.IncludedFiles.Any() ? string.Join(", ", result.IncludedFiles) : "no files";
                _chatUtilities.Print($"诊断文件已保存到 {result.ZipPath} ({included}).");
                if (result.MissingFiles.Any())
                {
                    _chatUtilities.PrintError($"以下文件未能包含在诊断文件中：{string.Join(", ", result.MissingFiles)}.");
                }
            }
            else
            {
                _chatUtilities.PrintError("生成诊断文件失败，请查看卫月日志了解详情。");
            }

            this.IsOpen = false;
        }

        public override FilterConfiguration? SelectedConfiguration => null;

        public override void Invalidate()
        {

        }
    }
}
