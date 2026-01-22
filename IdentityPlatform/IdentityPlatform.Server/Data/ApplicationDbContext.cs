using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityPlatform.Server.Domain.Models;

namespace IdentityPlatform.Server.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<ClientApp> ClientApps => Set<ClientApp>();
    public DbSet<UserAppEnrollment> UserAppEnrollments => Set<UserAppEnrollment>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // UserAppEnrollment
        builder.Entity<UserAppEnrollment>()
            .HasOne(e => e.User)
            .WithMany(u => u.Enrollments)
            .HasForeignKey(e => e.UserId);

        builder.Entity<UserAppEnrollment>()
            .HasOne(e => e.ClientApp)
            .WithMany(a => a.Enrollments)
            .HasForeignKey(e => e.ClientAppId);

        // RefreshToken
        builder.Entity<RefreshToken>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId);

        builder.Entity<RefreshToken>()
            .HasOne(t => t.ClientApp)
            .WithMany()
            .HasForeignKey(t => t.ClientAppId);
    }
}