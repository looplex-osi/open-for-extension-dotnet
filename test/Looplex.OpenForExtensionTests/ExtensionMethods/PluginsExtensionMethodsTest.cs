using Looplex.OpenForExtension.Abstractions.Commands;
using Looplex.OpenForExtension.Abstractions.Contexts;
using Looplex.OpenForExtension.Abstractions.ExtensionMethods;
using Looplex.OpenForExtension.Abstractions.Plugins;
using Looplex.OpenForExtension.Plugins;
using Looplex.OpenForExtensionTests.Commands;
using NSubstitute;

namespace Looplex.OpenForExtensionTests.ExtensionMethods
{
    [TestClass]
    public class PluginsExtensionMethodsTest
    {

        [TestMethod]
        public void Execute_PluginHaveCommandType_CommandIsExecuted()
        {
            // Prepare 
            var existingCommandMock1 = Substitute.For<ITestCommand1>();
            var plugins = new List<IPlugin>
            {
                MockPluginWithCommands(
                [
                    existingCommandMock1,
                ]),
            };
            var context = Substitute.For<IContext>();
            context.Plugins.Returns(plugins);

            // Act
            context.Plugins.Execute<ITestCommand1>(context, CancellationToken.None);

            // Assert
            existingCommandMock1.Received(1).ExecuteAsync(Arg.Is<IContext>(c => c == context), CancellationToken.None);
        }

        [TestMethod]
        public async Task ExecuteAsync_PluginsHaveCommandType_CommandsAreExecuted()
        {
            // Prepare 
            var existingCommandMock1 = Substitute.For<ITestCommand1>();
            var existingCommandMock2 = Substitute.For<ITestCommand2>();
            var existingCommandMock3 = Substitute.For<ITestCommand1>();
            var existingCommandMock4 = Substitute.For<ITestCommand4>();
            var existingCommandMock5 = Substitute.For<ITestCommand5>();
            var plugins = new List<IPlugin>
            {
                MockPluginWithCommands(new ICommand[]
                {
                    existingCommandMock1,
                    existingCommandMock2,
                }),
                MockPluginWithCommands(new ICommand[]
                {
                    existingCommandMock3,
                    existingCommandMock4,
                    existingCommandMock5,
                })
            };
            var context = Substitute.For<IContext>();
            context.Plugins.Returns(plugins);

            // Act
            await context.Plugins.ExecuteAsync<ITestCommand1>(context, CancellationToken.None);

            // Assert
            await existingCommandMock1.Received(1).ExecuteAsync(Arg.Is<IContext>(c => c == context), CancellationToken.None);
            await existingCommandMock2.DidNotReceive().ExecuteAsync(Arg.Any<IContext>(), CancellationToken.None);
            await existingCommandMock3.Received(1).ExecuteAsync(Arg.Is<IContext>(c => c == context), CancellationToken.None);
            await existingCommandMock4.DidNotReceive().ExecuteAsync(Arg.Any<IContext>(), CancellationToken.None);
            await existingCommandMock5.DidNotReceive().ExecuteAsync(Arg.Any<IContext>(), CancellationToken.None);
        }

        private static IPlugin MockPluginWithCommands(IEnumerable<ICommand> commands)
        {
            var pluginMock = Substitute.ForPartsOf<AbstractPlugin>();
            pluginMock!.Commands.Returns(commands);
            return pluginMock;
        }

        [TestMethod]
        public async Task ExecuteAsync_PluginsDoesNotHaveCommandType_CommandsAreNotExecuted()
        {
            // Prepare 
            var existingCommandMock1 = Substitute.For<ITestCommand1>();
            var existingCommandMock2 = Substitute.For<ITestCommand2>();
            var existingCommandMock3 = Substitute.For<ITestCommand1>();
            var existingCommandMock4 = Substitute.For<ITestCommand4>();
            var existingCommandMock5 = Substitute.For<ITestCommand5>();
            var plugins = new List<IPlugin>
            {
                MockPluginWithCommands(new ICommand[]
                {
                    existingCommandMock1,
                    existingCommandMock2,
                }),
                MockPluginWithCommands(new ICommand[]
                {
                    existingCommandMock3,
                    existingCommandMock4,
                    existingCommandMock5,
                })
            };
            var context = Substitute.For<IContext>();
            context.Plugins.Returns(plugins);

            // Act
            await context.Plugins.ExecuteAsync<ITestCommand3>(context, CancellationToken.None);

            // Assert
            await existingCommandMock1.DidNotReceive().ExecuteAsync(Arg.Any<IContext>(), CancellationToken.None);
            await existingCommandMock2.DidNotReceive().ExecuteAsync(Arg.Any<IContext>(), CancellationToken.None);
            await existingCommandMock3.DidNotReceive().ExecuteAsync(Arg.Any<IContext>(), CancellationToken.None);
            await existingCommandMock4.DidNotReceive().ExecuteAsync(Arg.Any<IContext>(), CancellationToken.None);
            await existingCommandMock5.DidNotReceive().ExecuteAsync(Arg.Any<IContext>(), CancellationToken.None);
        }
    }
}