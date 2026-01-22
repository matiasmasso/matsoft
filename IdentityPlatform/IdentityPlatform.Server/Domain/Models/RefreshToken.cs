namespace IdentityPlatform.Server.Domain.Models;

public class RefreshToken
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public Guid ClientAppId { get; set; }

    public string Token { get; set; } = "";
    public DateTime ExpiresAt { get; set; }
    public bool Revoked { get; set; }

    // Navigation
    public User? User { get; set; }
    public ClientApp? ClientApp { get; set; }
}