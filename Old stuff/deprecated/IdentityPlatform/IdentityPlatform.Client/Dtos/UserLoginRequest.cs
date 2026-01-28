namespace IdentityPlatform.Client.Dtos;

public record UserLoginRequest(string Email, string Password, Guid AppId);