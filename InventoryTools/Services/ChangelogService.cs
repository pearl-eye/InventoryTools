using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Ionide.KeepAChangelog;
using SemVersion;

namespace InventoryTools.Services;

public class ChangelogService
{
    private readonly IDalamudPluginInterface _pluginInterface;
    private readonly IPluginLog _pluginLog;
    private readonly List<(SemanticVersion, DateTime, Domain.ChangelogData)> changeLogs;

    public ChangelogService(IDalamudPluginInterface pluginInterface, IPluginLog pluginLog)
    {
        _pluginInterface = pluginInterface;
        _pluginLog = pluginLog;
        this.changeLogs = new();

        // Release notes are optional. A malformed or missing file must not
        // prevent the settings and other menu windows from being created.
        try
        {
            var assemblyLocation = _pluginInterface.AssemblyLocation.DirectoryName!;
            var fileName = Path.Combine(assemblyLocation, "CHANGELOG.md");
            var parsedLogs = Parser.parseChangeLog(new FileInfo(fileName));
            if (!parsedLogs.IsOk)
            {
                _pluginLog.Error($"Could not parse changelog file due to error: {parsedLogs.ErrorValue.Item1}");
                return;
            }

            var loadedLogs = new List<(SemanticVersion, DateTime, Domain.ChangelogData)>();
            foreach (var log in parsedLogs.ResultValue.Releases)
            {
                loadedLogs.Add((log.Item1, log.Item2, log.Item3.Value));
            }

            this.changeLogs = loadedLogs.OrderByDescending(c => c.Item1).ToList();
        }
        catch (Exception exception)
        {
            _pluginLog.Error(exception, "Could not read changelog; continuing without release notes.");
        }
    }

    public List<(SemanticVersion, DateTime, Domain.ChangelogData)> ChangeLogs => changeLogs;
}
