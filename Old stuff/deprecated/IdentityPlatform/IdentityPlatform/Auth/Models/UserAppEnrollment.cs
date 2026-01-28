namespace IdentityPlatform.Auth.Models;

public class UserAppEnrollment
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public Guid AppId { get; set; }
    public ClientApp App { get; set; } = default!;

    public Guid RoleId { get; set; }
    public Role Role { get; set; } = default!;

    public bool IsActive { get; set; } = true;
}