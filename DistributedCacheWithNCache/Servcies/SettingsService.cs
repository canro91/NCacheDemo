using DistributedCacheWithNCache.Responses;

namespace DistributedCacheWithNCache.Servcies
{
    public class SettingsService
    {
        public async Task<SettingsResponse> GetAsync(int propertyId)
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