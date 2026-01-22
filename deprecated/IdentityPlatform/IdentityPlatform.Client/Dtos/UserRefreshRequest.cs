namespace IdentityPlatform.Client.Dtos;

public record UserRefreshRequest(string RefreshToken, Guid AppId);