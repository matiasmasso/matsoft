using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OdbcClientConfiguration : IEntityTypeConfiguration<OdbcClient>
{
    public void Configure(EntityTypeBuilder<OdbcClient> builder)
    {
        builder.ToTable("OdbcClients");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.ClientId)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(c => c.ClientId).IsUnique();

        builder.Property(c => c.ClientName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.RedirectUrisJson).IsRequired();
        builder.Property(c => c.PostLogoutRedirectUrisJson).IsRequired();
        builder.Property(c => c.AllowedCorsOriginsJson).IsRequired();
        builder.Property(c => c.AllowedScopesJson).IsRequired();

        builder.Property(c => c.CreatedAt).IsRequired();
    }
}