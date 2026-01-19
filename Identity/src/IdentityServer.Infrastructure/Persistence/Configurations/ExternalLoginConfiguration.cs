using IdentityServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Infrastructure.Persistence.Configurations;

public class ExternalLoginConfiguration : IEntityTypeConfiguration<ExternalLogin>
{
    public void Configure(EntityTypeBuilder<ExternalLogin> builder)
    {
        builder.ToTable("ExternalLogins");

        builder.HasKey(el => el.Id);

        builder.Property(el => el.Provider)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(el => el.ProviderKey)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(el => new { el.Provider, el.ProviderKey })
            .IsUnique();

        builder.Property(el => el.CreatedAt).IsRequired();

        builder.HasOne(el => el.User)
            .WithMany(u => u.ExternalLogins)
            .HasForeignKey(el => el.UserId);
    }
}