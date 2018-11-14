using System;
using Orleans;
using System.Threading.Tasks;
using Application.Domain.Model;

namespace Grains.Interfaces
{
    public interface ICityGrain : IGrainWithStringKey
    {
        Task<(Location, Conditions)> GetWeatherData();
    }
}
