namespace IdentityPlatform.Server.Auth.Dtos;

public record AppleLoginRequest(string IdToken, Guid AppId);