using Application.Domain.Model;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IWeatherService
    {
        Task<(Location, Conditions)> FindWeatherDataForCity(string cityName);
    }
}
