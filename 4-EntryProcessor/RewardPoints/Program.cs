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

// Load customers
ICache cache = CacheManager.GetCache(CacheName);
PopulateCache(cache, customers);

// Part 1. Using regular cache operations
var withAtomicMethods = Stopwatch.StartNew();

foreach (var key in keys)
{
    var customer = cache.Get<Customer>(key);
    if (customer.LastPurchase >= DateTime.Today.AddDays(-5))
    {
        var updated = customer with { Points = customer.Points * 2 };
        cache.Insert(key, updated);
    }
}

withAtomicMethods.Stop();

var alice = cache.Get<Customer>(customers.First().ToCacheKey());
Debug.Assert(alice != null && alice.Points == 200);

// Load customers again
PopulateCache(cache, customers);

// Part 2. Using Bulk operations
var withBulkMethods = Stopwatch.StartNew();

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

withBulkMethods.Stop();

// Load customers again
PopulateCache(cache, customers);

// Part 3. Using Entry Processor
var withEntryProcessor = Stopwatch.StartNew();

var processor = new DoublePointsProcessor();
var processedEntries = cache.ExecutionService.Invoke(keys, processor);

withEntryProcessor.Stop();

alice = cache.Get<Customer>(customers.First().ToCacheKey());
Debug.Assert(alice != null && alice.Points == 200);

// Purge cache
cache.RemoveBulk(keys);

Console.WriteLine("Results:");
Console.WriteLine($"Atomic Cache operations: [{withAtomicMethods.Elapsed}]");
Console.WriteLine($"Bulk Cache operations: [{withBulkMethods.Elapsed}]");
Console.WriteLine($"Entry Processor: [{withEntryProcessor.Elapsed}]");
Console.ReadKey();

static void PopulateCache(ICache cache, IEnumerable<Customer> customers)
{
    // Purge cache before next run
    var keys = customers.Select(c => c.ToCacheKey());
    cache.RemoveBulk(keys);

    var cacheItems = customers.ToDictionary(c => c.ToCacheKey(), c => c.ToCacheItem());
    cache.AddBulk(cacheItems);
}