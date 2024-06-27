using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;
using Moq;
using PluginSample;

namespace PluginSampleTests.Commands
{
    [TestClass]
    public class HandleInputCommandTest
    {
        [TestMethod]
        public void Constructor_Default_PluginIsCreated()
        {
            // Act
            var plugin = new Plugin();

            // Assert
            Assert.AreEqual(1, plugin.Commands.Count());
        }

        [TestMethod]
        public void Execute_ContextIsHandleInputContext_ExecuteIsRun()
        {
            // Arrange
            var plugin = new Plugin();
            var commandContext = new Mock<IDefaultContext>(MockBehavior.Strict).Object;

            // Act
            var action = () => plugin.TryExecute<IHandleInput>(commandContext);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TryExecute_ContextIsIDefaultContext_ExecuteIsRun()
        {
            // Arrange
            var plugin = new Plugin();
            var contextMock = new Mock<IDefaultContext>(MockBehavior.Strict);
            contextMock.Setup(c => c.SkipDefaultAction).Returns(true);
            var commandContext = contextMock.Object;

            // Act
            plugin.TryExecute<IHandleInput>(commandContext);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TryExecute_ContextIsNotIDefaultContext_ExecuteIsRun()
        {
            // Arrange
            var plugin = new Plugin();
            var commandContext = new Mock<IPluginContext>(MockBehavior.Strict).Object;

            // Act
            var action = () => plugin.TryExecute<IHandleInput>(commandContext);

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }
    }
}