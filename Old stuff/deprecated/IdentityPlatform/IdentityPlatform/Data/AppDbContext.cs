using IdentityPlatform.Auth.Models;
using IdentityPlatform.Components;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IdentityPlatform.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<ClientApp> Apps => Set<ClientApp>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserAppEnrollment> Enrollments => Set<UserAppEnrollment>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAppEnrollment>()
            .HasKey(x => new { x.UserId, x.AppId });

        modelBuilder.Entity<UserAppEnrollment>()
            .HasOne(x => x.User)
            .WithMany(x => x.Enrollments)
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<UserAppEnrollment>()
            .HasOne(x => x.App)
            .WithMany(x => x.Enrollments)
            .HasForeignKey(x => x.AppId);

        modelBuilder.Entity<UserAppEnrollment>()
            .HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);
    }
}