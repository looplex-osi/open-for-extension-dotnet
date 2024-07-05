using Looplex.OpenForExtension.Commands;
using MutantNinjaTurtlePlugin;

namespace MutantNinjaTurtlePluginTests
{
    [TestClass]
    public class PluginTest
    {
        [TestMethod]
        public void Constructor_Default_PluginIsCreated()
        {
            // Act
            var plugin = new Plugin();

            // Assert
            Assert.IsNotNull(plugin);
            Assert.AreEqual(1, plugin.Commands.Count());
            Assert.IsTrue(typeof(IDefineActors).IsAssignableFrom(plugin.Commands.First().GetType()));
        }
    }
}