using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;
using Moq;

namespace Looplex.OpenForExtensionTests
{
    public abstract class OpenForExtensionTestsBase
    {
        internal static Mock<T> MockCommand<T>() where T : class, ICommand
        {
            var commandMock = new Mock<T>(MockBehavior.Strict);
            commandMock.Setup(c => c.Name).Returns(Guid.NewGuid().ToString());
            commandMock.Setup(c => c.Execute(It.IsAny<IPluginContext>()));
            return commandMock;
        }
    }
}
