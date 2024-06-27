using Looplex.OpenForExtension.Plugins;
using System.Collections.Generic;

namespace Looplex.OpenForExtension.Context
{
    public interface IPluginContext
    {
        IList<IPlugin> Plugins { get; }
    }
}