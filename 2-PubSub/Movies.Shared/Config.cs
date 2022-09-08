namespace Movies.Shared;

public class Config
{
    public const string CacheName = "demoCache";
    public static readonly TimeSpan Expiration = TimeSpan.FromSeconds(10);

    public class Topics
    {
        public const string NewReleases = "newReleases";
    }
}