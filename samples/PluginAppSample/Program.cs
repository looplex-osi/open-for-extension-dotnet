using Looplex.OpenForExtension.Context;
using Looplex.OpenForExtension.Manager;
using Looplex.OpenForExtension.Plugins;
using PluginAppSample.Actions;

namespace PluginAppSample
{
    internal class Program
    {
        static void Main(string[] _)
        {
            var context = DefaultContext.Create(LoadPlugins(), null);

            var action = new DefaultAction();

            action.Execute(context);

            Console.WriteLine("Waiting for any key ...");
            Console.ReadLine();
        }

        private static IList<IPlugin> LoadPlugins()
        {
            return (new PluginLoader()).LoadPlugins(GetPluginsPaths()).ToList();
        }

        private static IEnumerable<string> GetPluginsPaths()
        {
            string root = Path.GetFullPath(Path.Combine(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(typeof(Program).Assembly.Location)!)!)!)!)!));

            string[] pluginsPath = new string[] {
                @"PluginSample\bin\Debug\net6.0\PluginSample.dll",
            };

            foreach (var relativePath in pluginsPath)
            {
                yield return Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
            }
        }
    }
}