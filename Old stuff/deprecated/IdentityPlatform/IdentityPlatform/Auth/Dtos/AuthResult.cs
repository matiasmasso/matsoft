namespace IdentityPlatform.Auth.Dtos;

public record AuthResult(string AccessToken, string RefreshToken, DateTime ExpiresAt);