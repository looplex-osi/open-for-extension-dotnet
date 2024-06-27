using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;
using Looplex.OpenForExtension.Plugins;
using Looplex.OpenForExtensionTests.Commands;
using Moq;

namespace Looplex.OpenForExtensionTests.Plugins
{
    [TestClass]
    public class AbstractPluginTest : OpenForExtensionTestsBase
    {
        private Mock<AbstractPlugin>? _pluginMock;

        [TestInitialize] public void Init() 
        {
            _pluginMock = new Mock<AbstractPlugin>
            {
                CallBase = true
            };
        }

        [TestMethod]
        public void TryExecute_CommandExists_AllCommandsOfTypeAreExecuted()
        {
            // Prepare
            var existingCommandMock1 = MockCommand<ITestCommand1>();
            var existingCommandMock2 = MockCommand<ITestCommand1>();
            var existingCommandMock3 = MockCommand<ITestCommand3>();
            _pluginMock!.Setup(p => p.Commands).Returns(new ICommand[]
            {
                existingCommandMock1.Object,
                existingCommandMock2.Object,
                existingCommandMock3.Object,
            });
            var plugin = _pluginMock.Object;
            var context = new Mock<IPluginContext>().Object;

            // Act            
            plugin.TryExecute<ITestCommand1>(context);

            // Assert
            existingCommandMock1.Verify(c => c.Execute(It.Is<IPluginContext>(c => c == context)), Times.Once);
            existingCommandMock2.Verify(c => c.Execute(It.Is<IPluginContext>(c => c == context)), Times.Once);
            existingCommandMock3.Verify(c => c.Execute(It.IsAny<IPluginContext>()), Times.Never);
        }

        [TestMethod]
        public void TryExecute_CommandDoesNotExists_NoCommandIsExecuted()
        {
            // Prepare
            var existingCommandMock1 = MockCommand<ITestCommand1>();
            var existingCommandMock2 = MockCommand<ITestCommand2>();
            var existingCommandMock3 = MockCommand<ITestCommand3>();
            _pluginMock!.Setup(p => p.Commands).Returns(new ICommand[]
            {
                existingCommandMock1.Object,
                existingCommandMock2.Object,
                existingCommandMock3.Object,
            });
            var plugin = _pluginMock.Object;
            var context = new Mock<IPluginContext>().Object;

            // Act            
            plugin.TryExecute<ITestCommand4>(context);

            // Assert
            existingCommandMock1.Verify(c => c.Execute(It.IsAny<IPluginContext>()), Times.Never);
            existingCommandMock2.Verify(c => c.Execute(It.IsAny<IPluginContext>()), Times.Never);
            existingCommandMock3.Verify(c => c.Execute(It.IsAny<IPluginContext>()), Times.Never);
        }

        [TestMethod]
        public async Task TryExecuteAsync_CommandExists_AllCommandsOfTypeAreExecuted()
        {
            // Prepare
            var existingCommandMock1 = MockCommand<ITestCommand1>();
            var existingCommandMock2 = MockCommand<ITestCommand1>();
            var existingCommandMock3 = MockCommand<ITestCommand3>();
            _pluginMock!.Setup(p => p.Commands).Returns(new ICommand[]
            {
                existingCommandMock1.Object,
                existingCommandMock2.Object,
                existingCommandMock3.Object,
            });
            var plugin = _pluginMock.Object;
            var context = new Mock<IPluginContext>().Object;

            // Act            
            await plugin.TryExecuteAsync<ITestCommand1>(context);

            // Assert
            existingCommandMock1.Verify(c => c.Execute(It.Is<IPluginContext>(c => c == context)), Times.Once);
            existingCommandMock2.Verify(c => c.Execute(It.Is<IPluginContext>(c => c == context)), Times.Once);
            existingCommandMock3.Verify(c => c.Execute(It.IsAny<IPluginContext>()), Times.Never);
        }

        [TestMethod]
        public async Task TryExecuteAsync_CommandDoesNotExists_NoCommandIsExecuted()
        {
            // Prepare
            var existingCommandMock1 = MockCommand<ITestCommand1>();
            var existingCommandMock2 = MockCommand<ITestCommand2>();
            var existingCommandMock3 = MockCommand<ITestCommand3>();
            _pluginMock!.Setup(p => p.Commands).Returns(new ICommand[]
            {
                existingCommandMock1.Object,
                existingCommandMock2.Object,
                existingCommandMock3.Object,
            });
            var plugin = _pluginMock.Object;
            var context = new Mock<IPluginContext>().Object;

            // Act            
            await plugin.TryExecuteAsync<ITestCommand5>(context);

            // Assert
            existingCommandMock1.Verify(c => c.Execute(It.IsAny<IPluginContext>()), Times.Never);
            existingCommandMock2.Verify(c => c.Execute(It.IsAny<IPluginContext>()), Times.Never);
            existingCommandMock3.Verify(c => c.Execute(It.IsAny<IPluginContext>()), Times.Never);
        }
    }
}