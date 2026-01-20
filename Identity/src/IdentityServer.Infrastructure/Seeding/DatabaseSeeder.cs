using IdentityServer.Domain.Entities;
using IdentityServer.Infrastructure.Identity;
using IdentityServer.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Infrastructure.Seeding;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext db,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        await db.Database.MigrateAsync();

        // 1. Ensure Admin Role
        var adminRole = await roleManager.FindByNameAsync("Admin");
        if (adminRole == null)
        {
            adminRole = new ApplicationRole { Name = "Admin" };
            await roleManager.CreateAsync(adminRole);
        }

        var adminUser = await userManager.FindByEmailAsync("admin@local");
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = "admin@local",
                Email = "admin@local",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(adminUser, "Admin123$");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        // 3. Ensure OdbcClient: identity-manager
        var client = await db.OdbcClients
            .FirstOrDefaultAsync(c => c.ClientId == "identity-manager");

        if (client == null)
        {
            client = new OdbcClient
            {
                Id = Guid.NewGuid(),
                ClientId = "identity-manager",
                ClientName = "Identity Manager",
                ClientSecret = null, // PKCE
                RequirePkce = true,
                RequireClientSecret = false,

                RedirectUrisJson = "[\"https://local.identitymanager.test:7273/auth/callback\"]",
                PostLogoutRedirectUrisJson = "[\"https://local.identitymanager.test:7273/\"]",
                AllowedCorsOriginsJson = "[\"https://local.identitymanager.test:7273\"]",

                //RedirectUrisJson = "[\"https://localhost:7273/auth/callback\"]",
                //PostLogoutRedirectUrisJson = "[\"https://localhost:7273/logout/callback\"]",
                //AllowedCorsOriginsJson = "[\"https://localhost:7273\"]",
                AllowedScopesJson = "[\"openid\",\"profile\",\"email\",\"roles\"]",
                CreatedAt = DateTime.UtcNow
            };

            db.OdbcClients.Add(client);
            await db.SaveChangesAsync();
        }

    }
}