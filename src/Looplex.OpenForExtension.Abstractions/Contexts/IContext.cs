using System;
using System.Collections.Generic;
using Looplex.OpenForExtension.Abstractions.Plugins;

namespace Looplex.OpenForExtension.Abstractions.Contexts
{
    public interface IContext
    {
        IList<IPlugin> Plugins { get; }
        bool SkipDefaultAction { get; set; }
        dynamic State { get; }
        IDictionary<string, dynamic> Roles { get; }
        IServiceProvider Services { get; }
        object? Result { get; set; }
    }
}
