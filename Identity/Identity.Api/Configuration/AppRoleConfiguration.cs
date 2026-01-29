using Identity.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Api.Configuration;

public sealed class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> b)
    {
        b.ToTable("AppRoles");

        b.HasKey(x => x.Id);

        b.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        b.Property(x => x.Description)
            .HasMaxLength(500);

        b.HasMany(x => x.UserAppRoles)
            .WithOne(uar => uar.AppRole)
            .HasForeignKey(uar => uar.AppRoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}