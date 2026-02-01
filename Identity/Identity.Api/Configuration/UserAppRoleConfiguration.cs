using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class UserAppRoleConfiguration : IEntityTypeConfiguration<UserAppRole>
{
    public void Configure(EntityTypeBuilder<UserAppRole> b)
    {
        //b.ToTable("UserAppRoles");

        b.HasKey(x => new { x.UserId, x.AppRoleId });

        b.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.AppRole)
            .WithMany(r => r.UserAppRoles)
            .HasForeignKey(x => x.AppRoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}