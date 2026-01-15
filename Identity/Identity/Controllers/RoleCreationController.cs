namespace Identity.Controllers;

using Identity.Data;
using Identity.Domain.Entities;
using Identity.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("roles")]
public class RoleCreationController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public RoleCreationController(ApplicationDbContext db)
    {
        _db = db;
    }

    // ------------------------------------------------------------
    // POST /roles/create
    // Create a new role for an application (or global)
    // ------------------------------------------------------------
    [HttpPost("create")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
    {
        // Validate application if provided
        if (request.ApplicationId != null)
        {
            var appExists = await _db.Applications
                .AnyAsync(a => a.Id == request.ApplicationId);

            if (!appExists)
                return NotFound("Application not found");
        }

        // Check for duplicates
        var normalized = request.Name.ToUpperInvariant();

        var exists = await _db.Roles.AnyAsync(r =>
            r.NormalizedName == normalized &&
            r.ApplicationId == request.ApplicationId);

        if (exists)
            return BadRequest("Role already exists for this application");

        var role = new ApplicationRole
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            NormalizedName = normalized,
            ApplicationId = request.ApplicationId
        };

        _db.Roles.Add(role);
        await _db.SaveChangesAsync();

        return Ok(new
        {
            Message = "Role created",
            RoleId = role.Id,
            role.Name,
            role.ApplicationId
        });
    }

    // ------------------------------------------------------------
    // GET /roles/app/{appId}
    // List roles for an application
    // ------------------------------------------------------------
    [HttpGet("app/{appId:guid}")]
    public async Task<IActionResult> GetRolesForApp(Guid appId)
    {
        var roles = await _db.Roles
            .Where(r => r.ApplicationId == appId || r.ApplicationId == null)
            .Select(r => new { r.Id, r.Name, r.ApplicationId })
            .ToListAsync();

        return Ok(roles);
    }

    // ------------------------------------------------------------
    // GET /roles/global
    // List global roles
    // ------------------------------------------------------------
    [HttpGet("global")]
    public async Task<IActionResult> GetGlobalRoles()
    {
        var roles = await _db.Roles
            .Where(r => r.ApplicationId == null)
            .Select(r => new { r.Id, r.Name })
            .ToListAsync();

        return Ok(roles);
    }
}

