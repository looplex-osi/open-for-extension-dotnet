using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;
using Looplex.OpenForExtension.ExtensionMethods;
using Looplex.OpenForExtension.Plugins;
using Looplex.OpenForExtensionTests.Commands;
using NSubstitute;

namespace Looplex.OpenForExtensionTests.Context
{
    [TestClass]
    public class AbstractPluginContextTest
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
            var context = Substitute.For<IDefaultContext>();
            context.Plugins.Returns(plugins);

            // Act
            context.Plugins.Execute<ITestCommand1>(context);

            // Assert
            existingCommandMock1.Received(1).Execute(Arg.Is<IDefaultContext>(c => c == context));
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
            var context = Substitute.For<IDefaultContext>();
            context.Plugins.Returns(plugins);

            // Act
            await context.Plugins.ExecuteAsync<ITestCommand1>(context);

            // Assert
            existingCommandMock1.Received(1).Execute(Arg.Is<IDefaultContext>(c => c == context));
            existingCommandMock2.DidNotReceive().Execute(Arg.Any<IDefaultContext>());
            existingCommandMock3.Received(1).Execute(Arg.Is<IDefaultContext>(c => c == context));
            existingCommandMock4.DidNotReceive().Execute(Arg.Any<IDefaultContext>());
            existingCommandMock5.DidNotReceive().Execute(Arg.Any<IDefaultContext>());
        }

        private static IPlugin MockPluginWithCommands(IEnumerable<ICommand> commands)
        {
            var pluginMock = Substitute.For<AbstractPlugin>();
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
            var context = Substitute.For<IDefaultContext>();
            context.Plugins.Returns(plugins);

            // Act
            await context.Plugins.ExecuteAsync<ITestCommand3>(context);

            // Assert
            existingCommandMock1.DidNotReceive().Execute(Arg.Any<IDefaultContext>());
            existingCommandMock2.DidNotReceive().Execute(Arg.Any<IDefaultContext>());
            existingCommandMock3.DidNotReceive().Execute(Arg.Any<IDefaultContext>());
            existingCommandMock4.DidNotReceive().Execute(Arg.Any<IDefaultContext>());
            existingCommandMock5.DidNotReceive().Execute(Arg.Any<IDefaultContext>());
        }
    }
}