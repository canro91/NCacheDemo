using DistributedCacheWithNCache.Responses;
using Microsoft.Extensions.Caching.Distributed;

namespace DistributedCacheWithNCache.Services
{
    public class SlowService
    {
        private readonly IDistributedCache _cache;

        public SlowService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<Something> GetAsync(int someId)
        {
            var key = $"{nameof(someId)}:{someId}";
            return await _cache.GetOrSetValueAsync(key, async () => await DoSomethingSlowlyAsync(someId));
        }

        private static async Task<Something> DoSomethingSlowlyAsync(int someId)
        {
            // Beep, boop...Aligning satellites...
            await Task.Delay(3 * 1000);

            return new Something
            {
                SomeId = someId,
                Value = "Anything"
            };
        }
    }
}