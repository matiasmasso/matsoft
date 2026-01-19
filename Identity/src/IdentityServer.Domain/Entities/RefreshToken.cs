using IdentityServer.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public Guid OdbcClientId { get; set; }

    public string Token { get; set; } = default!;
    public string JwtId { get; set; } = default!;

    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }

    public string? ReplacedByToken { get; set; }

    public User User { get; set; } = default!;
    public OdbcClient OdbcClient { get; set; } = default!;
}