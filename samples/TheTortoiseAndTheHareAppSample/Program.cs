using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Looplex.OpenForExtension.Abstractions.Plugins;
using Looplex.OpenForExtension.Contexts;
using Looplex.OpenForExtension.Loader;
using TheTortoiseAndTheHareAppSample.Domain.Entities;
using TheTortoiseAndTheHareAppSample.Domain.Repositories;
using TheTortoiseAndTheHareAppSample.Repositories;
using TheTortoiseAndTheHareAppSample.Services;

namespace TheTortoiseAndTheHareAppSample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IRepository<Hare>, InMemoryRepository<Hare>>();
            serviceCollection.AddSingleton<IRepository<Tortoise>, InMemoryRepository<Tortoise>>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var hareRepository = serviceProvider.GetRequiredService<IRepository<Hare>>();
            var tortoiseRepository = serviceProvider.GetRequiredService<IRepository<Tortoise>>();

            // Use the repositories
            var hare = new Hare { Name = "Hare", Speed = 100, Endurance = 10 };
            hareRepository.Add(hare);

            var tortoise = new Tortoise { Name = "Tortoise", Speed = 10, Endurance = 10000 };
            tortoiseRepository.Add(tortoise);

            var context = DefaultContext.Create(LoadPlugins(args), serviceProvider);
            context.State.HareId = hare.Id;
            context.State.TortoiseId = tortoise.Id;
            context.State.Distance = 1000;

            var service = new RaceService();

            if (args.Length > 0)
                await service.StartRaceAsync(context, CancellationToken.None);
            else
                service.StartRace(context, CancellationToken.None);

            var placements = context.Result;
            var jsonString = JsonSerializer.Serialize(placements);
            Console.WriteLine(jsonString);

            Console.WriteLine("Waiting for any key ...");
            Console.ReadLine();
        }

        private static IList<IPlugin> LoadPlugins(string[] args)
        {
            return (new PluginLoader()).LoadPlugins(GetPluginsPaths(args), ["RaceService.StartRaceAsync"]).ToList();
        }

        private static IEnumerable<string> GetPluginsPaths(string[] args)
        {
            foreach (var pluginName in args)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{pluginName}.dll");

                yield return path;
            }
        }
    }
}