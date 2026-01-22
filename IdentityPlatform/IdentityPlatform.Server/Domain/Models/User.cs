namespace IdentityPlatform.Server.Domain.Models;

public class User
{
    public Guid Id { get; set; }

    // Link to IdentityUser (ApplicationUser)
    public string IdentityUserId { get; set; } = "";

    public string DisplayName { get; set; } = "";
    public string Email { get; set; } = "";

    // Navigation
    public List<UserAppEnrollment> Enrollments { get; set; } = new();
}