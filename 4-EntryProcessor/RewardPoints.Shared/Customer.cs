using Alachisoft.NCache.Client;
using Alachisoft.NCache.Runtime.Caching;

namespace RewardPoints.Shared;

[Serializable]
public record Customer(int Id, string Name, DateTime LastPurchase, int Points)
{
    public string ToCacheKey()
        => $"{nameof(Customer)}:{Id}";

    public CacheItem ToCacheItem()
        => new CacheItem(this)
        {
            Expiration = new Expiration(ExpirationType.Absolute, TimeSpan.FromMinutes(1))
        };
}