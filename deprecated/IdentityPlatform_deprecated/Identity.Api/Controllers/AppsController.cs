using Identity.Api.Data;
using Identity.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Controllers;

[ApiController]
[Route("apps")]
[Authorize(Roles = "GlobalAdmin")]
public class AppsController : ControllerBase
{
    private readonly IdentityDbContext _db;

    public AppsController(IdentityDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetApps()
    {
        var apps = await _db.Apps.ToListAsync();
        return Ok(apps);
    }

    public record CreateAppRequest(string Name, string ClientId, string? Description);

    [HttpPost]
    public async Task<IActionResult> CreateApp([FromBody] CreateAppRequest req)
    {
        var app = new App
        {
            Name = req.Name,
            ClientId = req.ClientId,
            Description = req.Description
        };

        _db.Apps.Add(app);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetApps), new { id = app.Id }, app);
    }
}