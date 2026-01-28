namespace IdentityPlatform.Server.Auth.Dtos;

public record UserRefreshRequest(string RefreshToken, Guid AppId);