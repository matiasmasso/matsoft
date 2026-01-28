namespace IdentityPlatform.Server.Auth.Dtos;

public record AuthResult(string AccessToken, string RefreshToken, DateTime ExpiresAt);