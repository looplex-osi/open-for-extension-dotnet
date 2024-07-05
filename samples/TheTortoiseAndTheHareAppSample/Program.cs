using Looplex.OpenForExtension.Context;
using Looplex.OpenForExtension.Manager;
using Looplex.OpenForExtension.Plugins;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using TheTortoiseAndTheHare.Entities;
using TheTortoiseAndTheHare.Repositories;
using TheTortoiseAndTheHareAppSample.Services;

namespace TheTortoiseAndTheHareAppSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IRepository<Hare>, InMemoryRepository<Hare>>();
            serviceCollection.AddSingleton<IRepository<Tortoise>, InMemoryRepository<Tortoise>>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var hareRepository = serviceProvider.GetRequiredService<IRepository<Hare>>();
            var tortoiseRepository = serviceProvider.GetRequiredService<IRepository<Tortoise>>();

            // Use the repositories
            var hare = new Hare { Name = "Speedy", Speed = 100, Endurance = 10 };
            hareRepository.Add(hare);

            var tortoise = new Tortoise { Name = "Steady", Speed = 10, Endurance = 10000 };
            tortoiseRepository.Add(tortoise);

            var context = DefaultContext.Create(LoadPlugins(args), serviceProvider);
            context.State.HareId = hare.Id;
            context.State.TortoiseId = tortoise.Id;
            context.State.Distance = 1000;

            var service = new RaceService();

            service.StartRace(context);

            var placements = context.Result;
            string jsonString = JsonSerializer.Serialize(placements);
            Console.WriteLine(jsonString);

            Console.WriteLine("Waiting for any key ...");
            Console.ReadLine();
        }

        private static IList<IPlugin> LoadPlugins(string[] args)
        {
            return (new PluginLoader()).LoadPlugins(GetPluginsPaths(args)).ToList();
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