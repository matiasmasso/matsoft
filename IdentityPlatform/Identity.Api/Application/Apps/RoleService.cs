using Identity.Api.Domain.Apps;
using Identity.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Application.Apps;

public class RoleService
{
    private readonly IdentityDbContext _db;

    public RoleService(IdentityDbContext db)
    {
        _db = db;
    }

    public async Task<AppRole> CreateRoleAsync(Guid appId, string roleName)
    {
        roleName = roleName.Trim();

        var app = await _db.Apps.FirstOrDefaultAsync(a => a.Id == appId);
        if (app == null)
            throw new Exception("App not found");

        if (await _db.AppRoles.AnyAsync(r => r.AppId == appId && r.Name == roleName))
            throw new Exception($"Role '{roleName}' already exists in this app");

        var role = new AppRole
        {
            Id = Guid.NewGuid(),
            AppId = appId,
            Name = roleName
        };

        _db.AppRoles.Add(role);
        await _db.SaveChangesAsync();

        return role;
    }

    public async Task<List<AppRole>> ListRolesAsync(Guid appId)
    {
        return await _db.AppRoles
            .Where(r => r.AppId == appId)
            .OrderBy(r => r.Name)
            .ToListAsync();
    }

    public async Task<AppRole?> GetRoleAsync(Guid roleId)
    {
        return await _db.AppRoles
            .Include(r => r.App)
            .FirstOrDefaultAsync(r => r.Id == roleId);
    }
}