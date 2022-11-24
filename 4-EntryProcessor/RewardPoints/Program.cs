using Alachisoft.NCache.Client;
using RewardPoints;
using RewardPoints.Shared;
using System.Diagnostics;

const string CacheName = "demoCache";

var customers = new List<Customer>
{
    new Customer(1, "Alice", DateTime.Today.AddDays(-1), 100),
    new Customer(2, "Bob", DateTime.Today.AddDays(-6), 5),
    new Customer(3, "Charlie", DateTime.Today.AddMonths(-1), 1),
    new Customer(4, "Daniel", DateTime.Today.AddDays(-3), 10),
    new Customer(5, "Earl", DateTime.Today, 20)
};
customers.AddRange(CustomerGenerator.Generate(count: 15, startingId: 6));

var keys = customers.Select(c => c.ToCacheKey());
var cacheItems = customers.ToDictionary(c => c.ToCacheKey(), c => c.ToCacheItem());

// 0. Load customers
ICache cache = CacheManager.GetCache(CacheName);
cache.AddBulk(cacheItems);

// 1. Double customer points using regular cache operations
var withoutEntryProcessorWatch = Stopwatch.StartNew();

var retrievedItems = cache.GetCacheItemBulk(keys);
var itemsToUpdate = new Dictionary<string, CacheItem>();
foreach (var item in retrievedItems)
{
    var customer = item.Value.GetValue<Customer>();
    if (customer.LastPurchase >= DateTime.Today.AddDays(-5))
    {
        var updated = customer with { Points = customer.Points * 2 };
        itemsToUpdate.Add(updated.ToCacheKey(), updated.ToCacheItem());
    }
}
cache.InsertBulk(itemsToUpdate);

withoutEntryProcessorWatch.Stop();

var alice = cache.Get<Customer>(customers.First().ToCacheKey());
Debug.Assert(alice != null && alice.Points == 200);

// 3. Purge cache before next run
cache.RemoveBulk(keys);

// 4. Load customers again
cache.AddBulk(cacheItems);

// 5. Double customer points using Entry Processor
var withEntryProcessorWatch = Stopwatch.StartNew();

var processor = new DoublePointsProcessor();
var processedEntries = cache.ExecutionService.Invoke(keys, processor);

withEntryProcessorWatch.Stop();

alice = cache.Get<Customer>(customers.First().ToCacheKey());
Debug.Assert(alice != null && alice.Points == 200);

// 6. Purge cache
cache.RemoveBulk(keys);

Console.WriteLine("Results:");
Console.WriteLine($"Cache operations: [{withoutEntryProcessorWatch.Elapsed}]");
Console.WriteLine($"Entry Processor: [{withEntryProcessorWatch.Elapsed}]");
Console.ReadKey();