using Looplex.OpenForExtension.Manager;
using Looplex.OpenForExtensionTests;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Looplex.OpenForExtension.ManagerTests
{
    [TestClass]
    public class PluginLoaderTest : OpenForExtensionTestsBase
    {
        [TestMethod]
        public void Constructor_PluginsPathsIsEmpty_PluginManagerIsCreated()
        {
            // Prepare 
            var loader = new PluginLoader();

            // Act
            var plugins = loader.LoadPlugins(Array.Empty<string>());

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
            var plugins = loader.LoadPlugins(GetPluginsPaths());

            // Assert
            Assert.IsNotNull(loader);
            Assert.AreEqual(1, plugins.Count());
        }

        private static IEnumerable<string> GetPluginsPaths()
        {
            string root = Path.GetFullPath(Path.Combine(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(
                                    Path.GetDirectoryName(typeof(Program).Assembly.Location)!)!)!)!)!)!));

            string[] pluginsPath = new string[] {
                @"samples\PluginSample\bin\Debug\net8.0\PluginSample.dll",
            };

            foreach (var relativePath in pluginsPath)
            {
                yield return Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
            }
        }


    }
}