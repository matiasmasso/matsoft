using Identity.Data;
using Identity.Domain.Entities;
using Identity.Services;
using Microsoft.EntityFrameworkCore;

public class RoleService : IRoleService
{
    private readonly IdentityDbContext _db;

    public RoleService(IdentityDbContext db)
    {
        _db = db;
    }

    public async Task<List<string>> GetAll()
        => await _db.Roles.Select(r => r.Name).ToListAsync();

    public async Task AssignRolesToUser(Guid userId, List<string> roles)
    {
        var user = await _db.Users.FindAsync(userId);
        if (user == null) return;

        // Remove old roles
        var existing = _db.UserRoles.Where(ur => ur.UserId == userId);
        _db.UserRoles.RemoveRange(existing);

        // Add new roles
        foreach (var roleName in roles)
        {
            var role = await _db.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
            {
                role = new Role { RoleId = Guid.NewGuid(), Name = roleName };
                _db.Roles.Add(role);
            }

            _db.UserRoles.Add(new UserRole
            {
                UserId = userId,
                RoleId = role.RoleId
            });
        }

        await _db.SaveChangesAsync();
    }
}
