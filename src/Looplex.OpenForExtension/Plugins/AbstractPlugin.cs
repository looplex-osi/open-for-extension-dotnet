using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looplex.OpenForExtension.Plugins
{
    public abstract class AbstractPlugin : IPlugin
    {
        public abstract string Name { get; }

        public abstract string Description { get; }
        
        public abstract IEnumerable<ICommand> Commands { get; }

        public void TryExecute<T>(IPluginContext commandContext) where T : ICommand
        {
            foreach (var command in Commands.Where(c => typeof(T).IsAssignableFrom(c.GetType())))
            {
                command.Execute(commandContext);
            }
        }

        public Task TryExecuteAsync<T>(IPluginContext commandContext) where T : ICommand
        {
            return Task.Run(() =>
            {
                TryExecute<T>(commandContext);
            });
        }
    }
}
