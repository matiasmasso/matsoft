namespace IdentityServer.Domain.Entities;

public class ExternalLogin
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public string Provider { get; set; } = default!;
    public string ProviderKey { get; set; } = default!;
    public string? EmailFromProvider { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }

    // Navigation
    public User User { get; set; } = default!;
}