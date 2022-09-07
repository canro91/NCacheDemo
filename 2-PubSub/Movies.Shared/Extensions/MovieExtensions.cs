using Alachisoft.NCache.Runtime.Caching;

namespace Movies.Shared.Extensions;

public static class MovieExtensions
{
    public static Message ToMessage(this object self, TimeSpan? expiration = null)
        => new Message(self, expiration);
}