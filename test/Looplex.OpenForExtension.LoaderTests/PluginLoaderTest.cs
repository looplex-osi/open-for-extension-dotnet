using Looplex.OpenForExtension.Loader;

namespace Looplex.OpenForExtension.LoaderTests
{
    [TestClass]
    public class PluginLoaderTest
    {
        [TestMethod]
        public void Constructor_PluginsPathsIsEmpty_PluginManagerIsCreated()
        {
            // Prepare 
            var loader = new PluginLoader();

            // Act
            var plugins = loader.LoadPlugins(Array.Empty<string>(), new List<string>());

            // Assert
            Assert.IsNotNull(plugins);
            Assert.AreEqual(0, plugins.Count());
        }

        [TestMethod]
        public void Constructor_PluginsPathsHasValue_PluginManagerIsCreated()
        {
            // Prepare
            var loader = new PluginLoader();

            // Act
            var plugins = loader.LoadPlugins(GetPluginsPaths(), ["RaceService.StartRace"]);

            // Assert
            Assert.IsNotNull(loader);
            Assert.AreEqual(2, plugins.Count());
        }

        private static IEnumerable<string> GetPluginsPaths()
        {
            string[] args = ["BoyInTheAudiencePlugin", "MutantNinjaTurtlePlugin"];
            foreach (var pluginName in args)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{pluginName}.dll");

                yield return path;
            }
        }
    }
}