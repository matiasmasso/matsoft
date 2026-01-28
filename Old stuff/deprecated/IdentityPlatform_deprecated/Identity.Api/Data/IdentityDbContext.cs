using Identity.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Data;

public class IdentityDbContext
    : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
    }

    public DbSet<App> Apps => Set<App>();
    public DbSet<AppUserRole> AppUserRoles => Set<AppUserRole>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUserRole>()
            .HasOne(aur => aur.User)
            .WithMany()
            .HasForeignKey(aur => aur.UserId);

        builder.Entity<AppUserRole>()
            .HasOne(aur => aur.App)
            .WithMany(a => a.UserRoles)
            .HasForeignKey(aur => aur.AppId);
    }
}