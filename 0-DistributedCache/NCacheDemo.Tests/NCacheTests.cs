using Alachisoft.NCache.Client;
using Alachisoft.NCache.Runtime.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace NCacheDemo.Tests;

[TestClass]
public class NCacheTests
{
    private const string CacheName = "demoCache";

    [TestMethod]
    public async Task AddItem()
    {
        var movie = new Movie(1, "Titanic");
        var cacheKey = movie.ToCacheKey();
        var cacheItem = ToCacheItem(movie);

        ICache cache = CacheManager.GetCache(CacheName);
        await cache.AddAsync(cacheKey, cacheItem);

        var cachedMovie = cache.Get<Movie>(cacheKey);
        Assert.AreEqual(movie, cachedMovie);
    }

    [TestMethod]
    public async Task RetrieveNonExistingItem()
    {
        ICache cache = CacheManager.GetCache(CacheName);

        var cachedMovie = cache.Get<Movie>("ThisMovieDoesNotExist");
        Assert.IsNull(cachedMovie);
    }

    [TestMethod]
    public async Task UpdateItem()
    {
        ICache cache = CacheManager.GetCache("demoCache");

        var movie = new Movie(2, "5th Element");
        var cacheKey = movie.ToCacheKey();
        var cacheItem = ToCacheItem(movie);
        await cache.AddAsync(cacheKey, cacheItem);

        var updatedMovie = new Movie(2, "Fifth Element");
        var updatedCacheItem = ToCacheItem(updatedMovie);
        await cache.InsertAsync(cacheKey, updatedCacheItem);

        var cachedMovie = cache.Get<Movie>(cacheKey);
        Assert.AreEqual(updatedMovie, cachedMovie);
    }

    [TestMethod]
    public async Task RemoveItem()
    {
        ICache cache = CacheManager.GetCache("demoCache");

        var movie = new Movie(3, "Titanic");
        var cacheKey = movie.ToCacheKey();
        var cacheItem = ToCacheItem(movie);
        await cache.AddAsync(cacheKey, cacheItem);

        await cache.RemoveAsync<Movie>(cacheKey);

        var cachedMovie = cache.Get<Movie>(cacheKey);
        Assert.IsNull(cachedMovie);
    }

    private CacheItem ToCacheItem(Movie movie)
        => new CacheItem(movie)
        {
            Expiration = new Expiration(ExpirationType.Absolute, TimeSpan.FromSeconds(1))
        };
}

[Serializable]
public record Movie(int Id, string Name)
{
    public string ToCacheKey()
        => $"{nameof(Movie)}:{Id}";
}