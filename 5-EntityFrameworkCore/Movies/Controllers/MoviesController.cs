using Alachisoft.NCache.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Movies.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : ControllerBase
{
    private readonly DatabaseContext _database;

    public MoviesController(DatabaseContext database)
    {
        _database = database;
    }

    [HttpPost]
    public async Task Post([FromBody] AddMovie request)
    {
        var newMovie = new Movie(
            name: request.Name,
            releaseYear: request.ReleaseYear,
            rating: request.Rating);
        _database.Movies.Add(newMovie);

        await _database.SaveChangesAsync();

        var options = new CachingOptions
        {
            StoreAs = StoreAs.SeperateEntities
        };
        Cache cache = _database.GetCache();
        cache.Insert(newMovie, out _, options);
    }

    [HttpGet]
    public async Task<IEnumerable<Movie>> Get()
    {
        return await _database
            .Movies
            .OrderByDescending(m => m.Rating)
            .ThenBy(m => m.ReleaseYear)
            .Take(10)
            .FromCacheAsync(new CachingOptions
            {
                StoreAs = StoreAs.SeperateEntities
            });
    }
}