using Identity.Api.Data;
using Identity.Api.Entities;
using Identity.Contracts.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("user-app-roles")]
public sealed class UserAppRolesController : ControllerBase
{
    private readonly AppDbContext _db;

    public UserAppRolesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("{userId:guid}/{appId:guid}")]
    public async Task<IEnumerable<UserAppRoleDto>> GetRoles(Guid userId, Guid appId)
    {
        var roles = await _db.AppRoles
            .Where(r => r.AppId == appId)
            .Select(r => new UserAppRoleDto
            {
                RoleId = r.Id,
                RoleName = r.Name,
                Assigned = _db.UserAppRoles.Any(uar => uar.UserId == userId && uar.AppRoleId == r.Id)
            })
            .ToListAsync();

        return roles;
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateRoles(UpdateUserAppRolesRequest dto)
    {
        var existing = await _db.UserAppRoles
            .Where(uar => uar.UserId == dto.UserId &&
                          uar.AppRole.AppId == dto.AppId)
            .ToListAsync();

        _db.UserAppRoles.RemoveRange(existing);

        foreach (var roleId in dto.RoleIds)
        {
            _db.UserAppRoles.Add(new UserAppRole
            {
                UserId = dto.UserId,
                AppRoleId = roleId
            });
        }

        await _db.SaveChangesAsync();
        return NoContent();
    }
}