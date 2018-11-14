using Application.Domain.Model;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAccuWeatherProxy
    {
        Task<Location> FindCity(string name);

        Task<Conditions> GetWeatherConditionsForCity(Location location);

        Task<Conditions> GetWeatherConditionsForCity(int locationID);
    }
}
