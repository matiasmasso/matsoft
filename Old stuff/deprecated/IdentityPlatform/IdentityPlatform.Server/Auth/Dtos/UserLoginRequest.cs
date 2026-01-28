namespace IdentityPlatform.Server.Auth.Dtos;

public record UserLoginRequest(string Email, string Password, Guid AppId);