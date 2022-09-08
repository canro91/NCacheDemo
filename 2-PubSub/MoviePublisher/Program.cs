using Alachisoft.NCache.Client;
using Alachisoft.NCache.Runtime.Caching;
using Movies.Shared;
using Movies.Shared.Entities;
using Movies.Shared.Extensions;

ICache cache = CacheManager.GetCache(Config.CacheName);
//             ^^^^^
// 1. Create an NCache cache instance

string topicName = Config.Topics.NewReleases;
ITopic newReleasesTopic = cache.MessagingService.CreateTopic(topicName);
//                        ^^^^^
// 2. Create a new topic
newReleasesTopic.MessageDeliveryFailure += OnFailureMessageReceived;
//               ^^^^
// Attach a callback in case of delivery failures

// 20 random movies taken from IMDb dump
// See: https://www.imdb.com/interfaces/
var newReleases = new List<Movie>
{
    new Movie("Toiset äänet", 2022, 7.70f, 87, new []{ Genre.Documentary }),
    new Movie("Loose Change", 2022, null, 60, new []{ Genre.Drama }),
    new Movie("Top Gun: Maverick", 2022, 8.60f, 130, new []{ Genre.Action, Genre.Drama }),
    new Movie("Who We Will Have Been", 2022, null, 81, new []{ Genre.Documentary }),
    new Movie("Alcarràs", 2022, 7.60f, 120, new []{ Genre.Drama }),
    new Movie("The Unlearning of US: The Blue-gieman & US", 2022, null, 59, new []{ Genre.Documentary }),
    new Movie("Why? - The Musical", 2022, null, 45, new []{ Genre.Musical }),
    new Movie("Ardh", 2022, 8.00f, 84, new []{ Genre.Drama, Genre.Romance }),
    new Movie("W. House", 2022, null, 75, new []{ Genre.Drama, Genre.Thriller }),
    new Movie("The Cursed Empire", 2023, null, 150, new []{ Genre.History }),
    new Movie("The Picture Frame", 2023, null, 20, new []{ Genre.Horror, Genre.Mystery }),
    new Movie("Smosh: Under the Influence", 2022, null, 238, new []{ Genre.Comedy }),
    new Movie("Monstré", 2022, null, 90, new []{ Genre.Thriller }),
    new Movie("Exploited", 2022, null, 81, new []{ Genre.Thriller }),
    new Movie("Waiting for Life", 2022, null, 114, new []{ Genre.Drama }),
    new Movie("Exposure", 2022, 8.30f, 88, new []{ Genre.Adventure, Genre.Documentary }),
    new Movie("Her Mother's Keeper", 2022, null, 90, new []{ Genre.Horror }),
    new Movie("5 Seasons - eine Reise", 2022, null, 120, new []{ Genre.Drama }),
    new Movie("The Long Haul", 2022, null, 60, new []{ Genre.Documentary }),
    new Movie("Hidden Wounds", 2022, null, 83, new []{ Genre.Documentary }),
};
foreach (var movie in newReleases)
{
    Console.WriteLine($"Releasing {movie}");

    var message = movie.ToMessage(Config.Expiration);
    //            ^^^^^
    // 3. Create a NCache message
    await newReleasesTopic.PublishAsync(message, DeliveryOption.All, true);
    //                     ^^^^^
    // 4. Publish it

    await Task.Delay(1 * 1_000);
}

Console.WriteLine("Press any key to continue");
Console.ReadKey();

static void OnFailureMessageReceived(object sender, MessageFailedEventArgs args)
{
    Console.WriteLine($"[ERROR] Failed to delivered message '{args.Message.Payload}'. Topic: [{args.TopicName}], Reason: [{args.MessageFailureReason}]");
}