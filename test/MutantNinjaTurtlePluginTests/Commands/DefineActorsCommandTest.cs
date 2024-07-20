using MutantNinjaTurtlePlugin;
using NSubstitute;
using System.Dynamic;
using Looplex.OpenForExtension.Abstractions.Commands;
using Looplex.OpenForExtension.Abstractions.Contexts;
using MutantNinjaTurtlePluginTests.Mocks;

namespace MutantNinjaTurtlePluginTests.Commands
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
            dynamic hare = new ExpandoObject();
            hare.Speed = 1;
            hare.Endurance = 2;
            dynamic tortoise = new ExpandoObject();
            tortoise.Speed = 0;
            tortoise.Endurance = 0;
            actors.Add("Tortoise", tortoise);
            actors.Add("Hare", hare);
            context.Roles.Returns(actors);

            // Act
            new RaceService().StartRaceAsync(() => plugin.Execute<IDefineRoles>(context, CancellationToken.None));

            // Assert
            var expectedSpeed = context.Roles["Hare"].Speed * 2;
            var expectedEndurance = context.Roles["Hare"].Endurance * 2;
            
            Assert.AreEqual(expectedSpeed, context.Roles["Tortoise"].Speed);
            Assert.AreEqual(expectedEndurance, context.Roles["Tortoise"].Endurance);
        }
    }
}