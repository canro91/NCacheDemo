using Alachisoft.NCache.Client;
using Alachisoft.NCache.Runtime.Caching;
using Movies.Shared;
using Movies.Shared.Entities;

ICache cache = CacheManager.GetCache(Config.CacheName);
//             ^^^^^
// 1. Create an NCache cache instance

string topicName = Config.Topics.NewReleases;
ITopic newReleasesTopic = cache.MessagingService.GetTopic(topicName);
//                        ^^^^^
// 2. Grab the same topic

newReleasesTopic.OnTopicDeleted = OnTopicDeleted;
//               ^^^^^
// Attach a callback if the topic gets deleted

if (newReleasesTopic == null)
{
    Console.WriteLine($"Ooops...Topic [{topicName}] deleted.");
}
else
{
    ITopicSubscription newReleasesSubscriber
        = newReleasesTopic.CreateSubscription(MessageReceived, DeliveryMode.Async);
    //    ^^^^^
    // 3. Attach a callback for new movies

    // Or
    // Alternative to create a Durable subscription
    //IDurableTopicSubscription newReleasesSubscriber =
    //       newReleasesTopic.CreateDurableSubscription(
    //           "orderTopicName",
    //            SubscriptionPolicy.Shared,
    //            MessageReceived,
    //            Config.Expiration,
    //            DeliveryMode.Async);

    Console.WriteLine("Press any key to continue");
    Console.ReadKey();

    newReleasesSubscriber.UnSubscribe();
    //                    ^^^^^
    // 4. Unsubscribe...
}

void MessageReceived(object sender, MessageEventArgs args)
{
    if (args.Message.Payload is Movie movie)
    {
        Console.WriteLine($"New Movie released: {movie}");
    }
}

void OnTopicDeleted(object sender, TopicDeleteEventArgs args)
{
    Console.WriteLine($"[ERROR] Ooops Topic deleted. Topic: [{args.TopicName}]");
}