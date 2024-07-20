using System.Diagnostics;
using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;

namespace MutantNinjaTurtlePlugin.Commands
{
    internal class DefineActorsCommand : IDefineActors
    {
        public string Name => "Changes tortoise to ninja turtle";

        public string Description => "Modifies the tortoise to be invincible";

        public Task ExecuteAsync(IDefaultContext context, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            if (new StackTrace().GetFrames()
                .Select(f => $"{f.GetMethod()?.DeclaringType?.Name}.{f.GetMethod()?.Name}")
                .Any(caller => caller == "RaceService.StartRaceAsync"))
            {
                dynamic tortoise = context.Actors["Tortoise"];
                dynamic hare = context.Actors["Hare"];

                tortoise.Speed = hare.Speed * 2;
                tortoise.Endurance = hare.Endurance * 2;
                tortoise.Name = "Ninja turtle";
            }

            return Task.CompletedTask;
        }
    }
}
