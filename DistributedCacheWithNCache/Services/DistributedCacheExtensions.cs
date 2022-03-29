using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace DistributedCacheWithNCache.Services
{
    public static class DistributedCacheExtensions
    {
        public static readonly DistributedCacheEntryOptions DefaultDistributedCacheEntryOptions
            = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                SlidingExpiration = TimeSpan.FromSeconds(10),
            };

        public static async Task<TObject> GetOrSetValueAsync<TObject>(this IDistributedCache cache, string key, Func<Task<TObject>> factory, DistributedCacheEntryOptions options = null)
            where TObject : class
        {
            var result = await cache.GetValueAsync<TObject>(key);
            if (result != null)
            {
                return result;
            }

            result = await factory();

            await cache.SetValueAsync(key, result, options);

            return result;
        }

        private static async Task<TObject> GetValueAsync<TObject>(this IDistributedCache cache, string key)
            where TObject : class
        {
            var data = await cache.GetStringAsync(key);
            if (data == null)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<TObject>(data);
        }

        private static async Task SetValueAsync<TObject>(this IDistributedCache cache, string key, TObject value, DistributedCacheEntryOptions options = null)
            where TObject : class
        {
            var data = JsonConvert.SerializeObject(value);

            await cache.SetStringAsync(key, data, options ?? DefaultDistributedCacheEntryOptions);
        }
    }
}