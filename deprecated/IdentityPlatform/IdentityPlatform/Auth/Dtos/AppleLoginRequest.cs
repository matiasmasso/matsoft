namespace IdentityPlatform.Auth.Dtos;

public record AppleLoginRequest(string IdToken, Guid AppId);