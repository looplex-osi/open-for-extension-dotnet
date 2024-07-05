using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;
using BoyInTheAudiencePlugin;
using NSubstitute;

namespace BoyInTheAudiencePluginTests.Commands
{
    [TestClass]
    public class DefineActorsCommandTest
    {
        [TestMethod]
        public void Execute_ContextIsHandleInputContext_ExecuteIsRun()
        {
            // Arrange
            var plugin = new Plugin();
            var context = Substitute.For<IDefaultContext>();
            var actors = new Dictionary<string, dynamic>();
            context.Actors.Returns(actors);

            // Act
            plugin.TryExecute<IDefineActors>(context);

            // Assert
            Assert.IsNotNull(context.Actors["BoyInTheAudience"]);
        }
    }
}