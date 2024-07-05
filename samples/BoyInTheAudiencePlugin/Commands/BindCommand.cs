using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;

namespace BoyInTheAudiencePlugin.Commands
{
    internal class BindCommand : IBind
    {
        public string Name => "Bind boy events";

        public string Description => "The boy will cheer for the tortoise";

        public void Execute(IDefaultContext context)
        {
            EventHandler hareIsExausted = (sender, e) => {
                context.Actors["BoyInTheAudience"].Cheer();
            };
            context.Actors["Hare"].On("IsExausted", hareIsExausted);

            EventHandler tortoiseFinishedTheRace = (sender, e) => {
                if (!((IDictionary<string, object>)context.State).ContainsKey("HareFinishTime"))
                {
                    context.Actors["BoyInTheAudience"].Celebrate();
                }
                else
                {
                    context.Actors["BoyInTheAudience"].Cry();
                }
            };
            context.Actors["Tortoise"].On("FinishedTheRace", tortoiseFinishedTheRace);
        }
    }
}
