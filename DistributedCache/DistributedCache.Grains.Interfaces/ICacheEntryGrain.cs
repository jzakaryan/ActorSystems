using Orleans;
using System;
using System.Threading.Tasks;

namespace DistributedCache.Grains.Interfaces
{
    public interface ICacheEntryGrain<T> : IGrainWithStringKey
    {
        /// <summary>
        /// Returns the value of the cache entry.
        /// </summary>
        /// <returns>The value of the entry.</returns>
        Task<T> GetValue();

        /// <summary>
        /// Sets the value for the entry, along with an expiration time span.
        /// </summary>
        /// <param name="value">Entry value.</param>
        /// <param name="expiration">Expiration time span.</param>
        Task SetValue(T value, TimeSpan expiration);

        /// <summary>
        /// Refreshes the expiration of the entry.
        /// </summary>
        /// <param name="expiration"></param>
        /// <returns></returns>
        Task Refresh(TimeSpan expiration);

        /// <summary>
        /// Removes the cache entry.
        /// </summary>
        Task Remove();
    }
}
