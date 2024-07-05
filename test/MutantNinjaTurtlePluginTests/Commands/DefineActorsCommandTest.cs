using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;
using MutantNinjaTurtlePlugin;
using NSubstitute;
using System.Dynamic;

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
            var context = Substitute.For<IDefaultContext>();
            var actors = new Dictionary<string, dynamic>();
            dynamic hare = new ExpandoObject();
            hare.Speed = 1;
            hare.Endurance = 2;
            dynamic tortoise = new ExpandoObject();
            tortoise.Speed = 0;
            tortoise.Endurance = 0;
            actors.Add("Tortoise", tortoise);
            actors.Add("Hare", hare);
            context.Actors.Returns(actors);

            // Act
            plugin.TryExecute<IDefineActors>(context);

            // Assert
            var expectedSpeed = context.Actors["Hare"].Speed * 2;
            var expectedEndurance = context.Actors["Hare"].Endurance * 2;
            
            Assert.AreEqual(expectedSpeed, context.Actors["Tortoise"].Speed);
            Assert.AreEqual(expectedEndurance, context.Actors["Tortoise"].Endurance);
        }
    }
}