using LazyCache;
using Microsoft.AspNetCore.Mvc;
using T_Shop.Application.Common.Constants;

namespace T_Shop.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UtilityController : ControllerBase
{
    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;

    public UtilityController(IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    [HttpGet("cache/clear")]
    public async Task<IActionResult> ClearCacheAsync()
    {
        await Task.Run(() =>
        {
            foreach (var key in _cacheKeyConstants.CacheKeyList)
            {
                _cache.Remove(key);
            }

            _cacheKeyConstants.CacheKeyList = new List<string>();
        });

        return Ok();
    }
}
