namespace Identity.Api.Models;

public class RefreshToken
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = default!;

    public string ClientId { get; set; } = string.Empty; // app/client
    public string Token { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }
    public bool Revoked { get; set; }
}