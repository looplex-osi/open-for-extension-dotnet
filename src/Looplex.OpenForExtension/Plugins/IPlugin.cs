using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Looplex.OpenForExtension.Plugins
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }
        IEnumerable<ICommand> Commands { get; }

        Task TryExecuteAsync<T>(IDefaultContext context, CancellationToken cancellationToken) where T : ICommand;
        void TryExecute<T>(IDefaultContext context, CancellationToken cancellationToken) where T : ICommand;
        IEnumerable<string> GetSubscriptions();
    }
}