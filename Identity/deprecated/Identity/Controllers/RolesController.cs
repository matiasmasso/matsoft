using Azure.Core;
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
    // GLOBAL ROLES GET /roles/  
    // ------------------------------------------------------------
    [HttpGet]
    public async Task<ActionResult<List<RoleDto>>> GetGlobalRoles()
    {
        IQueryable<ApplicationRole> query = _db.Roles;

        query = query.Where(r => r.ApplicationId == null);

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
    public async Task<ActionResult> Update(RoleDto value)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(value.Name))
                return BadRequest("Cal un nom per aquest rol");

            bool exists = await _db.Roles.AnyAsync(r =>
                r.Id != value.Id &&
                r.Name == value.Name &&
                r.ApplicationId == value.ApplicationId
            );

            if (exists)
                return BadRequest("Aquest rol ja existeix per aquesta aplicació");

            var entity = await _db.Roles.FindAsync(value.Id);
            if (entity == null)
            {
                entity = new ApplicationRole();
                entity.Id = value.Id;
                _db.Roles.Add(entity);
            }

            entity.Name = value.Name;
            entity.NormalizedName = value.Name.ToUpperInvariant();
            entity.ApplicationId = value.ApplicationId;

            await _db.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


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