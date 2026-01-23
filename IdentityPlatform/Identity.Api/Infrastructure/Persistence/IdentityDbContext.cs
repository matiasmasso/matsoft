using Identity.Api.Application.Auth;
using Identity.Api.Domain.Apps;
using Identity.Api.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Infrastructure.Persistence;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<App> Apps => Set<App>();
    public DbSet<AppRole> AppRoles => Set<AppRole>();
    public DbSet<UserAppEnrollment> UserAppEnrollments => Set<UserAppEnrollment>();
    public DbSet<UserRole> UserRoles => Set<UserRole>(); public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.Email).IsUnique();
        });

        modelBuilder.Entity<App>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.Key).IsUnique();
        });

        modelBuilder.Entity<AppRole>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => new { x.AppId, x.Name }).IsUnique();
        });

        modelBuilder.Entity<UserAppEnrollment>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.HasIndex(x => new { x.UserId, x.AppId }).IsUnique();

            entity.HasOne(x => x.User)
                .WithMany(u => u.AppEnrollments)
                .HasForeignKey(x => x.UserId);

            entity.HasOne(x => x.App)
                .WithMany(a => a.Enrollments)
                .HasForeignKey(x => x.AppId);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.HasIndex(x => new { x.EnrollmentId, x.RoleId }).IsUnique();

            entity.HasOne(x => x.Enrollment)
                .WithMany(e => e.Roles)
                .HasForeignKey(x => x.EnrollmentId);

            entity.HasOne(x => x.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(x => x.RoleId);
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.Token).IsUnique();
        });


    }
}