using Identity.Api.Domain.Apps;
using Identity.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Application.Apps;

public class AppService
{
    private readonly IdentityDbContext _db;

    public AppService(IdentityDbContext db)
    {
        _db = db;
    }

    public async Task<App> CreateAppAsync(string key, string name)
    {
        key = key.Trim().ToLowerInvariant();

        if (await _db.Apps.AnyAsync(a => a.Key == key))
            throw new Exception($"App with key '{key}' already exists");

        var app = new App
        {
            Id = Guid.NewGuid(),
            Key = key,
            Name = name
        };

        _db.Apps.Add(app);
        await _db.SaveChangesAsync();

        return app;
    }

    public async Task<App> UpdateAppAsync(Guid appId, string name)
    {
        var app = await _db.Apps.FirstOrDefaultAsync(a => a.Id == appId);
        if (app == null)
            throw new Exception("App not found");
        app.Name = name;
        _db.Apps.Update(app);
        await _db.SaveChangesAsync();
        return app;
    }

    public async Task<App?> GetByKeyAsync(string key)
    {
        key = key.Trim().ToLowerInvariant();

        return await _db.Apps
            .Include(a => a.Roles)
            .Include(a => a.Enrollments)
            .FirstOrDefaultAsync(a => a.Key == key);
    }

    public async Task<List<App>> ListAppsAsync()
    {
            return await _db.Apps
                .OrderBy(a => a.Name)
                .ToListAsync();
    }

    public async Task DeleteAppAsync(Guid appId)
    {
        await using var tx = await _db.Database.BeginTransactionAsync();

        var app = await _db.Apps
            .Include(a => a.Enrollments)
            .Include(a => a.Roles)
            .FirstOrDefaultAsync(a => a.Id == appId);

        if (app == null)
            throw new Exception("App not found");

        if (app.Enrollments.Any())
            _db.UserAppEnrollments.RemoveRange(app.Enrollments);

        if (app.Roles.Any())
            _db.AppRoles.RemoveRange(app.Roles);

        _db.Apps.Remove(app);

        await _db.SaveChangesAsync();
        await tx.CommitAsync();
    }
}