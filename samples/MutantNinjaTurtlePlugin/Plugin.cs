using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Plugins;
using MutantNinjaTurtlePlugin.Commands;

namespace MutantNinjaTurtlePlugin
{
    public class Plugin : AbstractPlugin
    {
        public override string? Name => "Plugin name";

        public override string? Description => "Plugin description";

        public override IEnumerable<ICommand> Commands =>
        [
            new DefineActorsCommand()
        ];

        public override IEnumerable<string> GetSubscriptions()=>
        [
            "RaceService.StartRace"
        ];
    }
}