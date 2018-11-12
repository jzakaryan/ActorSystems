using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans.Runtime;

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
                    options.ServiceId = "TemplateApp";
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
            // Use client to do work here
        }

    }
}
