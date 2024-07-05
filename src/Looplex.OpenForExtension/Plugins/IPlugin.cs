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

        Task TryExecuteAsync<T>(IDefaultContext context) where T : ICommand;
        void TryExecute<T>(IDefaultContext context) where T : ICommand;
    }
}