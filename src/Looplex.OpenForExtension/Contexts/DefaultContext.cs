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
        public IList<IPlugin> Plugins { get; private set; }
        public object Result { get; set; }

        public static IContext New()
        {
            return New(new List<IPlugin>());
        }
        
        public static IContext New(IList<IPlugin> plugins)
        {
            return new DefaultContext()
            {
                Plugins = plugins
            };
        }
    }
}
