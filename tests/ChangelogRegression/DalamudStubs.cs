namespace Dalamud.Plugin
{
    // Only the members used by the real ChangelogService are needed here.
    public interface IDalamudPluginInterface
    {
        FileInfo AssemblyLocation { get; }
    }
}

namespace Dalamud.Plugin.Services
{
    public interface IPluginLog
    {
        void Error(string message);
        void Error(Exception exception, string message);
    }
}
