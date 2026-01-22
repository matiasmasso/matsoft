namespace IdentityPlatform.Server.Domain.Models;

public class ClientApp
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";
    public string Description { get; set; } = "";

    // Optional: OAuth2-style client credentials
    public string ClientId { get; set; } = "";
    public string ClientSecretHash { get; set; } = "";

    // Navigation
    public List<UserAppEnrollment> Enrollments { get; set; } = new();
}