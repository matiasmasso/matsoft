using Identity.Data;
using Identity.Domain.Entities;
using Identity.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Controllers;

[ApiController]
[Route("roles")]
public class RoleManagementController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RoleManagementController(
        ApplicationDbContext db,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // ------------------------------------------------------------
    // GET /roles/app/{appId}
    // List all roles for an application
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
    // GET /roles/user/{userId}/app/{appId}
    // List user roles for an application
    // ------------------------------------------------------------
    [HttpGet("user/{userId:guid}/app/{appId:guid}")]
    public async Task<IActionResult> GetUserRoles(Guid userId, Guid appId)
    {
        var roles = await _db.UserRoles
            .Where(ur => ur.UserId == userId && ur.ApplicationId == appId)
            .Include(ur => ur.Role)
            .Select(ur => ur.Role.Name)
            .ToListAsync();

        return Ok(roles);
    }

    // ------------------------------------------------------------
    // POST /roles/assign
    // Assign a role to a user for an application
    // ------------------------------------------------------------
    [HttpPost("assign")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            return NotFound("User not found");

        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role == null)
            return NotFound("Role not found");

        if (role.ApplicationId != null && role.ApplicationId != request.ApplicationId)
            return BadRequest("Role does not belong to this application");

        var exists = await _db.UserRoles.AnyAsync(ur =>
            ur.UserId == request.UserId &&
            ur.RoleId == request.RoleId &&
            ur.ApplicationId == request.ApplicationId);

        if (exists)
            return BadRequest("User already has this role for this application");

        var userRole = new ApplicationUserRole
        {
            UserId = request.UserId,
            RoleId = request.RoleId,
            ApplicationId = request.ApplicationId
        };

        _db.UserRoles.Add(userRole);
        await _db.SaveChangesAsync();

        return Ok("Role assigned");
    }

    // ------------------------------------------------------------
    // POST /roles/remove
    // Remove a role from a user for an application
    // ------------------------------------------------------------
    [HttpPost("remove")]
    public async Task<IActionResult> RemoveRole([FromBody] RemoveRoleRequest request)
    {
        var userRole = await _db.UserRoles.FirstOrDefaultAsync(ur =>
            ur.UserId == request.UserId &&
            ur.RoleId == request.RoleId &&
            ur.ApplicationId == request.ApplicationId);

        if (userRole == null)
            return NotFound("User does not have this role for this application");

        _db.UserRoles.Remove(userRole);
        await _db.SaveChangesAsync();

        return Ok("Role removed");
    }
}
