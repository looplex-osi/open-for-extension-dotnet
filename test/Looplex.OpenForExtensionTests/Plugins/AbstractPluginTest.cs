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
            plugin.TryExecute<ITestCommand1>(context, CancellationToken.None);

            // Assert
            existingCommandMock1.Received(1).ExecuteAsync(Arg.Is<IDefaultContext>(c => c == context), CancellationToken.None);
            existingCommandMock2.Received(1).ExecuteAsync(Arg.Is<IDefaultContext>(c => c == context), CancellationToken.None);
            existingCommandMock3.DidNotReceive().ExecuteAsync(Arg.Any<IDefaultContext>(), CancellationToken.None);
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
            plugin.TryExecute<ITestCommand4>(context, CancellationToken.None);

            // Assert
            existingCommandMock1.DidNotReceive().ExecuteAsync(Arg.Any<IDefaultContext>(), CancellationToken.None);
            existingCommandMock2.DidNotReceive().ExecuteAsync(Arg.Any<IDefaultContext>(), CancellationToken.None);
            existingCommandMock3.DidNotReceive().ExecuteAsync(Arg.Any<IDefaultContext>(), CancellationToken.None);
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
            await plugin.TryExecuteAsync<ITestCommand1>(context, CancellationToken.None);

            // Assert
            await existingCommandMock1.Received(1).ExecuteAsync(Arg.Is<IDefaultContext>(c => c == context), CancellationToken.None);
            await existingCommandMock2.Received(1).ExecuteAsync(Arg.Is<IDefaultContext>(c => c == context), CancellationToken.None);
            await existingCommandMock3.DidNotReceive().ExecuteAsync(Arg.Any<IDefaultContext>(), CancellationToken.None);
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
            await plugin.TryExecuteAsync<ITestCommand5>(context, CancellationToken.None);

            // Assert
            await existingCommandMock1.DidNotReceive().ExecuteAsync(Arg.Any<IDefaultContext>(), CancellationToken.None);
            await existingCommandMock2.DidNotReceive().ExecuteAsync(Arg.Any<IDefaultContext>(), CancellationToken.None);
            await existingCommandMock3.DidNotReceive().ExecuteAsync(Arg.Any<IDefaultContext>(), CancellationToken.None);
        }
    }
}