using Identity.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Api.Configuration;

public sealed class AppSecretConfiguration : IEntityTypeConfiguration<AppSecret>
{
    public void Configure(EntityTypeBuilder<AppSecret> b)
    {
        b.ToTable("AppSecrets");

        b.HasKey(x => x.Id);

        b.Property(x => x.Provider)
            .IsRequired()
            .HasMaxLength(100);

        b.Property(x => x.ClientId)
            .IsRequired()
            .HasMaxLength(200);

        b.Property(x => x.ClientSecret)
            .IsRequired()
            .HasMaxLength(500);

        b.HasOne(x => x.App)
            .WithMany(a => a.Secrets)
            .HasForeignKey(x => x.AppId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}