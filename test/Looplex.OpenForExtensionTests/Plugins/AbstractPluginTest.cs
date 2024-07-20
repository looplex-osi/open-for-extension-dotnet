using Looplex.OpenForExtension.Abstractions.Contexts;
using Looplex.OpenForExtension.Plugins;
using Looplex.OpenForExtensionTests.Commands;
using NSubstitute;

namespace Looplex.OpenForExtensionTests.Plugins
{
    [TestClass]
    public class AbstractPluginTest
    {
        private AbstractPlugin? _plugin;

        [TestInitialize] public void Init() 
        {
            _plugin = Substitute.ForPartsOf<AbstractPlugin>();
        }

        [TestMethod]
        public void TryExecute_CommandExists_AllCommandsOfTypeAreExecuted()
        {
            // Prepare
            var existingCommandMock1 = Substitute.For<ITestCommand1>();
            var existingCommandMock2 = Substitute.For<ITestCommand1>();
            var existingCommandMock3 = Substitute.For<ITestCommand3>();
            _plugin!.Commands.Returns(
            [
                existingCommandMock1,
                existingCommandMock2,
                existingCommandMock3,
            ]);
            var context = Substitute.For<IContext>();

            // Act            
            _plugin.Execute<ITestCommand1>(context, CancellationToken.None);

            // Assert
            existingCommandMock1.Received(1).ExecuteAsync(Arg.Is<IContext>(c => c == context), CancellationToken.None);
            existingCommandMock2.Received(1).ExecuteAsync(Arg.Is<IContext>(c => c == context), CancellationToken.None);
            existingCommandMock3.DidNotReceive().ExecuteAsync(Arg.Any<IContext>(), CancellationToken.None);
        }

        [TestMethod]
        public void TryExecute_CommandDoesNotExists_NoCommandIsExecuted()
        {
            // Prepare
            var existingCommandMock1 = Substitute.For<ITestCommand1>();
            var existingCommandMock2 = Substitute.For<ITestCommand2>();
            var existingCommandMock3 = Substitute.For<ITestCommand3>();
            _plugin!.Commands.Returns(
            [
                existingCommandMock1,
                existingCommandMock2,
                existingCommandMock3,
            ]);
            var context = Substitute.For<IContext>();

            // Act            
            _plugin.Execute<ITestCommand4>(context, CancellationToken.None);

            // Assert
            existingCommandMock1.DidNotReceive().ExecuteAsync(Arg.Any<IContext>(), CancellationToken.None);
            existingCommandMock2.DidNotReceive().ExecuteAsync(Arg.Any<IContext>(), CancellationToken.None);
            existingCommandMock3.DidNotReceive().ExecuteAsync(Arg.Any<IContext>(), CancellationToken.None);
        }

        [TestMethod]
        public async Task TryExecuteAsync_CommandExists_AllCommandsOfTypeAreExecuted()
        {
            // Prepare
            var existingCommandMock1 = Substitute.For<ITestCommand1>();
            var existingCommandMock2 = Substitute.For<ITestCommand1>();
            var existingCommandMock3 = Substitute.For<ITestCommand3>();
            _plugin!.Commands.Returns(
            [
                existingCommandMock1,
                existingCommandMock2,
                existingCommandMock3,
            ]);
            var context = Substitute.For<IContext>();

            // Act            
            await _plugin.ExecuteAsync<ITestCommand1>(context, CancellationToken.None);

            // Assert
            await existingCommandMock1.Received(1).ExecuteAsync(Arg.Is<IContext>(c => c == context), CancellationToken.None);
            await existingCommandMock2.Received(1).ExecuteAsync(Arg.Is<IContext>(c => c == context), CancellationToken.None);
            await existingCommandMock3.DidNotReceive().ExecuteAsync(Arg.Any<IContext>(), CancellationToken.None);
        }

        [TestMethod]
        public async Task TryExecuteAsync_CommandDoesNotExists_NoCommandIsExecuted()
        {
            // Prepare
            var existingCommandMock1 = Substitute.For<ITestCommand1>();
            var existingCommandMock2 = Substitute.For<ITestCommand2>();
            var existingCommandMock3 = Substitute.For<ITestCommand3>();
            _plugin!.Commands.Returns(
            [
                existingCommandMock1,
                existingCommandMock2,
                existingCommandMock3,
            ]);
            var context = Substitute.For<IContext>();

            // Act            
            await _plugin.ExecuteAsync<ITestCommand5>(context, CancellationToken.None);

            // Assert
            await existingCommandMock1.DidNotReceive().ExecuteAsync(Arg.Any<IContext>(), CancellationToken.None);
            await existingCommandMock2.DidNotReceive().ExecuteAsync(Arg.Any<IContext>(), CancellationToken.None);
            await existingCommandMock3.DidNotReceive().ExecuteAsync(Arg.Any<IContext>(), CancellationToken.None);
        }
    }
}