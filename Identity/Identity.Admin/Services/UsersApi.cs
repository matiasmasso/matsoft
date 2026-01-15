using System.Net.Http.Json;
using Identity.DTO;

namespace Identity.Admin.Services;

public class UsersApi
{
    private readonly HttpClient _http;

    public UsersApi(HttpClient http)
    {
        _http = http;
    }

    // ------------------------------------------------------------
    // GET /users
    // ------------------------------------------------------------
    public async Task<List<UserDto>> GetUsersAsync()
    {
        return await _http.GetFromJsonAsync<List<UserDto>>("users")
               ?? new List<UserDto>();
    }

    // ------------------------------------------------------------
    // GET /users/{id}
    // ------------------------------------------------------------
    public async Task<UserDetailsDto> GetUserDetailsAsync(Guid userId)
    {
        return await _http.GetFromJsonAsync<UserDetailsDto>($"users/{userId}")
               ?? new UserDetailsDto();
    }

    // ------------------------------------------------------------
    // POST /users/create
    // ------------------------------------------------------------
    public async Task<UserDto> CreateUserAsync(CreateUserRequest request)
    {
        var response = await _http.PostAsJsonAsync("users/create", request);
        response.EnsureSuccessStatusCode();

        // Controller returns: { message, id }
        var result = await response.Content.ReadFromJsonAsync<CreateUserResponse>();
        return new UserDto
        {
            Id = result!.Id,
            Email = request.Email,
            UserName = request.UserName,
            IsActive = true
        };
    }

    // ------------------------------------------------------------
    // DELETE /users/{id}
    // ------------------------------------------------------------
    public async Task DeleteUserAsync(Guid userId)
    {
        var response = await _http.DeleteAsync($"users/{userId}");
        response.EnsureSuccessStatusCode();
    }

    // ------------------------------------------------------------
    // POST /users/activate
    // ------------------------------------------------------------
    public async Task ActivateUserAsync(Guid userId, bool isActive)
    {
        var request = new ActivateUserRequest
        {
            UserId = userId,
            IsActive = isActive
        };

        var response = await _http.PostAsJsonAsync("users/activate", request);
        response.EnsureSuccessStatusCode();
    }

    // ------------------------------------------------------------
    // POST /users/enroll
    // ------------------------------------------------------------
    public async Task EnrollUserAsync(Guid userId, Guid appId)
    {
        var request = new EnrollUserRequest
        {
            UserId = userId,
            ApplicationId = appId
        };

        var response = await _http.PostAsJsonAsync("users/enroll", request);
        response.EnsureSuccessStatusCode();
    }

    // ------------------------------------------------------------
    // POST /users/unenroll
    // ------------------------------------------------------------
    public async Task UnenrollUserAsync(Guid userId, Guid appId)
    {
        var request = new EnrollUserRequest
        {
            UserId = userId,
            ApplicationId = appId
        };

        var response = await _http.PostAsJsonAsync("users/unenroll", request);
        response.EnsureSuccessStatusCode();
    }

    // ------------------------------------------------------------
    // GET /users/{id}/roles/{appId}
    // ------------------------------------------------------------
    public async Task<List<Guid>> GetUserRolesAsync(Guid userId, Guid appId)
    {
        return await _http.GetFromJsonAsync<List<Guid>>(
                   $"users/{userId}/roles/{appId}"
               ) ?? new List<Guid>();
    }

    // ------------------------------------------------------------
    // POST /users/roles/assign
    // ------------------------------------------------------------
    public async Task AssignRoleAsync(Guid userId, Guid roleId, Guid appId)
    {
        var request = new AssignRoleRequest
        {
            UserId = userId,
            RoleId = roleId,
            ApplicationId = appId
        };

        var response = await _http.PostAsJsonAsync("users/roles/assign", request);
        response.EnsureSuccessStatusCode();
    }

    // ------------------------------------------------------------
    // POST /users/roles/remove
    // ------------------------------------------------------------
    public async Task RemoveRoleAsync(Guid userId, Guid roleId, Guid appId)
    {
        var request = new AssignRoleRequest
        {
            UserId = userId,
            RoleId = roleId,
            ApplicationId = appId
        };

        var response = await _http.PostAsJsonAsync("users/roles/remove", request);
        response.EnsureSuccessStatusCode();
    }

    // ------------------------------------------------------------
    // POST /users/reset-password
    // ------------------------------------------------------------
    public async Task ResetPasswordAsync(Guid userId, string newPassword)
    {
        var request = new AdminResetPasswordRequest
        {
            UserId = userId,
            NewPassword = newPassword
        };

        var response = await _http.PostAsJsonAsync("users/reset-password", request);
        response.EnsureSuccessStatusCode();
    }
}

public class CreateUserResponse
{
    public Guid Id { get; set; }
    public string Message { get; set; } = "";
}