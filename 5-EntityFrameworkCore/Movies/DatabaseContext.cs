using Microsoft.EntityFrameworkCore;

namespace Movies;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
}