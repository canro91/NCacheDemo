using Alachisoft.NCache.Caching.Distributed;
using DistributedCacheWithNCache;
using DistributedCacheWithNCache.Services;

var (builder, services) = WebApplication.CreateBuilder(args);

services.AddControllers();
services.AddNCacheDistributedCache((options) =>
{
    options.CacheName = "demoCache";
    options.EnableLogs = true;
    options.ExceptionsEnabled = true;
});
services.AddTransient<SlowService>();

var app = builder.Build();
app.MapControllers();
app.Run();