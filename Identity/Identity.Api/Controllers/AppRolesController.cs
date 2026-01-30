using Identity.Api.Data;
using Identity.Api.Entities;
using Identity.Contracts.Apps;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("apps/{appId:guid}/roles")]
public sealed class AppRolesController : ControllerBase
{
    private readonly AppDbContext _db;

    public AppRolesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IEnumerable<AppRoleDto>> GetRoles(Guid appId)
    {
        return await _db.AppRoles
            .Where(r => r.AppId == appId)
            .Select(r => new AppRoleDto
            {
                Id = r.Id,
                AppId = r.AppId,
                Name = r.Name,
                Description = r.Description
            })
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<AppRoleDto>> Create(Guid appId, CreateAppRoleRequest dto)
    {
        if (appId != dto.AppId)
            return BadRequest("AppId mismatch");

        var exists = await _db.AppRoles
            .AnyAsync(r => r.AppId == appId && r.Name == dto.Name);

        if (exists)
            return Conflict("A role with this name already exists in this app.");

        var role = new AppRole
        {
            Id = Guid.NewGuid(),
            AppId = appId,
            Name = dto.Name,
            Description = dto.Description
        };

        _db.AppRoles.Add(role);
        await _db.SaveChangesAsync();

        return new AppRoleDto
        {
            Id = role.Id,
            AppId = role.AppId,
            Name = role.Name,
            Description = role.Description
        };
    }

    [HttpPut("{roleId:guid}")]
    public async Task<IActionResult> Update(Guid appId, Guid roleId, UpdateAppRoleRequest dto)
    {
        if (roleId != dto.Id)
            return BadRequest("RoleId mismatch");

        var role = await _db.AppRoles.FindAsync(roleId);
        if (role is null)
            return NotFound();

        if (role.AppId != appId)
            return BadRequest("Role does not belong to this app.");

        role.Name = dto.Name;
        role.Description = dto.Description;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{roleId:guid}")]
    public async Task<IActionResult> Delete(Guid appId, Guid roleId)
    {
        var role = await _db.AppRoles.FindAsync(roleId);
        if (role is null)
            return NotFound();

        if (role.AppId != appId)
            return BadRequest("Role does not belong to this app.");

        _db.AppRoles.Remove(role);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}