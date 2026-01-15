using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        Guid,
        ApplicationUserClaim,
        ApplicationUserRole,
        ApplicationUserLogin,
        ApplicationRoleClaim,
        ApplicationUserToken>
    {
        public DbSet<Application> Applications { get; set; }
        public DbSet<UserApplication> UserApplications { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // TABLE NAMES
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<ApplicationRole>().ToTable("Roles");
            builder.Entity<ApplicationUserRole>().ToTable("UserRoles");
            builder.Entity<ApplicationUserClaim>().ToTable("UserClaims");
            builder.Entity<ApplicationRoleClaim>().ToTable("RoleClaims");
            builder.Entity<ApplicationUserLogin>().ToTable("UserLogins");
            builder.Entity<ApplicationUserToken>().ToTable("UserTokens");

            builder.Entity<Application>().ToTable("Applications");
            builder.Entity<RefreshToken>().ToTable("RefreshTokens");
            builder.Entity<PasswordResetToken>().ToTable("PasswordResetTokens");

            // USERROLES (composite key)
            builder.Entity<ApplicationUserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId, ur.ApplicationId });

            builder.Entity<ApplicationUserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            builder.Entity<ApplicationUserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            builder.Entity<ApplicationUserRole>()
                .HasOne(ur => ur.Application)
                .WithMany(a => a.UserRoles)
                .HasForeignKey(ur => ur.ApplicationId);

            // ROLE → APPLICATION (optional)
            builder.Entity<ApplicationRole>()
                .HasOne(r => r.Application)
                .WithMany(a => a.Roles)
                .HasForeignKey(r => r.ApplicationId)
                .OnDelete(DeleteBehavior.Restrict);

            // REFRESH TOKENS
            builder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId);

            builder.Entity<RefreshToken>()
                .HasOne(rt => rt.Application)
                .WithMany(a => a.RefreshTokens)
                .HasForeignKey(rt => rt.ApplicationId);

            builder.Entity<RefreshToken>()
                .HasIndex(rt => new { rt.UserId, rt.ApplicationId });

            builder.Entity<RefreshToken>()
                .HasIndex(rt => rt.Token)
                .IsUnique();

            // PASSWORD RESET TOKENS
            builder.Entity<PasswordResetToken>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId);

            builder.Entity<PasswordResetToken>()
                .HasIndex(p => p.Token)
                .IsUnique();
        }
    }

}
