namespace RewardPoints.Shared;

[Serializable]
public record Customer(int Id, string Name, DateTime LastPurchase, int Points)
{
    public string ToCacheKey()
        => $"{nameof(Customer)}:{Id}";
}