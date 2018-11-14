using System;
using Application.Domain.Model;
using System.Threading.Tasks;
using Orleans;
using Grains.Interfaces;

namespace Application.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IClusterClient orleansClient;

        public WeatherService(IClusterClient orleansClient)
        {
            this.orleansClient = orleansClient;
        }

        public async Task<(Location, Conditions)> FindWeatherDataForCity(string cityName)
        {
            ICityGrain cityGrain = orleansClient.GetGrain<ICityGrain>(cityName);
            return await cityGrain.GetWeatherData();
        }
    }
}
