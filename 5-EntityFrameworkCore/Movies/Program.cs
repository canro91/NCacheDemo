using Alachisoft.NCache.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options => {
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("Default");

    NCacheConfiguration.Configure(cacheId: "demoCache", DependencyType.SqlServer);
    NCacheConfiguration.ConfigureLogger();

    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    DbInitializer.Initialize(context);
}

app.Run();
