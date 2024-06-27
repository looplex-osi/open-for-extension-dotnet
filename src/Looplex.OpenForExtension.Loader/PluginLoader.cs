using Looplex.OpenForExtension.Plugins;
using System.Reflection;

namespace Looplex.OpenForExtension.Manager
{
    public class PluginLoader
    {
        public virtual IEnumerable<IPlugin> LoadPlugins(IEnumerable<string> pluginsPaths)
        {
            var plugins = new List<IPlugin>();
            foreach (var pluginPath in pluginsPaths)
            {
                var assembly = LoadAssembly(pluginPath);
                plugins.AddRange(CreatePlugins(assembly));
            }
            return plugins;
        }

        private static Assembly LoadAssembly(string pluginPath)
        {
            Console.WriteLine($"Loading commands from: {pluginPath}");

            var loadContext = new PluginLoadContext(pluginPath);
            return loadContext.LoadFromAssemblyName(
                new AssemblyName(Path.GetFileNameWithoutExtension(pluginPath)));
        }

        private static IEnumerable<IPlugin> CreatePlugins(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IPlugin).IsAssignableFrom(type))
                {
                    if (Activator.CreateInstance(type) is IPlugin plugin)
                    {
                        yield return plugin;
                    }
                }
            }
        }
    }
}