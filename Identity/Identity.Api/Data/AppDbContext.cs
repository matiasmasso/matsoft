using Identity.Api.Configuration;
using Identity.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<App> Apps => Set<App>();
    public DbSet<AppRole> AppRoles => Set<AppRole>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserApp> UserApps => Set<UserApp>();
    public DbSet<UserAppRole> UserAppRoles => Set<UserAppRole>();
    public DbSet<AppSecret> AppSecrets => Set<AppSecret>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AppConfiguration());
        modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserAppConfiguration());
        modelBuilder.ApplyConfiguration(new UserAppRoleConfiguration());
        modelBuilder.ApplyConfiguration(new AppSecretConfiguration());
    }
}