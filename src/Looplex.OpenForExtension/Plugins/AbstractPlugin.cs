using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Looplex.OpenForExtension.Plugins
{
    public abstract class AbstractPlugin : IPlugin
    {
        public abstract string Name { get; }

        public abstract string Description { get; }
        
        public abstract IEnumerable<ICommand> Commands { get; }

        public virtual void TryExecute<T>(IDefaultContext context, CancellationToken cancellationToken) where T : ICommand
        {
            TryExecuteAsync<T>(context, cancellationToken).GetAwaiter().GetResult();
        }

        public virtual async Task TryExecuteAsync<T>(IDefaultContext context, CancellationToken cancellationToken) where T : ICommand
        {
            foreach (var command in Commands.Where(c => typeof(T).IsAssignableFrom(c.GetType())))
            {
                await command.ExecuteAsync(context, cancellationToken);
            }
        }

        public abstract IEnumerable<string> GetSubscriptions();
    }
}
