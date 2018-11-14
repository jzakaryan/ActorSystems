using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans.Runtime;
using Grains.Interfaces;
using Application.Services;
using Application.Domain.Model;

namespace Application.Orleans.Client
{
    public class Program
    {
        const int retryCount = 5;
        private static int attempt = 0;

        public static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                using (var client = await StartClientWithRetries())
                {
                    await DoClientWork(client);
                    Console.Read();
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.Read();
                return 1;
            }
        }

        private static async Task<IClusterClient> StartClientWithRetries()
        {
            attempt = 0;
            IClusterClient client;
            client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "WeatherApp";
                })
                .ConfigureLogging(logging => logging.AddConsole())
                .Build();

            await client.Connect(RetryFilter);

            Console.WriteLine("Client successfully connect to silo host");

            return client;
        }

        private static async Task<bool> RetryFilter(Exception exception)
        {
            if (exception.GetType() != typeof(SiloUnavailableException))
            {
                Console.WriteLine($"Cluster client failed to connect to cluster with unexpected error.  Exception: {exception}");
                return false;
            }

            attempt++;

            Console.WriteLine($"Cluster client attempt {attempt} of {retryCount} failed to connect to cluster.  Exception: {exception}");

            if (attempt > retryCount)
            {
                return false;
            }

            await Task.Delay(TimeSpan.FromSeconds(4));

            return true;
        }

        private static async Task DoClientWork(IClusterClient client)
        {
            Console.WriteLine("Enter city name to get weather data or 'Q' to quit.");
            IWeatherService weatherService = new WeatherService(client); 

            string userInput = Console.ReadLine();

            while (userInput != "Q")
            {
                (Location city, Conditions weather) =  await weatherService.FindWeatherDataForCity(userInput);
                OutputWeather(city, weather);
                string userInput = Console.ReadLine();
            }
        }

        private static void OutputWeather(Location location, Conditions conditions)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Weather for {location.EnglishName}, {location.Country.EnglishName}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{conditions.WeatherText}, {conditions.Temperature.Metric.Value}{conditions.Temperature.Metric.Unit}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
