using Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Controllers;

[ApiController]
[Route("roles")]
public class RoleAdminController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public RoleAdminController(ApplicationDbContext db)
    {
        _db = db;
    }

    // ------------------------------------------------------------
    // DELETE /roles/{roleId}
    // ------------------------------------------------------------
    [HttpDelete("{roleId:guid}")]
    public async Task<IActionResult> DeleteRole(Guid roleId)
    {
        var role = await _db.Roles
            .Include(r => r.UserRoles)
            .FirstOrDefaultAsync(r => r.Id == roleId);

        if (role == null)
            return NotFound("Role not found");

        if (role.UserRoles.Any())
            return BadRequest("Cannot delete role because it is assigned to users");

        _db.Roles.Remove(role);
        await _db.SaveChangesAsync();

        return Ok("Role deleted");
    }

    // ------------------------------------------------------------
    // PUT /roles/{roleId}
    // ------------------------------------------------------------
    [HttpPut("{roleId:guid}")]
    public async Task<IActionResult> UpdateRole(Guid roleId, [FromBody] UpdateRoleRequest request)
    {
        var role = await _db.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
        if (role == null)
            return NotFound("Role not found");

        // Validate application if changed
        if (request.ApplicationId != null)
        {
            var appExists = await _db.Applications.AnyAsync(a => a.Id == request.ApplicationId);
            if (!appExists)
                return BadRequest("Application does not exist");
        }

        // Check duplicate name within the same app
        var normalized = request.Name.ToUpperInvariant();
        var duplicate = await _db.Roles.AnyAsync(r =>
            r.Id != roleId &&
            r.NormalizedName == normalized &&
            r.ApplicationId == request.ApplicationId);

        if (duplicate)
            return BadRequest("A role with this name already exists in this application");

        role.Name = request.Name;
        role.NormalizedName = normalized;
        role.ApplicationId = request.ApplicationId;

        await _db.SaveChangesAsync();

        return Ok("Role updated");
    }

}
