using Identity.Api.Entities;
using Microsoft.AspNetCore.Identity;

public sealed class User : IdentityUser<Guid>
{
    public string DisplayName { get; set; } = default!;
    public bool Enabled { get; set; }

    public List<UserApp> UserApps { get; set; } = new();
}
