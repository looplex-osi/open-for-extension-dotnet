using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;
using Looplex.OpenForExtension.Plugins;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Looplex.OpenForExtension.ExtensionMethods
{
    public static class PluginsExtensionMethods
    {
        public static async Task ExecuteAsync<T>(this IEnumerable<IPlugin> plugins, IDefaultContext pluginContext, CancellationToken cancellationToken) where T : ICommand
        {
            foreach (var plugin in plugins)
            {
                await plugin.TryExecuteAsync<T>(pluginContext, cancellationToken);
            }
        }
        public static void Execute<T>(this IEnumerable<IPlugin> plugins, IDefaultContext pluginContext, CancellationToken cancellationToken) where T : ICommand
        {
            foreach (var plugin in plugins)
            {
                plugin.TryExecute<T>(pluginContext, cancellationToken);
            }
        }
    }
}
