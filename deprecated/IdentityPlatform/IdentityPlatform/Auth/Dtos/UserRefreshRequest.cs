namespace IdentityPlatform.Auth.Dtos;

public record UserRefreshRequest(string RefreshToken, Guid AppId);