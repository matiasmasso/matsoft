namespace IdentityPlatform.Auth.Dtos;

public record GoogleLoginRequest(string IdToken, Guid AppId);