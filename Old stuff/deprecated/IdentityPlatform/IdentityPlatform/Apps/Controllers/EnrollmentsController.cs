using IdentityPlatform.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityPlatform.Apps.Controllers;

[ApiController]
[Route("apps/{appId:guid}/enrollments")]
[Authorize(Roles = "Admin")]
public class EnrollmentsController : ControllerBase
{
    private readonly AppDbContext _db;

    public EnrollmentsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid appId)
    {
        var result = await _db.Enrollments
            .Include(e => e.User)
            .Include(e => e.Role)
            .Where(e => e.AppId == appId)
            .Select(e => new
            {
                e.UserId,
                e.User.Email,
                e.RoleId,
                Role = e.Role.Name,
                e.IsActive
            })
            .ToListAsync();

        return Ok(result);
    }

    [HttpPost("{userId:guid}/role/{roleId:guid}")]
    public async Task<IActionResult> ChangeRole(Guid appId, Guid userId, Guid roleId)
    {
        var enrollment = await _db.Enrollments
            .FirstOrDefaultAsync(e => e.AppId == appId && e.UserId == userId);

        if (enrollment is null) return NotFound();

        enrollment.RoleId = roleId;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{userId:guid}/toggle")]
    public async Task<IActionResult> Toggle(Guid appId, Guid userId)
    {
        var enrollment = await _db.Enrollments
            .FirstOrDefaultAsync(e => e.AppId == appId && e.UserId == userId);

        if (enrollment is null) return NotFound();

        enrollment.IsActive = !enrollment.IsActive;
        await _db.SaveChangesAsync();

        return NoContent();
    }
}