using BoyInTheAudiencePlugin;
using NSubstitute;
using BoyInTheAudiencePluginTests.Mocks;
using Looplex.OpenForExtension.Abstractions.Commands;
using Looplex.OpenForExtension.Abstractions.Contexts;

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
            var context = Substitute.For<IContext>();
            var actors = new Dictionary<string, dynamic>();
            context.Roles.Returns(actors);

            // Act
            new RaceService().StartRaceAsync(() => plugin.Execute<IDefineRoles>(context, CancellationToken.None));
            
            // Assert
            Assert.IsNotNull(context.Roles["BoyInTheAudience"]);
        }
    }
}