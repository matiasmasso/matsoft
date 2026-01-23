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
}