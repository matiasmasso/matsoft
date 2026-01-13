using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configuration
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasKey(x => x.AppRoleId);

            builder.HasIndex(x => new { x.AppId, x.RoleId })
                .IsUnique();

            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.AppRole)
                .HasForeignKey(x => x.AppRoleId);
        }
    }

}
