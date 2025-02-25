using Looplex.OpenForExtension.Abstractions.Commands;
using Looplex.OpenForExtension.Abstractions.Contexts;
using Looplex.OpenForExtension.Abstractions.ExtensionMethods;
using Microsoft.Extensions.DependencyInjection;
using TheTortoiseAndTheHareAppSample.Domain.Entities;
using TheTortoiseAndTheHareAppSample.Domain.Repositories;

namespace TheTortoiseAndTheHareAppSample.Services
{
    internal class RaceService
    {        
        public void StartRace(IContext context, CancellationToken cancellationToken)
        {
            IList<dynamic> results = [];

            ArgumentNullException.ThrowIfNull(context);

            Guid tortoiseId = context.State.TortoiseId;
            Guid hareId = context.State.HareId;
            int distance = context.State.Distance;
            context.Plugins.Execute<IHandleInput>(context, cancellationToken);
            
            var hareRepository = (IRepository<Hare>)context.Roles["HareRepository"];
            var tortoiseRepository = (IRepository<Tortoise>)context.Roles["TortoiseRepository"];
            var tortoise = tortoiseRepository.GetById(tortoiseId);
            var hare = hareRepository.GetById(hareId);            

            // Validations
            ValidateDistance(distance);
            ValidateRacer(tortoiseId, tortoise);
            ValidateRacer(hareId, hare);
            context.Plugins.Execute<IValidateInput>(context, cancellationToken);

            // Define actors
            context.Roles.Add("Tortoise", tortoise);
            context.Roles.Add("Hare", hare);
            context.Plugins.Execute<IDefineRoles>(context, cancellationToken);

            // Bind events
            BindEvents(context, results);
            context.Plugins.Execute<IBind>(context, cancellationToken); 

            context.Plugins.Execute<IBeforeAction>(context, cancellationToken);

            if (!context.SkipDefaultAction)
            {
                DefaultAction(context, distance, results);
            }

            context.Plugins.Execute<IAfterAction>(context, cancellationToken); 

            context.Plugins.Execute<IReleaseUnmanagedResources>(context, cancellationToken); 
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

        private static void BindEvents(IContext context, IList<dynamic> results)
        {
            context.Roles["Tortoise"].On("StartedToRun", (EventHandler)TortoiseStartedToRun);
            context.Roles["Hare"].On("StartedToRun", (EventHandler)HareStartedToRun);
            context.Roles["Tortoise"].On("FinishedTheRace", (EventHandler)TortoiseFinishedTheRace);
            context.Roles["Hare"].On("FinishedTheRace", (EventHandler)HareFinishedTheRace);
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
                    context.Roles["Tortoise"].Name, ElapsedTime = (context.State.TortoiseFinishTime - context.State.TortoiseStartTime).TotalSeconds
                });
            }
            void HareFinishedTheRace(object? sender, EventArgs e)
            {
                context.State.HareFinishTime = DateTime.UtcNow;
                results.Add(new
                {
                    context.Roles["Hare"].Name, ElapsedTime = (context.State.HareFinishTime - context.State.HareStartTime).TotalSeconds
                });
            }
        }

        private void DefaultAction(IContext context, int distance, IList<dynamic> results)
        {
            context.Roles["Tortoise"].StartTheRace(distance);
            context.Roles["Hare"].StartTheRace(distance);

            while (context.Roles["Tortoise"].State != "Stopped"
                || context.Roles["Hare"].State != "Stopped")
            {
                context.Roles["Tortoise"].Sprint();
                context.Roles["Hare"].Sprint();
            }

            context.Result = results;
        }
        
        public async Task StartRaceAsync(IContext context, CancellationToken cancellationToken)
        {
            IList<dynamic> results = [];

            ArgumentNullException.ThrowIfNull(context);

            Guid tortoiseId = context.State.TortoiseId;
            Guid hareId = context.State.HareId;
            int distance = context.State.Distance;
            await context.Plugins.ExecuteAsync<IHandleInput>(context, cancellationToken);
            
            var hareRepository = (IRepository<Hare>)context.Roles["HareRepository"];
            var tortoiseRepository = (IRepository<Tortoise>)context.Roles["TortoiseRepository"];
            var tortoise = tortoiseRepository.GetById(tortoiseId);
            var hare = hareRepository.GetById(hareId);            

            // Validations
            ValidateDistance(distance);
            ValidateRacer(tortoiseId, tortoise);
            ValidateRacer(hareId, hare);
            await context.Plugins.ExecuteAsync<IValidateInput>(context, cancellationToken);

            // Define actors
            context.Roles.Add("Tortoise", tortoise);
            context.Roles.Add("Hare", hare);
            await context.Plugins.ExecuteAsync<IDefineRoles>(context, cancellationToken);

            // Bind events
            BindEvents(context, results);
            await context.Plugins.ExecuteAsync<IBind>(context, cancellationToken); 

            await context.Plugins.ExecuteAsync<IBeforeAction>(context, cancellationToken);

            if (!context.SkipDefaultAction)
            {
                DefaultAction(context, distance, results);
            }

            await context.Plugins.ExecuteAsync<IAfterAction>(context, cancellationToken); 

            await context.Plugins.ExecuteAsync<IReleaseUnmanagedResources>(context, cancellationToken); 
        }
    }
}
