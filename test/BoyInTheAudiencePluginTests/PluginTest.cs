using BoyInTheAudiencePlugin;
using Looplex.OpenForExtension.Abstractions.Commands;

namespace BoyInTheAudiencePluginTests
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
            Assert.AreEqual(2, plugin.Commands.Count());
            Assert.IsTrue(typeof(IDefineRoles).IsAssignableFrom(plugin.Commands.First().GetType()));
            Assert.IsTrue(typeof(IBind).IsAssignableFrom(plugin.Commands.Last().GetType()));
        }
    }
}