using Microsoft.EntityFrameworkCore;
using Identity.Domain.Entities;
using Identity.Infrastructure.Configuration;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Identity.Data
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<App> Apps => Set<App>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<AppRole> AppRoles => Set<AppRole>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
                public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AppConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());

            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            // Seed data goes here
        }
    }

}
