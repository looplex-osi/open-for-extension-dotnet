using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;

namespace PluginSample.Commands
{
    internal class HandleInputCommand : IHandleInput
    {
        public string Name => "HandleInputCommand name";

        public string Description => "HandleInputCommand description";

        public void Execute(IPluginContext commandContext)
        {            
            if (commandContext is not IDefaultContext) 
            {
                throw new ArgumentException(nameof(commandContext));
            }
            var context = commandContext as IDefaultContext;

            Console.WriteLine($"Execute: {Name} - {Description}");
            Console.WriteLine($"SkipDefaultAction: {context!.SkipDefaultAction}");
        }
    }
}
