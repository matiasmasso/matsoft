using Identity.Data;
using Identity.Domain.Entities;
using Identity.Services;
using Microsoft.EntityFrameworkCore;

public class AppService : IAppService
{
    private readonly IdentityDbContext _db;

    public AppService(IdentityDbContext db)
    {
        _db = db;
    }

    public async Task<List<string>> GetAll()
        => await _db.Apps.Select(a => a.Name).ToListAsync();

    public async Task AssignAppsToUser(Guid userId, List<string> apps)
    {
        var user = await _db.Users.FindAsync(userId);
        if (user == null) return;

        // Remove old apps
        var existing = _db.UserApps.Where(ua => ua.UserId == userId);
        _db.UserApps.RemoveRange(existing);

        // Add new apps
        foreach (var appName in apps)
        {
            var app = await _db.Apps.FirstOrDefaultAsync(a => a.Name == appName);
            if (app == null)
            {
                app = new App { AppId = Guid.NewGuid(), Name = appName };
                _db.Apps.Add(app);
            }

            _db.UserApps.Add(new UserApp
            {
                UserId = userId,
                AppId = app.AppId
            });
        }

        await _db.SaveChangesAsync();
    }
}
