using Alachisoft.NCache.Caching.Distributed;
using Alachisoft.NCache.Client;
using DistributedCacheWithNCache.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddNCacheDistributedCache((options) =>
{
    options.ServerList = new List<ServerInfo>
    {
        new ServerInfo(IPAddress.Parse("127.0.0.1"), 9801),
        new ServerInfo(IPAddress.Parse("127.0.0.1"), 9802)
    };
    options.CacheName = "DemoClusteredCache";
    options.EnableLogs = true;
    options.ExceptionsEnabled = true;
});
builder.Services.AddTransient<SettingsService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
