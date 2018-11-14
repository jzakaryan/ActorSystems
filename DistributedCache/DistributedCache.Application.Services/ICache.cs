using System;
using System.Threading.Tasks;

namespace DistributedCache.Application.Services
{
    public interface ICache
    {
        Task Set<T>(string key, T value);

        Task<T> Get<T>(string key);

        Task Remove<T>(string key);
    }
}
