using Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

[ApiController]
[Route("apps")]
public class UserApplicationController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public UserApplicationController(ApplicationDbContext db)
    {
        _db = db;
    }

    // ------------------------------------------------------------
    // POST /apps/enroll
    // Enroll a user into an application
    // ------------------------------------------------------------
    [HttpPost("enroll")]
    public async Task<IActionResult> Enroll([FromBody] EnrollUserRequest request)
    {
        var exists = await _db.UserApplications.AnyAsync(ua =>
            ua.UserId == request.UserId &&
            ua.ApplicationId == request.ApplicationId);

        if (exists)
            return BadRequest("User already enrolled in this application");

        var enrollment = new UserApplication
        {
            UserId = request.UserId,
            ApplicationId = request.ApplicationId,
            IsActive = true
        };

        _db.UserApplications.Add(enrollment);
        await _db.SaveChangesAsync();

        return Ok("User enrolled in application");
    }

    // ------------------------------------------------------------
    // POST /apps/unenroll
    // Remove a user from an application
    // ------------------------------------------------------------
    [HttpPost("unenroll")]
    public async Task<IActionResult> Unenroll([FromBody] EnrollUserRequest request)
    {
        var enrollment = await _db.UserApplications.FirstOrDefaultAsync(ua =>
            ua.UserId == request.UserId &&
            ua.ApplicationId == request.ApplicationId);

        if (enrollment == null)
            return NotFound("User is not enrolled in this application");

        _db.UserApplications.Remove(enrollment);
        await _db.SaveChangesAsync();

        return Ok("User unenrolled from application");
    }

    // ------------------------------------------------------------
    // GET /apps/user/{userId}
    // List all applications a user is enrolled in
    // ------------------------------------------------------------
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetUserApps(Guid userId)
    {
        var apps = await _db.UserApplications
            .Where(ua => ua.UserId == userId)
            .Include(ua => ua.Application)
            .Select(ua => new
            {
                ua.ApplicationId,
                ua.Application.Name,
                ua.IsActive
            })
            .ToListAsync();

        return Ok(apps);
    }
}
