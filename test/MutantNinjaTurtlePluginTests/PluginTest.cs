using Looplex.OpenForExtension.Abstractions.Commands;
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
            Assert.IsTrue(typeof(IDefineRoles).IsAssignableFrom(plugin.Commands.First().GetType()));
        }
    }
}