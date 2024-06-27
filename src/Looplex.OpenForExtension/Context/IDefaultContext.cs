using System;

namespace Looplex.OpenForExtension.Context
{
    public interface IDefaultContext : IPluginContext
    {
        bool SkipDefaultAction { get; set; }
        dynamic State { get; }
        IServiceProvider Services { get; }
        object Result { get; set; }
    }
}
