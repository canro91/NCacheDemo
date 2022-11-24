using Bogus;
using RewardPoints.Shared;

namespace RewardPoints;

public static class CustomerGenerator
{
    public static IEnumerable<Customer> Generate(int count, int startingId)
    {
        var faker = new Faker();

        foreach (var _ in Enumerable.Range(0, count))
        {
            yield return new Customer(
                startingId++,
                faker.Random.AlphaNumeric(10),
                DateTime.Today.AddDays(faker.Random.Int(-10, 10)),
                faker.Random.Int(0, 200));
        }
    }
}