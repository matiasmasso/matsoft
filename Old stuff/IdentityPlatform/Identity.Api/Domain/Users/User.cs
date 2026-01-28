using Microsoft.AspNetCore.Identity;

namespace Identity.Api.Domain.Users;

public class User: IdentityUser
{
    public string Name { get; set; } = default!;
    public string? AvatarUrl { get; set; }

    public bool IsActive { get; set; } = true;
    public Dictionary<string, string>? Preferences { get; set; }

    public ICollection<UserAppEnrollment> AppEnrollments { get; set; } = new List<UserAppEnrollment>();
}