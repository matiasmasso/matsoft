using System.Net.Http.Json;
using Identity.Api.Infrastructure.Errors;
using Identity.DTO;

namespace Identity.Admin.Services;

public class UsersApi : _BaseApiClient
{
    public UsersApi(HttpClient http) : base(http) { }


    // ------------------------------------------------------------
    // GET /users
    // ------------------------------------------------------------

    public Task<(List<UserDto>? Users, List<string>? Errors)> GetUsersAsync()
    {
        return ReadAsync<List<UserDto>>(() =>
            _http.GetAsync("users")
        );
    }

    // ------------------------------------------------------------
    // GET /users/{id}
    // ------------------------------------------------------------

    public Task<(UserDto? User, List<string>? Errors)> GetUserDetailsAsync(Guid userId)
    {
        return ReadAsync<UserDto?>(() =>
            _http.GetAsync($"users/{userId}")
        );
    }

    public async Task<UserDto?> GetUserDetailsAsyncByEmail(string email)
    {
        var response = await _http.GetAsync($"users/by-email/{email}");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<UserDto>();
    }

    // ------------------------------------------------------------
    // POST /users/create
    // ------------------------------------------------------------
    public Task<List<string>?> CreateUserAsync(CreateUserRequest request)
    {
        return CallAsync(() =>
            _http.PostAsJsonAsync("users/create", request)
        );
    }


    // ------------------------------------------------------------
    // PUT /users/{id}
    // ------------------------------------------------------------

    public Task<List<string>?> UpdateUserAsync(Guid id, UpdateUserRequest request)
    {
        return CallAsync(() =>
            _http.PutAsJsonAsync($"users/{id}", request)
        );
    }


    // ------------------------------------------------------------
    // DELETE /users/{id}
    // ------------------------------------------------------------
    public Task<List<string>?> DeleteUserAsync(Guid userId)
    {
        return CallAsync(() =>
            _http.DeleteAsync($"users/{userId}")
        );
    }

    // ------------------------------------------------------------
    // POST /users/activate
    // ------------------------------------------------------------
    public Task<List<string>?> ActivateUserAsync(Guid userId, bool isActive)
    {
        var request = new ActivateUserRequest
        {
            UserId = userId,
            IsActive = isActive
        };

        return CallAsync(() =>
            _http.PostAsJsonAsync("users/activate", request)
        );
    }

    // ------------------------------------------------------------
    // POST /users/enroll
    // ------------------------------------------------------------
    public Task<List<string>?> EnrollUserAsync(Guid userId, Guid appId)
    {
        var request = new EnrollUserRequest
        {
            UserId = userId,
            ApplicationId = appId
        };

        return CallAsync(() =>
            _http.PostAsJsonAsync("users/enroll", request)
        );
    }

    // ------------------------------------------------------------
    // POST /users/unenroll
    // ------------------------------------------------------------
    public Task<List<string>?> UnenrollUserAsync(Guid userId, Guid appId)
    {
        var request = new EnrollUserRequest
        {
            UserId = userId,
            ApplicationId = appId
        };

        return CallAsync(() =>
            _http.PostAsJsonAsync("users/unenroll", request)
        );
    }

    // ------------------------------------------------------------
    // GET /users/{id}/roles/{appId}
    // ------------------------------------------------------------
    public Task<(List<Guid>?, List<string>? Errors)> GetUserRolesAsync(Guid userId, Guid appId)
    {
        return ReadAsync<List<Guid>>(() =>
            _http.GetAsync($"users/{userId}/roles/{appId}")
        );
    }

    // ------------------------------------------------------------
    // POST /users/roles/assign
    // ------------------------------------------------------------
    public Task<List<string>?> AssignRoleAsync(Guid userId, Guid roleId, Guid appId)
    {
        var request = new AssignRoleRequest
        {
            UserId = userId,
            RoleId = roleId,
            ApplicationId = appId
        };

        return CallAsync(() =>
            _http.PostAsJsonAsync("users/roles/assign", request)
        );
    }

    // ------------------------------------------------------------
    // POST /users/roles/remove
    // ------------------------------------------------------------
    public Task<List<string>?> RemoveRoleAsync(Guid userId, Guid roleId, Guid appId)
    {
        var request = new AssignRoleRequest
        {
            UserId = userId,
            RoleId = roleId,
            ApplicationId = appId
        };

        return CallAsync(() =>
            _http.PostAsJsonAsync("users/roles/remove", request)
        );
    }

    // ------------------------------------------------------------
    // POST /users/reset-password
    // ------------------------------------------------------------
    public Task<List<string>?> ResetPasswordAsync(Guid userId, string newPassword)
    {
        var request = new AdminResetPasswordRequest
        {
            UserId = userId,
            NewPassword = newPassword
        };

        return CallAsync(() =>
            _http.PostAsJsonAsync("users/reset-password", request)
        );
    }

    public Task<(HashSet<Guid>? Values, List<string>? Errors)> GetUserApplicationsAsync(Guid userId)
    {
        return ReadAsync<HashSet<Guid>?>(() =>
            _http.GetAsync($"users/{userId}/applications")
        );
    }
}

