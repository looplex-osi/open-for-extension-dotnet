using Looplex.OpenForExtension.Context;

namespace PluginAppSample.OpenForExtension.Definitions.Contexts
{
    public interface IDefaultContext : IPluginContext
    {
        bool SkipDefaultAction { get; }
    }
}
