namespace IdentityServer.Domain.Entities;

public class AuthorizationCode
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public string ClientId { get; set; } = default!;
    public string RedirectUri { get; set; } = default!;
    public string CodeChallenge { get; set; } = default!;
    public string CodeChallengeMethod { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
}
