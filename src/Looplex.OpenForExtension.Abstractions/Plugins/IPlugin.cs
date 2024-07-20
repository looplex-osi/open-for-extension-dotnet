using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Looplex.OpenForExtension.Abstractions.Commands;
using Looplex.OpenForExtension.Abstractions.Contexts;

namespace Looplex.OpenForExtension.Abstractions.Plugins
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }
        IEnumerable<ICommand> Commands { get; }

        Task ExecuteAsync<T>(IContext context, CancellationToken cancellationToken) where T : ICommand;
        void Execute<T>(IContext context, CancellationToken cancellationToken) where T : ICommand;
        IEnumerable<string> GetSubscriptions();
    }
}