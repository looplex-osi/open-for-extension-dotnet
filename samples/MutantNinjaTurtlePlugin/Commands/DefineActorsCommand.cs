using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;

namespace MutantNinjaTurtlePlugin.Commands
{
    internal class DefineActorsCommand : IDefineActors
    {
        public string Name => "Changes tortoise to ninja turtle";

        public string Description => "Modifies the tortoise to be invincible";

        public void Execute(IDefaultContext context)
        {
            dynamic tortoise = context.Actors["Tortoise"];
            dynamic hare = context.Actors["Hare"];

            tortoise.Speed = hare.Speed * 2;
            tortoise.Endurance = hare.Endurance * 2;
        }
    }
}
