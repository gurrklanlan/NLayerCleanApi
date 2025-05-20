using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Application.Contracts.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace App.Caching
{
    public class CacheService(IMemoryCache memoryCache) : ICacheService
    {
    

        public Task<T?> GetAsync<T>(string cacheKey)
        {
            return Task.FromResult(memoryCache.TryGetValue(cacheKey, out T cacheitem) ? cacheitem : default(T));
        }

       public Task AddAsync<T>(string cacheKey, T value, TimeSpan expirationTime)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expirationTime
            };
            memoryCache.Set(cacheKey, value, cacheEntryOptions);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string cacheKey)
        {
            memoryCache.Remove(cacheKey);
            return Task.CompletedTask;
        }

    }
}
