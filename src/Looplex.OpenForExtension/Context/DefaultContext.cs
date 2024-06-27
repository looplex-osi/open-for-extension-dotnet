using Looplex.OpenForExtension.Plugins;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Looplex.OpenForExtension.Context
{
    public class DefaultContext : IDefaultContext
    {
        public bool SkipDefaultAction { get; set; } = true;
        public dynamic State { get; } = new ExpandoObject();
        public IList<IPlugin> Plugins { get; private set; }
        public IServiceProvider Services { get; private set; }
        public object Result { get; set; }

        public static IDefaultContext Create(IList<IPlugin> plugins, IServiceProvider services)
        {
            DefaultContext context = new DefaultContext()
            {
                Plugins = plugins,
                Services = services
            };

            return context;
        }
    }
}
