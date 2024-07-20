using Looplex.OpenForExtension.Commands;
using BoyInTheAudiencePlugin;
using NSubstitute;
using BoyInTheAudiencePluginTests.Mocks;

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
            new RaceService().StartRaceAsync(() => plugin.TryExecute<IDefineActors>(context, CancellationToken.None));
            
            // Assert
            Assert.IsNotNull(context.Actors["BoyInTheAudience"]);
        }
    }
}