using Identity.Api.Data;
using Identity.Api.Entities;
using Identity.Contracts.Apps;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("app-roles")]
public sealed class AppRolesController : ControllerBase
{
    private readonly AppDbContext _db;

    public AppRolesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("{appId:guid}")]
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
    public async Task<ActionResult<AppRoleDto>> Create(CreateAppRoleRequest dto)
    {
        var role = new AppRole
        {
            Id = Guid.NewGuid(),
            AppId = dto.AppId,
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

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateAppRoleRequest dto)
    {
        if (id != dto.Id) return BadRequest();

        var role = await _db.AppRoles.FindAsync(id);
        if (role is null) return NotFound();

        role.Name = dto.Name;
        role.Description = dto.Description;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var role = await _db.AppRoles.FindAsync(id);
        if (role is null) return NotFound();

        _db.AppRoles.Remove(role);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}