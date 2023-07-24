using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }

    [HttpGet]
    public async Task<List<Movie>> Get()
    {
        return await _database
            .Movies
            .OrderByDescending(m => m.Rating)
            .ThenBy(m => m.ReleaseYear)
            .Take(10)
            .ToListAsync();
    }
}