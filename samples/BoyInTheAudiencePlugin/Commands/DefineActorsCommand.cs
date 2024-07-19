﻿using System.Diagnostics;
using BoyInTheAudiencePlugin.Entities;
using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;

namespace BoyInTheAudiencePlugin.Commands
{
    internal class DefineActorsCommand : IDefineActors
    {
        public string Name => "Defines a boy in the audience";

        public string Description => "Adds the boy to the actors dict";

        public Task ExecuteAsync(IDefaultContext context)
        {
            if (new StackTrace().GetFrames()
                .Select(f => $"{f.GetMethod()?.DeclaringType?.Name}.{f.GetMethod()?.Name}")
                .Any(caller => caller == "RaceService.StartRaceAsync"))
            {
                context.Actors["BoyInTheAudience"] = new Boy();
            }
            
            return Task.CompletedTask;
        }
    }
}
