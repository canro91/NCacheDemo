using DistributedCacheWithNCache.Responses;
using DistributedCacheWithNCache.Services;
using Microsoft.AspNetCore.Mvc;

namespace DistributedCacheWithNCache;

[Route("api/[controller]")]
[ApiController]
public class SettingsController : ControllerBase
{
    private readonly SettingsService _settingsService;

    public SettingsController(SettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    [HttpGet]
    public async Task<SettingsResponse> GetAsync(int propertyId)
    {
        return await _settingsService.GetAsync(propertyId);
    }
}