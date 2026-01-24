using Identity.Api.Application.Apps;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/apps")]
public class AppsController : ControllerBase
{
    private readonly AppService _apps;

    public AppsController(AppService apps)
    {
        _apps = apps;
    }

    public record CreateAppRequest(string Key, string Name);

    [HttpPost]
    public async Task<IActionResult> Create(CreateAppRequest request)
    {
        var app = await _apps.CreateAppAsync(request.Key, request.Name);
        return Ok(app);
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        try
        {
            var apps = await _apps.ListAppsAsync();
            return Ok(apps);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpGet("{key}")]
    public async Task<IActionResult> Get(string key)
    {
        var app = await _apps.GetByKeyAsync(key);
        return app == null ? NotFound() : Ok(app);
    }
}