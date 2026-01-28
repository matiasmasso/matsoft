using Identity.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(
        IServiceProvider services,
        IConfiguration config)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
        var users = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roles = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        // Apply migrations
        await db.Database.MigrateAsync();

        // -----------------------------------------
        // 1. Ensure GlobalAdmin role exists
        // -----------------------------------------
        const string globalAdminRole = "GlobalAdmin";

        if (!await roles.RoleExistsAsync(globalAdminRole))
        {
            await roles.CreateAsync(new ApplicationRole { Name = globalAdminRole });
        }

        // -----------------------------------------
        // 2. Create GlobalAdmin user
        // -----------------------------------------
        var adminEmail = config["Seed:AdminEmail"] ?? "admin@local";
        var adminPassword = config["Seed:AdminPassword"] ?? "Admin123!";

        var admin = await users.FindByEmailAsync(adminEmail);
        if (admin == null)
        {
            admin = new ApplicationUser
            {
                Email = adminEmail,
                UserName = adminEmail,
                EmailConfirmed = true,
                FullName = "Global Administrator"
            };

            var result = await users.CreateAsync(admin, adminPassword);
            if (!result.Succeeded)
                throw new Exception("Failed to create GlobalAdmin user: " +
                                    string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        // Ensure user has GlobalAdmin role
        if (!await users.IsInRoleAsync(admin, globalAdminRole))
        {
            await users.AddToRoleAsync(admin, globalAdminRole);
        }

        // -----------------------------------------
        // 3. Create default App (Identity.Admin)
        // -----------------------------------------
        const string defaultAppClientId = "identity-admin-client";

        var app = await db.Apps.FirstOrDefaultAsync(a => a.ClientId == defaultAppClientId);
        if (app == null)
        {
            app = new App
            {
                Name = "Identity.Admin",
                ClientId = defaultAppClientId,
                Description = "Administrative console for managing users and apps"
            };

            db.Apps.Add(app);
            await db.SaveChangesAsync();
        }

        // -----------------------------------------
        // 4. Enroll GlobalAdmin into the default app
        // -----------------------------------------
        var existingEnrollment = await db.AppUserRoles
            .FirstOrDefaultAsync(x => x.UserId == admin.Id && x.AppId == app.Id);

        if (existingEnrollment == null)
        {
            db.AppUserRoles.Add(new AppUserRole
            {
                UserId = admin.Id,
                AppId = app.Id,
                RoleName = "Admin"
            });

            await db.SaveChangesAsync();
        }
    }
}