using DistributedCacheWithNCache.Responses;
using DistributedCacheWithNCache.Services;
using Microsoft.AspNetCore.Mvc;

namespace DistributedCacheWithNCache;

[Route("api/[controller]")]
[ApiController]
public class SlowController : ControllerBase
{
    private readonly SlowService _settingsService;

    public SlowController(SlowService settingsService)
    {
        _settingsService = settingsService;
    }

    [HttpGet]
    public async Task<Something> GetAsync(int propertyId)
    {
        return await _settingsService.GetAsync(propertyId);
    }
}