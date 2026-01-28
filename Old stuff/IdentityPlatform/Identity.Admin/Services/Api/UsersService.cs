using Identity.Admin.Http;
using Identity.Admin.Models.Users;

namespace Identity.Admin.Services.Api;

public class UsersService : BaseApiService
{
    public UsersService(SafeHttpClient http) : base(http) { }

    public Task<List<UserDto>?> GetAllAsync()
        => Http.GetAsync<List<UserDto>>("users");

    public Task<UserDto?> GetByIdAsync(Guid id)
        => Http.GetAsync<UserDto>($"users/{id}");

    public Task<UserDto?> CreateAsync(CreateUserRequest request)
        => Http.PostAsync<CreateUserRequest, UserDto>("users", request);

    public Task<UserDto?> UpdateAsync(Guid id, UpdateUserRequest request)
        => Http.PutAsync<UpdateUserRequest, UserDto>($"users/{id}", request);

    public Task<bool> DeleteAsync(Guid id)
        => Http.DeleteAsync($"users/{id}");

    public Task<bool> EnrollAsync(Guid userId, Guid appId)
        => Http.PostAsync($"users/{userId}/enroll/{appId}");

    public Task<bool> UnenrollAsync(Guid userId, Guid appId)
        => Http.DeleteAsync($"users/{userId}/enroll/{appId}");

    public Task<bool> ResetPasswordAsync(Guid userId, string newPassword)
    {
        var payload = new { NewPassword = newPassword };
        return Http.PostAsync<object, bool>($"users/{userId}/reset-password", payload);
    }
}