using DistributedCache.Application.Services;
using DistributedCache.Grains.Interfaces;
using Orleans;
using System;
using System.Threading.Tasks;

namespace DistributedCache.Orleans.Cache
{
    public class DistributedCache : ICache
    {
        private readonly IClusterClient client;

        public DistributedCache(IClusterClient client)
        {
            this.client = client;
        }

        public async Task<T> Get<T>(string key)
        {
            return await client.GetGrain<ICacheEntryGrain<T>>(key).GetValue();
        }

        public async Task Remove<T>(string key)
        {
            await client.GetGrain<ICacheEntryGrain<T>>(key).Remove();
        }

        public async Task Set<T>(string key, T value)
        {
            await client.GetGrain<ICacheEntryGrain<T>>(key).SetValue(value, TimeSpan.FromHours(5));
        }
    }
}
