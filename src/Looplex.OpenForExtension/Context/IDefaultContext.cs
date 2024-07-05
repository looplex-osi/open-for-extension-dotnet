using Looplex.OpenForExtension.Plugins;
using System;
using System.Collections.Generic;

namespace Looplex.OpenForExtension.Context
{
    public interface IDefaultContext
    {
        IList<IPlugin> Plugins { get; }
        bool SkipDefaultAction { get; set; }
        dynamic State { get; }
        IDictionary<string, dynamic> Actors { get; }
        IServiceProvider Services { get; }
        object Result { get; set; }
    }
}
