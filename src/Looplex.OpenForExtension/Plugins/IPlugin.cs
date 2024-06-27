using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Looplex.OpenForExtension.Plugins
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }
        IEnumerable<ICommand> Commands { get; }

        Task TryExecuteAsync<T>(IPluginContext commandContext) where T : ICommand;
        void TryExecute<T>(IPluginContext commandContext) where T : ICommand;
    }
}