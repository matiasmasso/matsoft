using Identity.Api.Data;
using Identity.Api.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("apps/{appId:guid}/users/{userId:guid}/roles")]
public class UserRolesController : ControllerBase
{
    private readonly IdentityDbContext _db;

    public UserRolesController(IdentityDbContext db)
    {
        _db = db;
    }

    // GET assigned roles
    [HttpGet]
    public async Task<ActionResult<List<Guid>>> GetAssigned(Guid appId, Guid userId)
    {
        var roleIds = await _db.UserRoles
            .Where(ur => ur.Enrollment.AppId == appId && ur.Enrollment.UserId == userId.ToString())
            .Select(ur => ur.RoleId)
            .ToListAsync();

        return roleIds;
    }

    // POST assign role
    [HttpPost("{roleId:guid}")]
    public async Task<ActionResult> Assign(Guid appId, Guid userId, Guid roleId)
    {
        var enrollment = await _db.UserAppEnrollments
            .FirstOrDefaultAsync(e => e.AppId == appId && e.UserId == userId.ToString());

        if (enrollment is null)
            return BadRequest("User is not enrolled in this app.");

        var exists = await _db.UserRoles
            .AnyAsync(ur => ur.EnrollmentId == enrollment.Id && ur.RoleId == roleId);

        if (!exists)
        {
            _db.UserRoles.Add(new UserRole
            {
                Id = Guid.NewGuid(),
                EnrollmentId = enrollment.Id,
                RoleId = roleId
            });

            await _db.SaveChangesAsync();
        }

        return Ok(true);
    }

    // DELETE unassign role
    [HttpDelete("{roleId:guid}")]
    public async Task<ActionResult> Unassign(Guid appId, Guid userId, Guid roleId)
    {
        var enrollment = await _db.UserAppEnrollments
            .FirstOrDefaultAsync(e => e.AppId == appId && e.UserId == userId.ToString());

        if (enrollment is null)
            return NoContent();

        var ur = await _db.UserRoles
            .FirstOrDefaultAsync(ur => ur.EnrollmentId == enrollment.Id && ur.RoleId == roleId);

        if (ur is not null)
        {
            _db.UserRoles.Remove(ur);
            await _db.SaveChangesAsync();
        }

        return Ok(true);
    }
}