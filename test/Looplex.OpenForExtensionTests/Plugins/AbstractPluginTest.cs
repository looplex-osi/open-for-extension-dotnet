using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;
using Looplex.OpenForExtension.Plugins;
using Looplex.OpenForExtensionTests.Commands;
using NSubstitute;

namespace Looplex.OpenForExtensionTests.Plugins
{
    [TestClass]
    public class AbstractPluginTest
    {
        private AbstractPlugin? plugin;

        [TestInitialize] public void Init() 
        {
            plugin = Substitute.ForPartsOf<AbstractPlugin>();
        }

        [TestMethod]
        public void TryExecute_CommandExists_AllCommandsOfTypeAreExecuted()
        {
            // Prepare
            var existingCommandMock1 = Substitute.For<ITestCommand1>();
            var existingCommandMock2 = Substitute.For<ITestCommand1>();
            var existingCommandMock3 = Substitute.For<ITestCommand3>();
            plugin!.Commands.Returns(
            [
                existingCommandMock1,
                existingCommandMock2,
                existingCommandMock3,
            ]);
            var context = Substitute.For<IDefaultContext>();

            // Act            
            plugin.TryExecute<ITestCommand1>(context);

            // Assert
            existingCommandMock1.Received(1).Execute(Arg.Is<IDefaultContext>(c => c == context));
            existingCommandMock2.Received(1).Execute(Arg.Is<IDefaultContext>(c => c == context));
            existingCommandMock3.DidNotReceive().Execute(Arg.Any<IDefaultContext>());
        }

        [TestMethod]
        public void TryExecute_CommandDoesNotExists_NoCommandIsExecuted()
        {
            // Prepare
            var existingCommandMock1 = Substitute.For<ITestCommand1>();
            var existingCommandMock2 = Substitute.For<ITestCommand2>();
            var existingCommandMock3 = Substitute.For<ITestCommand3>();
            plugin!.Commands.Returns(
            [
                existingCommandMock1,
                existingCommandMock2,
                existingCommandMock3,
            ]);
            var context = Substitute.For<IDefaultContext>();

            // Act            
            plugin.TryExecute<ITestCommand4>(context);

            // Assert
            existingCommandMock1.DidNotReceive().Execute(Arg.Any<IDefaultContext>());
            existingCommandMock2.DidNotReceive().Execute(Arg.Any<IDefaultContext>());
            existingCommandMock3.DidNotReceive().Execute(Arg.Any<IDefaultContext>());
        }

        [TestMethod]
        public async Task TryExecuteAsync_CommandExists_AllCommandsOfTypeAreExecuted()
        {
            // Prepare
            var existingCommandMock1 = Substitute.For<ITestCommand1>();
            var existingCommandMock2 = Substitute.For<ITestCommand1>();
            var existingCommandMock3 = Substitute.For<ITestCommand3>();
            plugin!.Commands.Returns(
            [
                existingCommandMock1,
                existingCommandMock2,
                existingCommandMock3,
            ]);
            var context = Substitute.For<IDefaultContext>();

            // Act            
            await plugin.TryExecuteAsync<ITestCommand1>(context);

            // Assert
            existingCommandMock1.Received(1).Execute(Arg.Is<IDefaultContext>(c => c == context));
            existingCommandMock2.Received(1).Execute(Arg.Is<IDefaultContext>(c => c == context));
            existingCommandMock3.DidNotReceive().Execute(Arg.Any<IDefaultContext>());
        }

        [TestMethod]
        public async Task TryExecuteAsync_CommandDoesNotExists_NoCommandIsExecuted()
        {
            // Prepare
            var existingCommandMock1 = Substitute.For<ITestCommand1>();
            var existingCommandMock2 = Substitute.For<ITestCommand2>();
            var existingCommandMock3 = Substitute.For<ITestCommand3>();
            plugin!.Commands.Returns(
            [
                existingCommandMock1,
                existingCommandMock2,
                existingCommandMock3,
            ]);
            var context = Substitute.For<IDefaultContext>();

            // Act            
            await plugin.TryExecuteAsync<ITestCommand5>(context);

            // Assert
            existingCommandMock1.DidNotReceive().Execute(Arg.Any<IDefaultContext>());
            existingCommandMock2.DidNotReceive().Execute(Arg.Any<IDefaultContext>());
            existingCommandMock3.DidNotReceive().Execute(Arg.Any<IDefaultContext>());
        }
    }
}