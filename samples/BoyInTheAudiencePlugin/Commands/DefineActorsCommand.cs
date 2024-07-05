using BoyInTheAudiencePlugin.Entities;
using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;

namespace BoyInTheAudiencePlugin.Commands
{
    internal class DefineActorsCommand : IDefineActors
    {
        public string Name => "Defines a boy in the audience";

        public string Description => "Adds the boy to the actors dict";

        public void Execute(IDefaultContext context)
        {
            context.Actors["BoyInTheAudience"] = new Boy();
        }
    }
}
