using System;
using Orleans;
using Grains.Interfaces;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Application.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using Application.Services;

namespace Grains.Implementation
{
    public class CityGrain : Grain, ICityGrain
    {
        private Location location;
        private Conditions conditions;

        public override async Task OnActivateAsync()
        {
            await UpdateWeatherData();
            this.RegisterTimer(param => UpdateWeatherData(), null, TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(10));
        }

        public Task<(Location, Conditions)> GetWeatherData()
        {
            return Task.FromResult((location, conditions));
        }

        private async Task UpdateWeatherData()
        {
            IAccuWeatherProxy proxy = new AccuWeatherProxy();

            location = await proxy.FindCity(this.GetPrimaryKeyString());
            conditions = await proxy.GetWeatherConditionsForCity(location.Key);
        }
    }
}
