﻿using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;
using Looplex.OpenForExtension.ExtensionMethods;
using Microsoft.Extensions.DependencyInjection;
using TheTortoiseAndTheHareAppSample.Domain.Entities;
using TheTortoiseAndTheHareAppSample.Domain.Repositories;

namespace TheTortoiseAndTheHareAppSample.Services
{
    internal class RaceService
    {        
        public void StartRace(IDefaultContext context)
        {
            IList<dynamic> results = [];

            ArgumentNullException.ThrowIfNull(context);

            Guid tortoiseId = context.State.TortoiseId;
            Guid hareId = context.State.HareId;
            int distance = context.State.Distance;
            context.Plugins.Execute<IHandleInput>(context);
            
            var hareRepository = context.Services.GetRequiredService<IRepository<Hare>>();
            var tortoiseRepository = context.Services.GetRequiredService<IRepository<Tortoise>>();
            var tortoise = tortoiseRepository.GetById(tortoiseId);
            var hare = hareRepository.GetById(hareId);            

            // Validations
            ValidateDistance(distance);
            ValidateRacer(tortoiseId, tortoise);
            ValidateRacer(hareId, hare);
            context.Plugins.Execute<IValidateInput>(context);

            // Define actors
            context.Actors.Add("Tortoise", tortoise);
            context.Actors.Add("Hare", hare);
            context.Plugins.Execute<IDefineActors>(context);

            // Bind events
            BindEvents(context, results);
            context.Plugins.Execute<IBind>(context); 

            context.Plugins.Execute<IBeforeAction>(context);

            if (!context.SkipDefaultAction)
            {
                DefaultAction(context, distance, results);
            }

            context.Plugins.Execute<IAfterAction>(context); 

            context.Plugins.Execute<IReleaseUnmanagedResources>(context); 
        }

        private void ValidateDistance(int distance)
        {
            if (distance <= 0)
            {
                throw new ArgumentException($"Distance too short to be a race.");
            }
        }

        private static void ValidateRacer(Guid id, Racer? racer)
        {
            if (racer == null)
            {
                throw new InvalidOperationException($"Tortoise {id} was not found.");
            }
            if (racer.Speed == 0)
            {
                throw new InvalidOperationException($"Tortoise {racer.Name} can't race now.");
            }
        }

        private static void BindEvents(IDefaultContext context, IList<dynamic> results)
        {
            context.Actors["Tortoise"].On("StartedToRun", (EventHandler)TortoiseStartedToRun);
            context.Actors["Hare"].On("StartedToRun", (EventHandler)HareStartedToRun);
            context.Actors["Tortoise"].On("FinishedTheRace", (EventHandler)TortoiseFinishedTheRace);
            context.Actors["Hare"].On("FinishedTheRace", (EventHandler)HareFinishedTheRace);
            return;
            
            void TortoiseStartedToRun(object? sender, EventArgs e)
            {
                context.State.TortoiseStartTime = DateTime.UtcNow;
            }
            void HareStartedToRun(object? sender, EventArgs e)
            {
                context.State.HareStartTime = DateTime.UtcNow;
            }
            void TortoiseFinishedTheRace(object? sender, EventArgs e)
            {
                context.State.TortoiseFinishTime = DateTime.UtcNow;
                results.Add(new
                {
                    context.Actors["Tortoise"].Name, ElapsedTime = (context.State.TortoiseFinishTime - context.State.TortoiseStartTime).TotalSeconds
                });
            }
            void HareFinishedTheRace(object? sender, EventArgs e)
            {
                context.State.HareFinishTime = DateTime.UtcNow;
                results.Add(new
                {
                    context.Actors["Hare"].Name, ElapsedTime = (context.State.HareFinishTime - context.State.HareStartTime).TotalSeconds
                });
            }
        }

        private void DefaultAction(IDefaultContext context, int distance, IList<dynamic> results)
        {
            context.Actors["Tortoise"].StartTheRace(distance);
            context.Actors["Hare"].StartTheRace(distance);

            while (context.Actors["Tortoise"].State != "Stopped"
                || context.Actors["Hare"].State != "Stopped")
            {
                context.Actors["Tortoise"].Sprint();
                context.Actors["Hare"].Sprint();
            }

            context.Result = results;
        }
        
        public async Task StartRaceAsync(IDefaultContext context)
        {
            IList<dynamic> results = [];

            ArgumentNullException.ThrowIfNull(context);

            Guid tortoiseId = context.State.TortoiseId;
            Guid hareId = context.State.HareId;
            int distance = context.State.Distance;
            await context.Plugins.ExecuteAsync<IHandleInput>(context);
            
            var hareRepository = context.Services.GetRequiredService<IRepository<Hare>>();
            var tortoiseRepository = context.Services.GetRequiredService<IRepository<Tortoise>>();
            var tortoise = tortoiseRepository.GetById(tortoiseId);
            var hare = hareRepository.GetById(hareId);            

            // Validations
            ValidateDistance(distance);
            ValidateRacer(tortoiseId, tortoise);
            ValidateRacer(hareId, hare);
            await context.Plugins.ExecuteAsync<IValidateInput>(context);

            // Define actors
            context.Actors.Add("Tortoise", tortoise);
            context.Actors.Add("Hare", hare);
            await context.Plugins.ExecuteAsync<IDefineActors>(context);

            // Bind events
            BindEvents(context, results);
            await context.Plugins.ExecuteAsync<IBind>(context); 

            await context.Plugins.ExecuteAsync<IBeforeAction>(context);

            if (!context.SkipDefaultAction)
            {
                DefaultAction(context, distance, results);
            }

            await context.Plugins.ExecuteAsync<IAfterAction>(context); 

            await context.Plugins.ExecuteAsync<IReleaseUnmanagedResources>(context); 
        }
    }
}
