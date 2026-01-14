using Identity.Domain.Entities;

public class RefreshToken
{
    public Guid RefreshTokenId { get; set; }   // Primary key
    public Guid UserId { get; set; }
    public string Token { get; set; } = "";
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool Revoked { get; set; }

    public DateTime? RevokedAt { get; set; }

    public User User { get; set; } = default!;
}
