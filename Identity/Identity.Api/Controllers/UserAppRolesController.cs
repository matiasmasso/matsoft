using Identity.Api.Data;
using Identity.Api.Entities;
using Identity.Contracts.Apps;
using Identity.Contracts.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("users")]
public sealed class UserAppRolesController : ControllerBase
{
    private readonly AppDbContext _db;

    public UserAppRolesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("{userId:guid}/apps/{appId:guid}/roles")]
    public async Task<List<AppRoleAssignmentDto>> GetAssignments(Guid userId, Guid appId)
    {
        var allRoles = await _db.AppRoles
            .Where(r => r.AppId == appId)
            .ToListAsync();

        var assignedRoleIds = await _db.UserAppRoles
            .Where(x => x.UserId == userId && x.AppRole.AppId == appId)
            .Select(x => x.AppRoleId)
            .ToListAsync();

        return allRoles
            .Select(r => new AppRoleAssignmentDto
            {
                Id = r.Id,
                Name = r.Name,
                Assigned = assignedRoleIds.Contains(r.Id)
            })
            .ToList();
    }

    [HttpPut("{userId:guid}/apps/{appId:guid}/roles")]
    public async Task<IActionResult> Update(Guid userId, Guid appId, UpdateUserAppRolesRequest dto)
    {
        var existing = _db.UserAppRoles
            .Where(x => x.UserId == userId && x.AppRole.AppId == appId);

        _db.UserAppRoles.RemoveRange(existing);

        foreach (var roleId in dto.RoleIds)
        {
            _db.UserAppRoles.Add(new UserAppRole
            {
                UserId = userId,
                AppRoleId = roleId
            });
        }

        await _db.SaveChangesAsync();
        return Ok(true);
    }
}