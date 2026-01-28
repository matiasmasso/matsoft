namespace IdentityPlatform.Server.Auth.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string? GoogleId { get; set; }
    public string? AppleId { get; set; }

    public List<UserAppEnrollment> Enrollments { get; set; } = new();
}