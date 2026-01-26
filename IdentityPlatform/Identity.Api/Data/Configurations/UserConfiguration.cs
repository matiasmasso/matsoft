using Identity.Api.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Identity.Api.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> b)
    {
        b.HasKey(u => u.Id);

        b.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(200);

        b.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(200);

        b.Property(u => u.AvatarUrl)
            .HasMaxLength(500);

        // JSON dictionary mapping
        b.Property(u => u.Preferences)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => v == null
                    ? null
                    : JsonSerializer.Deserialize<Dictionary<string, string>>(v)
            );
    }
}