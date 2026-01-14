using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<App> Apps => Set<App>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<UserApp> UserApps => Set<UserApp>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();


    protected override void OnModelCreating(ModelBuilder model)
    {
        base.OnModelCreating(model);

        model.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        model.Entity<UserApp>()
            .HasKey(ua => new { ua.UserId, ua.AppId });

        model.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId }); 
        
        model.Entity<UserApp>().HasKey(ua => new { ua.UserId, ua.AppId }); 
        

        var adminRoleId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var userRoleId = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

        var dashboardAppId = new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc");
        var reportsAppId = new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd");

        model.Entity<Role>().HasData(
            new Role { RoleId = adminRoleId, Name = "Admin" },
            new Role { RoleId = userRoleId, Name = "User" }
        );

        model.Entity<App>().HasData(
            new App { AppId = dashboardAppId, Name = "Dashboard" },
            new App { AppId = reportsAppId, Name = "Reports" }
        );
    }

}
