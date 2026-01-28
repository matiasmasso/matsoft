namespace IdentityPlatform.Server.Auth.Dtos;

public record GoogleLoginRequest(string IdToken, Guid AppId);