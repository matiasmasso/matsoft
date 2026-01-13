using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data.Seed
{
    public static class IdentitySeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var albumsAppId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var tatitaAppId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            var adminRoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var viewerRoleId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var managerRoleId = Guid.Parse("33333333-3333-3333-3333-333333333333");

            modelBuilder.Entity<App>().HasData(
                new App { AppId = albumsAppId, Name = "Albums", Description = "Albums WASM App" },
                new App { AppId = tatitaAppId, Name = "Tatita", Description = "Tatita WASM App" }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = adminRoleId, Name = "Admin" },
                new Role { RoleId = viewerRoleId, Name = "Viewer" },
                new Role { RoleId = managerRoleId, Name = "Manager" }
            );

            modelBuilder.Entity<AppRole>().HasData(
                new AppRole { AppRoleId = Guid.NewGuid(), AppId = albumsAppId, RoleId = adminRoleId },
                new AppRole { AppRoleId = Guid.NewGuid(), AppId = albumsAppId, RoleId = viewerRoleId },
                new AppRole { AppRoleId = Guid.NewGuid(), AppId = tatitaAppId, RoleId = managerRoleId }
            );
        }
    }
}
