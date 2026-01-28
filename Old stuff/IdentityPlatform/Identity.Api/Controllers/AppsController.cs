using Identity.Api.Application.Apps;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Identity.Api.Controllers;

[ApiController]
[Route("apps")]
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

    public record UpdateAppRequest(string Name);

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateApp(Guid id, UpdateAppRequest request)
    {
        try
        {
            var updated = await _apps.UpdateAppAsync(id, request.Name);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteApp(Guid id)
    {
        try
        {
            await _apps.DeleteAppAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}