namespace IdentityPlatform.Client.Dtos;

public record AuthResult(string AccessToken, string RefreshToken, DateTime ExpiresAt);