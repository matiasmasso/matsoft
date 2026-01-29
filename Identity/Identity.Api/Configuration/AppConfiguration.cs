using Identity.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Api.Configuration;

public sealed class AppConfiguration : IEntityTypeConfiguration<App>
{
    public void Configure(EntityTypeBuilder<App> b)
    {
        b.ToTable("Apps");

        b.HasKey(x => x.Id);

        b.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        b.Property(x => x.ClientId)
            .IsRequired()
            .HasMaxLength(200);

        b.Property(x => x.Description)
            .HasMaxLength(500);

        b.HasMany(x => x.Roles)
            .WithOne(r => r.App)
            .HasForeignKey(r => r.AppId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasMany(x => x.UserApps)
            .WithOne(ua => ua.App)
            .HasForeignKey(ua => ua.AppId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasMany(x => x.Secrets)
            .WithOne(s => s.App)
            .HasForeignKey(s => s.AppId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}