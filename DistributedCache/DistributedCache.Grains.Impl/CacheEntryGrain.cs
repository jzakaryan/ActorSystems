using DistributedCache.Grains.Interfaces;
using Orleans;
using System;
using System.Threading.Tasks;

namespace DistributedCache.Grains.Impl
{
    public class CacheEntryGrain<T> : Grain, ICacheEntryGrain<T>
    {
        #region Instance State

        private TimeSpan expiration;
        private T value;

        #endregion Instance State

        public Task<T> GetValue()
        {
            return Task.FromResult(this.value);
        }

        public Task Refresh(TimeSpan expiration)
        {
            this.DelayDeactivation(expiration);

            return Task.CompletedTask;
        }

        public Task Remove()
        {
            this.DeactivateOnIdle();

            return Task.CompletedTask;
        }

        public Task SetValue(T value, TimeSpan expiration)
        {
            this.value = value;
            this.expiration = expiration;

            return Task.CompletedTask;
        }
    }
}
