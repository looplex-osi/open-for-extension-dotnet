using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;
using Looplex.OpenForExtension.Plugins;
using Looplex.OpenForExtension.ExtensionMethods;
using Moq;
using Looplex.OpenForExtensionTests.Commands;

namespace Looplex.OpenForExtensionTests.Context
{
    [TestClass]
    public class AbstractPluginContextTest : OpenForExtensionTestsBase
    {

        [TestMethod]
        public void Execute_PluginHaveCommandType_CommandIsExecuted()
        {
            // Prepare 
            var existingCommandMock1 = MockCommand<ITestCommand1>();
            var plugins = new List<IPlugin>
            {
                MockPluginWithCommands(new ICommand[]
                {
                    existingCommandMock1.Object,
                }),
            };
            var contextMock = new Mock<IPluginContext>(MockBehavior.Strict);
            contextMock.Setup(c => c.Plugins).Returns(plugins);
            var context = contextMock.Object;

            // Act
            context.Plugins.Execute<ITestCommand1>(context);

            // Assert
            existingCommandMock1.Verify(c => c.Execute(It.Is<IPluginContext>(c => c == context)), Times.Once);
        }

        [TestMethod]
        public async Task ExecuteAsync_PluginsHaveCommandType_CommandsAreExecuted()
        {
            // Prepare 
            var existingCommandMock1 = MockCommand<ITestCommand1>();
            var existingCommandMock2 = MockCommand<ITestCommand2>();
            var existingCommandMock3 = MockCommand<ITestCommand1>();
            var existingCommandMock4 = MockCommand<ITestCommand4>();
            var existingCommandMock5 = MockCommand<ITestCommand5>();
            var plugins = new List<IPlugin>
            {
                MockPluginWithCommands(new ICommand[]
                {
                    existingCommandMock1.Object,
                    existingCommandMock2.Object,
                }),
                MockPluginWithCommands(new ICommand[]
                {
                    existingCommandMock3.Object,
                    existingCommandMock4.Object,
                    existingCommandMock5.Object,
                })
            };
            var contextMock = new Mock<IPluginContext>(MockBehavior.Strict);
            contextMock.Setup(c => c.Plugins).Returns(plugins);
            var context = contextMock.Object;

            // Act
            await context.Plugins.ExecuteAsync<ITestCommand1>(context);

            // Assert
            existingCommandMock1.Verify(c => c.Execute(It.Is<IPluginContext>(c => c == context)), Times.Once);
            existingCommandMock2.Verify(c => c.Execute(It.IsAny<IPluginContext>()), Times.Never);
            existingCommandMock3.Verify(c => c.Execute(It.Is<IPluginContext>(c => c == context)), Times.Once);
            existingCommandMock4.Verify(c => c.Execute(It.IsAny<IPluginContext>()), Times.Never);
            existingCommandMock5.Verify(c => c.Execute(It.IsAny<IPluginContext>()), Times.Never);
        }

        private static IPlugin MockPluginWithCommands(IEnumerable<ICommand> commands)
        {
            var pluginMock = new Mock<AbstractPlugin>();
            pluginMock!.Setup(p => p.Commands).Returns(commands);
            return pluginMock.Object;
        }

        [TestMethod]
        public async Task ExecuteAsync_PluginsDoesNotHaveCommandType_CommandsAreNotExecuted()
        {
            // Prepare 
            var existingCommandMock1 = MockCommand<ITestCommand1>();
            var existingCommandMock2 = MockCommand<ITestCommand2>();
            var existingCommandMock3 = MockCommand<ITestCommand1>();
            var existingCommandMock4 = MockCommand<ITestCommand4>();
            var existingCommandMock5 = MockCommand<ITestCommand5>();
            var plugins = new List<IPlugin>
            {
                MockPluginWithCommands(new ICommand[]
                {
                    existingCommandMock1.Object,
                    existingCommandMock2.Object,
                }),
                MockPluginWithCommands(new ICommand[]
                {
                    existingCommandMock3.Object,
                    existingCommandMock4.Object,
                    existingCommandMock5.Object,
                })
            };
            var contextMock = new Mock<IPluginContext>(MockBehavior.Strict);
            contextMock.Setup(c => c.Plugins).Returns(plugins);
            var context = contextMock.Object;

            // Act
            await context.Plugins.ExecuteAsync<ITestCommand3>(context);

            // Assert
            existingCommandMock1.Verify(c => c.Execute(It.IsAny<IPluginContext>()), Times.Never);
            existingCommandMock2.Verify(c => c.Execute(It.IsAny<IPluginContext>()), Times.Never);
            existingCommandMock3.Verify(c => c.Execute(It.IsAny<IPluginContext>()), Times.Never);
            existingCommandMock4.Verify(c => c.Execute(It.IsAny<IPluginContext>()), Times.Never);
            existingCommandMock5.Verify(c => c.Execute(It.IsAny<IPluginContext>()), Times.Never);
        }
    }
}