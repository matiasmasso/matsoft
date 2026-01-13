using Identity.Data;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IdentityDbContext _db;

        public RoleService(IdentityDbContext db)
        {
            _db = db;
        }

        public async Task AssignRole(Guid userId, Guid appId, Guid roleId)
        {
            // Find the AppRole (role scoped to the app)
            var appRole = await _db.AppRoles
                .FirstOrDefaultAsync(ar => ar.AppId == appId && ar.RoleId == roleId);

            if (appRole == null)
                throw new Exception("AppRole not found for this app and role");

            // Check if already assigned
            var exists = await _db.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.AppRoleId == appRole.AppRoleId);

            if (exists)
                return;

            // Assign role
            var userRole = new UserRole
            {
                UserRoleId = Guid.NewGuid(),
                UserId = userId,
                AppRoleId = appRole.AppRoleId
            };

            _db.UserRoles.Add(userRole);
            await _db.SaveChangesAsync();
        }
    }
}

