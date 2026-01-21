using Identity.Api.Data;
using Identity.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Controllers;

[ApiController]
[Route("users")]
[Authorize(Roles = "GlobalAdmin")]
public class UsersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _users;
    private readonly IdentityDbContext _db;

    public UsersController(UserManager<ApplicationUser> users, IdentityDbContext db)
    {
        _users = users;
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _users.Users
            .Select(u => new { u.Id, u.Email, u.UserName })
            .ToListAsync();

        return Ok(users);
    }

    public record EnrollRequest(string UserId, int AppId, string RoleName);

    [HttpPost("enroll")]
    public async Task<IActionResult> Enroll([FromBody] EnrollRequest req)
    {
        var user = await _users.FindByIdAsync(req.UserId);
        if (user == null) return NotFound("User not found");

        var app = await _db.Apps.FindAsync(req.AppId);
        if (app == null) return NotFound("App not found");

        var existing = await _db.AppUserRoles
            .FirstOrDefaultAsync(x => x.UserId == req.UserId && x.AppId == req.AppId && x.RoleName == req.RoleName);

        if (existing != null) return NoContent();

        var aur = new AppUserRole
        {
            UserId = req.UserId,
            AppId = req.AppId,
            RoleName = req.RoleName
        };

        _db.AppUserRoles.Add(aur);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}