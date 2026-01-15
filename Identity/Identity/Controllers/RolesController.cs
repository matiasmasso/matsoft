using Identity.Data;
using Identity.Domain.Entities;
using Identity.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Controllers;

[ApiController]
[Route("roles")]
public class RolesController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public RolesController(ApplicationDbContext db)
    {
        _db = db;
    }

    // ------------------------------------------------------------
    // GET /roles/app/{appId}
    // ------------------------------------------------------------
    [HttpGet("app/{appId:guid}")]
    public async Task<ActionResult<List<RoleDto>>> GetRolesForApp(Guid appId)
    {
        IQueryable<ApplicationRole> query = _db.Roles;

        if (appId == Guid.Empty)
            query = query.Where(r => r.ApplicationId == null);
        else
            query = query.Where(r => r.ApplicationId == appId);

        var roles = await query
            .Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name,
                ApplicationId = r.ApplicationId
            })
            .ToListAsync();

        return Ok(roles);
    }

    // ------------------------------------------------------------
    // POST /roles
    // ------------------------------------------------------------
    [HttpPost]
    public async Task<ActionResult<RoleDto>> CreateRole(CreateRoleRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Role name is required");

        bool exists = await _db.Roles.AnyAsync(r =>
            r.Name == request.Name &&
            r.ApplicationId == request.ApplicationId
        );

        if (exists)
            return BadRequest("Role already exists for this application");

        var role = new ApplicationRole
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            NormalizedName = request.Name.ToUpperInvariant(),
            ApplicationId = request.ApplicationId
        };

        _db.Roles.Add(role);
        await _db.SaveChangesAsync();

        var dto = new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            ApplicationId = role.ApplicationId
        };

        return CreatedAtAction(nameof(GetRolesForApp),
            new { appId = request.ApplicationId ?? Guid.Empty },
            dto);
    }

    // ------------------------------------------------------------
    // PUT /roles/{roleId}
    // ------------------------------------------------------------
    [HttpPut("{roleId:guid}")]
    public async Task<ActionResult> UpdateRole(Guid roleId, UpdateRoleRequest request)
    {
        var role = await _db.Roles.FindAsync(roleId);
        if (role == null)
            return NotFound();

        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Role name is required");

        bool exists = await _db.Roles.AnyAsync(r =>
            r.Id != roleId &&
            r.Name == request.Name &&
            r.ApplicationId == request.ApplicationId
        );

        if (exists)
            return BadRequest("Another role with this name already exists");

        role.Name = request.Name;
        role.NormalizedName = request.Name.ToUpperInvariant();
        role.ApplicationId = request.ApplicationId;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    // ------------------------------------------------------------
    // DELETE /roles/{roleId}
    // ------------------------------------------------------------
    [HttpDelete("{roleId:guid}")]
    public async Task<ActionResult> DeleteRole(Guid roleId)
    {
        var role = await _db.Roles.FindAsync(roleId);
        if (role == null)
            return NotFound();

        _db.Roles.Remove(role);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}