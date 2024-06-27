using Looplex.OpenForExtension.Context;

namespace Looplex.OpenForExtension.Commands
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }
        void Execute(IPluginContext commandContext);
    }
}