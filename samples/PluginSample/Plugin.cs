using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Plugins;
using PluginSample.Commands;

namespace PluginSample
{
    public class Plugin : AbstractPlugin
    {
        public override string? Name => "Plugin name";

        public override string? Description => "Plugin description";

        public override IEnumerable<ICommand> Commands => new ICommand[]
        {
            new HandleInputCommand()
        };
    }
}