namespace IdentityPlatform.Auth.Dtos;

public record UserLoginRequest(string Email, string Password, Guid AppId);