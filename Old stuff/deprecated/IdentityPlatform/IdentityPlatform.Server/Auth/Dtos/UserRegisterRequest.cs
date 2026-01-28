namespace IdentityPlatform.Server.Auth.Dtos;

public record UserRegisterRequest(string Email, string Password, Guid AppId);