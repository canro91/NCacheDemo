using DistributedCacheWithNCache.Responses;
using Microsoft.Extensions.Caching.Distributed;

namespace DistributedCacheWithNCache.Services
{
    public class SettingsService
    {
        private readonly IDistributedCache _cache;

        public SettingsService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<SettingsResponse> GetAsync(int propertyId)
        {
            var key = $"{nameof(propertyId)}:{propertyId}";
            return await _cache.GetOrSetValueAsync(key, async () => await GetSettingsAsync(propertyId));
        }

        private static async Task<SettingsResponse> GetSettingsAsync(int propertyId)
        {
            // Beep, boop...Aligning satellites...
            await Task.Delay(5 * 1000);

            return new SettingsResponse
            {
                PropertyId = propertyId,
                Value = "Anything"
            };
        }
    }
}