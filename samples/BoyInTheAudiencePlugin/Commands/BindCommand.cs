using System.Diagnostics;
using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;

namespace BoyInTheAudiencePlugin.Commands
{
    internal class BindCommand : IBind
    {
        public string Name => "Bind boy events";

        public string Description => "The boy will cheer for the tortoise";

        public Task ExecuteAsync(IDefaultContext context)
        {
            if (new StackTrace().GetFrames()
                .Select(f => $"{f.GetMethod()?.DeclaringType?.Name}.{f.GetMethod()?.Name}")
                .Any(caller => caller == "RaceService.StartRaceAsync"))
            {
                context.Actors["Hare"].On("IsExausted", (EventHandler)HareIsExausted);

                void TortoiseFinishedTheRace(object? sender, EventArgs e)
                {
                    if (!((IDictionary<string, object>)context.State).ContainsKey("HareFinishTime"))
                    {
                        context.Actors["BoyInTheAudience"].Celebrate();
                    }
                    else
                    {
                        context.Actors["BoyInTheAudience"].Cry();
                    }
                }

                context.Actors["Tortoise"].On("FinishedTheRace", (EventHandler)TortoiseFinishedTheRace);
            }
            return Task.CompletedTask;
            
            void HareIsExausted(object? sender, EventArgs e)
            {
                if (!((IDictionary<string, object>)context.State).ContainsKey("TortoiseFinishTime"))
                {
                    context.Actors["BoyInTheAudience"].Cheer();
                }
            }
        }
    }
}
