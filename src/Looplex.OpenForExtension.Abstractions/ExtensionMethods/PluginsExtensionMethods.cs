using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Looplex.OpenForExtension.Abstractions.Commands;
using Looplex.OpenForExtension.Abstractions.Contexts;
using Looplex.OpenForExtension.Abstractions.Plugins;

namespace Looplex.OpenForExtension.Abstractions.ExtensionMethods
{
    public static class PluginsExtensionMethods
    {
        public static async Task ExecuteAsync<T>(this IEnumerable<IPlugin> plugins, IContext context, CancellationToken cancellationToken) where T : ICommand
        {
            foreach (var plugin in plugins)
            {
                await plugin.ExecuteAsync<T>(context, cancellationToken);
            }
        }
        public static void Execute<T>(this IEnumerable<IPlugin> plugins, IContext context, CancellationToken cancellationToken) where T : ICommand
        {
            foreach (var plugin in plugins)
            {
                plugin.Execute<T>(context, cancellationToken);
            }
        }
    }
}
