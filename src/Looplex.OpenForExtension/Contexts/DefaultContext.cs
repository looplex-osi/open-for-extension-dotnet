using System;
using System.Collections.Generic;
using System.Dynamic;
using Looplex.OpenForExtension.Abstractions.Contexts;
using Looplex.OpenForExtension.Abstractions.Plugins;

namespace Looplex.OpenForExtension.Contexts
{
    public class DefaultContext : IContext
    {
        public bool SkipDefaultAction { get; set; } = false;
        public dynamic State { get; } = new ExpandoObject();
        public IDictionary<string, dynamic> Roles { get; } = new Dictionary<string, dynamic>();
        public IList<IPlugin> Plugins { get; private set; } = null!;
        public IServiceProvider Services { get; private set; } = null!;
        public object Result { get; set; }

        public static IContext Create(IList<IPlugin> plugins, IServiceProvider services)
        {
            return new DefaultContext()
            {
                Plugins = plugins,
                Services = services
            };
        }
    }
}
