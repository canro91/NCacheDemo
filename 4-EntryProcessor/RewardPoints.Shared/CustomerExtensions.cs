using Alachisoft.NCache.Client;
using Alachisoft.NCache.Runtime.Caching;

namespace RewardPoints.Shared;

public static class CustomerExtensions
{
    public static CacheItem ToCacheItem(this Customer customer)
        => new CacheItem(customer)
        {
            Expiration = new Expiration(ExpirationType.Absolute, TimeSpan.FromMinutes(1))
        };
}