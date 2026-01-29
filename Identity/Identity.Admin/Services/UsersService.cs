using Identity.Admin.Services;
using Identity.Contracts.Users;

public sealed class UsersService(HttpClient http) : IUsersService
{
    public Task<List<UserDto>> GetAllAsync()
        => http.SafeGetAsync<List<UserDto>>("users");

    public Task<UserDto> GetAsync(Guid id)
        => http.SafeGetAsync<UserDto>($"users/{id}");

    public Task CreateAsync(CreateUserRequest request)
        => http.SafePostAsync("users", request);

    public Task UpdateAsync(UpdateUserRequest request)
        => http.SafePutAsync($"users/{request.Id}", request);

    public Task DeleteAsync(Guid id)
        => http.SafeDeleteAsync($"users/{id}");

    public Task ToggleEnabledAsync(Guid id)
        => http.SafePostAsync($"users/{id}/toggle-enabled", new { });
}