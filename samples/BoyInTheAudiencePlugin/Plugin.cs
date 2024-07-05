using BoyInTheAudiencePlugin.Commands;
using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Plugins;

namespace BoyInTheAudiencePlugin
{
    public class Plugin : AbstractPlugin
    {
        public override string? Name => "Plugin name";

        public override string? Description => "Plugin description";

        public override IEnumerable<ICommand> Commands =>
        [
            new DefineActorsCommand(),
            new BindCommand()
        ];
    }
}