namespace IdentityPlatform.Client.Dtos;

public record UserRegisterRequest(string Email, string Password, Guid AppId);