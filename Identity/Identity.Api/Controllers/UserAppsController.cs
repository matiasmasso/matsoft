using Identity.Api.Data;
using Identity.Api.Entities;
using Identity.Contracts.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("user-apps")]
public sealed class UserAppsController : ControllerBase
{
    private readonly AppDbContext _db;

    public UserAppsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("{userId:guid}")]
    public async Task<IEnumerable<UserAppDto>> GetUserApps(Guid userId)
    {
        return await _db.UserApps
            .Where(ua => ua.UserId == userId)
            .Select(ua => new UserAppDto
            {
                Id = ua.AppId,
                Name = ua.App.Name
            })
            .ToListAsync();
    }

    [HttpPost("{userId:guid}/{appId:guid}")]
    public async Task<IActionResult> Assign(Guid userId, Guid appId)
    {
        var exists = await _db.UserApps.AnyAsync(x => x.UserId == userId && x.AppId == appId);
        if (exists) return NoContent();

        _db.UserApps.Add(new UserApp
        {
            UserId = userId,
            AppId = appId
        });

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{userId:guid}/{appId:guid}")]
    public async Task<IActionResult> Remove(Guid userId, Guid appId)
    {
        var ua = await _db.UserApps.FindAsync(userId, appId);
        if (ua is null) return NotFound();

        _db.UserApps.Remove(ua);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}