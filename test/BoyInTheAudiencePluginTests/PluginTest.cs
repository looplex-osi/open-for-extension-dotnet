using Looplex.OpenForExtension.Commands;
using BoyInTheAudiencePlugin;

namespace PluginSampleTests
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
            Assert.IsTrue(typeof(IDefineActors).IsAssignableFrom(plugin.Commands.First().GetType()));
            Assert.IsTrue(typeof(IBind).IsAssignableFrom(plugin.Commands.Last().GetType()));
        }
    }
}