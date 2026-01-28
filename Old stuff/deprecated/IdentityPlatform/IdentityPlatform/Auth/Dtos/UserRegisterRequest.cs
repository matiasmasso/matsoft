namespace IdentityPlatform.Auth.Dtos;

public record UserRegisterRequest(string Email, string Password, Guid AppId);