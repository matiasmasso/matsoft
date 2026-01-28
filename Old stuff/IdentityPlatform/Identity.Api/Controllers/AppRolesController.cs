using Identity.Api.Data;
using Identity.Api.Domain.Apps;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("apps/{appId:guid}/roles")]
public class AppRolesController : ControllerBase
{
    private readonly IdentityDbContext _db;

    public AppRolesController(IdentityDbContext db)
    {
        _db = db;
    }

    // GET /apps/{appId}/roles
    [HttpGet]
    public async Task<ActionResult<List<AppRoleDto>>> GetAll(Guid appId)
    {
        var roles = await _db.AppRoles
            .Where(r => r.AppId == appId)
            .ToListAsync();

        return roles.Select(r => r.ToDto()).ToList();
    }

    // POST /apps/{appId}/roles
    [HttpPost]
    public async Task<ActionResult<AppRoleDto>> Create(Guid appId, CreateAppRoleRequest request)
    {
        var role = new AppRole
        {
            Id = Guid.NewGuid(),
            AppId = appId,
            Name = request.Name
        };

        _db.AppRoles.Add(role);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { appId }, role.ToDto());
    }

    // PUT /apps/{appId}/roles/{roleId}
    [HttpPut("{roleId:guid}")]
    public async Task<ActionResult<AppRoleDto>> Update(Guid appId, Guid roleId, UpdateAppRoleRequest request)
    {
        var role = await _db.AppRoles
            .FirstOrDefaultAsync(r => r.Id == roleId && r.AppId == appId);

        if (role is null)
            return NotFound();

        role.Name = request.Name;

        await _db.SaveChangesAsync();

        return role.ToDto();
    }

    // DELETE /apps/{appId}/roles/{roleId}
    [HttpDelete("{roleId:guid}")]
    public async Task<ActionResult> Delete(Guid appId, Guid roleId)
    {
        var role = await _db.AppRoles
            .FirstOrDefaultAsync(r => r.Id == roleId && r.AppId == appId);

        if (role is null)
            return NotFound();

        _db.AppRoles.Remove(role);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}