using IdentityServer.Domain.Entities;
using IdentityServer.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdentityServer.Infrastructure.Persistence;

public class ApplicationDbContext
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> UsersDomain => Set<User>();
    public DbSet<OdbcClient> OdbcClients => Set<OdbcClient>();
    public DbSet<ExternalLogin> ExternalLogins => Set<ExternalLogin>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<AuthorizationCode> AuthorizationCodes => Set<AuthorizationCode>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<ApplicationUser>().ToTable("AspNetUsers");
        builder.Entity<ApplicationRole>().ToTable("AspNetRoles");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("AspNetUserRoles");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("AspNetUserClaims");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("AspNetUserLogins");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AspNetRoleClaims");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("AspNetUserTokens");
    }
}