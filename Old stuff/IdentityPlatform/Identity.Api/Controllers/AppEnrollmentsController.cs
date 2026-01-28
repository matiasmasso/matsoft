using Identity.Api.Data;
using Identity.Api.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("apps/{appId:guid}/users")]
public class AppEnrollmentsController : ControllerBase
{
    private readonly IdentityDbContext _db;

    public AppEnrollmentsController(IdentityDbContext db)
    {
        _db = db;
    }

    // GET /apps/{appId}/users
    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetUsers(Guid appId)
    {
        var users = await _db.UserAppEnrollments
            .Where(e => e.AppId == appId)
            .Select(e => e.User)
            .ToListAsync();

        return users.Select(u => u.ToDto()).ToList();
    }

    // POST /apps/{appId}/users/{userId}
    [HttpPost("{userId:guid}")]
    public async Task<ActionResult> Enroll(Guid appId, string userId)
    {
        var exists = await _db.UserAppEnrollments
            .AnyAsync(e => e.AppId == appId && e.UserId == userId);

        if (!exists)
        {
            _db.UserAppEnrollments.Add(new UserAppEnrollment
            {
                Id = Guid.NewGuid(),
                AppId = appId,
                UserId = userId
            });

            await _db.SaveChangesAsync();
        }

        return Ok(true);
    }

    // DELETE /apps/{appId}/users/{userId}
    [HttpDelete("{userId:guid}")]
    public async Task<ActionResult> Unenroll(Guid appId, Guid userId)
    {
        var enrollment = await _db.UserAppEnrollments
            .FirstOrDefaultAsync(e => e.AppId == appId && e.UserId == userId.ToString());

        if (enrollment is not null)
        {
            _db.UserAppEnrollments.Remove(enrollment);
            await _db.SaveChangesAsync();
        }

        return Ok(true);
    }
}