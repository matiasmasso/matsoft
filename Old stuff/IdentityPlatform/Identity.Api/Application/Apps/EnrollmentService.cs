using Identity.Api.Data;
using Identity.Api.Domain.Apps;
using Identity.Api.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Application.Apps;

public class EnrollmentService
{
    private readonly IdentityDbContext _db;

    public EnrollmentService(IdentityDbContext db)
    {
        _db = db;
    }

    // ------------------------------------------------------------
    // 1. Enroll a user into an app
    // ------------------------------------------------------------
    public async Task<UserAppEnrollment> EnrollUserAsync(string userId, Guid appId)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            throw new Exception("User not found");

        var app = await _db.Apps.FirstOrDefaultAsync(a => a.Id == appId);
        if (app == null)
            throw new Exception("App not found");

        if (await _db.UserAppEnrollments.AnyAsync(e => e.UserId == userId && e.AppId == appId))
            throw new Exception("User is already enrolled in this app");

        var enrollment = new UserAppEnrollment
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            AppId = appId
        };

        _db.UserAppEnrollments.Add(enrollment);
        await _db.SaveChangesAsync();

        return enrollment;
    }

    // ------------------------------------------------------------
    // 2. Assign a role to a user enrollment
    // ------------------------------------------------------------
    public async Task<UserRole> AssignRoleAsync(Guid enrollmentId, Guid roleId)
    {
        var enrollment = await _db.UserAppEnrollments
            .Include(e => e.Roles)
            .FirstOrDefaultAsync(e => e.Id == enrollmentId);

        if (enrollment == null)
            throw new Exception("Enrollment not found");

        var role = await _db.AppRoles.FirstOrDefaultAsync(r => r.Id == roleId);
        if (role == null)
            throw new Exception("Role not found");

        if (role.AppId != enrollment.AppId)
            throw new Exception("Role does not belong to this app");

        if (await _db.UserRoles.AnyAsync(ur => ur.EnrollmentId == enrollmentId && ur.RoleId == roleId))
            throw new Exception("User already has this role");

        var userRole = new UserRole
        {
            Id = Guid.NewGuid(),
            EnrollmentId = enrollmentId,
            RoleId = roleId
        };

        _db.UserRoles.Add(userRole);
        await _db.SaveChangesAsync();

        return userRole;
    }

    // ------------------------------------------------------------
    // 3. Remove a role from a user enrollment
    // ------------------------------------------------------------
    public async Task RemoveRoleAsync(Guid enrollmentId, Guid roleId)
    {
        var userRole = await _db.UserRoles
            .FirstOrDefaultAsync(ur => ur.EnrollmentId == enrollmentId && ur.RoleId == roleId);

        if (userRole == null)
            throw new Exception("Role assignment not found");

        _db.UserRoles.Remove(userRole);
        await _db.SaveChangesAsync();
    }

    // ------------------------------------------------------------
    // 4. List all users enrolled in an app
    // ------------------------------------------------------------
    public async Task<List<UserAppEnrollment>> ListUsersInAppAsync(Guid appId)
    {
        return await _db.UserAppEnrollments
            .Include(e => e.User)
            .Include(e => e.Roles)
                .ThenInclude(ur => ur.Role)
            .Where(e => e.AppId == appId)
            .ToListAsync();
    }

    // ------------------------------------------------------------
    // 5. Get roles for a user in a specific app
    // ------------------------------------------------------------
    public async Task<List<string>> GetUserRolesAsync(string userId, string appKey)
    {
        appKey = appKey.Trim().ToLowerInvariant();

        var app = await _db.Apps.FirstOrDefaultAsync(a => a.Key == appKey);
        if (app == null)
            return new List<string>();

        var enrollment = await _db.UserAppEnrollments
            .Include(e => e.Roles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(e => e.UserId == userId && e.AppId == app.Id);

        if (enrollment == null)
            return new List<string>();

        return enrollment.Roles
            .Select(r => r.Role.Name)
            .OrderBy(n => n)
            .ToList();
    }
}