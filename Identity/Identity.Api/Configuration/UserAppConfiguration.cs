using Identity.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Api.Configuration;

public sealed class UserAppConfiguration : IEntityTypeConfiguration<UserApp>
{
    public void Configure(EntityTypeBuilder<UserApp> b)
    {
        b.ToTable("UserApps");

        b.HasKey(x => new { x.UserId, x.AppId });
    }
}