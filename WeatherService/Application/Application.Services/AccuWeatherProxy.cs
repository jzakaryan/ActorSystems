using System;
using Application.Domain.Model;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class AccuWeatherProxy : IAccuWeatherProxy
    {
        private const string apiKey = "MY_API_KEY"; // Put your AccuWeather API Key here

        public async Task<Location> FindCity(string name)
        {
            using (HttpClient client = new HttpClient())
            {
                var locationResponse = await client.GetAsync($"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apiKey}&q={name}");
                var locationContent = await locationResponse.Content.ReadAsStringAsync();

                var location = JsonConvert.DeserializeObject<List<Location>>(locationContent).FirstOrDefault();
                return location;
            }
        }

        public async Task<Conditions> GetWeatherConditionsForCity(Location location)
        {
            return await this.GetWeatherConditionsForCity(location.Key);
        }

        public async Task<Conditions> GetWeatherConditionsForCity(int locationID)
        {
            using (HttpClient client = new HttpClient())
            {
                var locationResponse = await client.GetAsync($"http://dataservice.accuweather.com/currentconditions/v1/{locationID}?apikey={apiKey}");
                var locationContent = await locationResponse.Content.ReadAsStringAsync();

                var conditions = JsonConvert.DeserializeObject<List<Conditions>>(locationContent).FirstOrDefault();
                return conditions;
            }
        }
    }
}
