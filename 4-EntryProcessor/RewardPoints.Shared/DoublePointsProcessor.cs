using Alachisoft.NCache.Runtime.Processor;

namespace RewardPoints.Shared;

[Serializable]
public class DoublePointsProcessor : IEntryProcessor
{
    public bool IgnoreLock()
        => true;

    public object Process(IMutableEntry entry, params object[] arguments)
    {
        if (entry.Key.StartsWith(nameof(Customer))
            && entry.Value is Customer { LastPurchase: var lastPurchase } customer
            && lastPurchase >= DateTime.Today.AddDays(-5))
        {
            var updatedCustomer = customer with { Points = customer.Points * 2 };
            entry.Value = updatedCustomer;
            return updatedCustomer;
        }

        return false;
    }
}