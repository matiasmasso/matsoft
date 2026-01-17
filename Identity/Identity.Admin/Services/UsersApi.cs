using System.Net.Http.Json;
using Identity.Api.Infrastructure.Errors;
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

    public async Task<(List<UserDto>? Users, List<string> Errors)> GetUsersAsync()
    {
        try
        {
            var response = await _http.GetAsync("users");

            if (!response.IsSuccessStatusCode)
            {
                var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                return (null, payload?.Errors ?? new List<string> { "Unknown error" });
            }

            var users = await response.Content.ReadFromJsonAsync<List<UserDto>>();
            return (users ?? new List<UserDto>(), new List<string>());
        }
        catch (Exception ex)
        {
            return (null, new List<string> { ex.Message });
        }
    }

    // ------------------------------------------------------------
    // GET /users/{id}
    // ------------------------------------------------------------

    public async Task<(UserDto? User, List<string> Errors)> GetUserDetailsAsync(Guid userId)
    {
        try
        {
            var response = await _http.GetAsync($"users/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                return (null, payload?.Errors ?? new List<string> { "Unknown error" });
            }

            var user = await response.Content.ReadFromJsonAsync<UserDto>();
            return (user, new List<string>());
        }
        catch (Exception ex)
        {
            return (null, new List<string> { ex.Message });
        }
    }

    // ------------------------------------------------------------
    // POST /users/create
    // ------------------------------------------------------------
    public async Task<(UserDto? User, List<string> Errors)> CreateUserAsync(CreateUserRequest request)
    {
        var response = await _http.PostAsJsonAsync("users/create", request);

        if (!response.IsSuccessStatusCode)
        {
            var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (null, payload?.Errors ?? new List<string> { "Unknown error" });
        }

        var result = await response.Content.ReadFromJsonAsync<CreateUserResponse>();

        var user = new UserDto
        {
            Id = result!.Id,
            Email = request.Email,
            UserName = request.UserName,
            IsActive = true
        };

        return (user, new());
    }


    // ------------------------------------------------------------
    // DELETE /users/{id}
    // ------------------------------------------------------------
    public async Task<List<string>> DeleteUserAsync(Guid userId)
    {
        var response = await _http.DeleteAsync($"users/{userId}");
        if (!response.IsSuccessStatusCode)
        {
            var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (payload?.Errors ?? new List<string> { "Unknown error" });
        }
        return new();
    }

    // ------------------------------------------------------------
    // POST /users/activate
    // ------------------------------------------------------------
    public async Task<List<string>> ActivateUserAsync(Guid userId, bool isActive)
    {
        var request = new ActivateUserRequest
        {
            UserId = userId,
            IsActive = isActive
        };

        var response = await _http.PostAsJsonAsync("users/activate", request);
        if (!response.IsSuccessStatusCode)
        {
            var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (payload?.Errors ?? new List<string> { "Unknown error" });
        }
        return new();
    }

    // ------------------------------------------------------------
    // POST /users/enroll
    // ------------------------------------------------------------
    public async Task<List<string>> EnrollUserAsync(Guid userId, Guid appId)
    {
        var request = new EnrollUserRequest
        {
            UserId = userId,
            ApplicationId = appId
        };

        var response = await _http.PostAsJsonAsync("users/enroll", request);
        if (!response.IsSuccessStatusCode)
        {
            var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (payload?.Errors ?? new List<string> { "Unknown error" });
        }
        return new();
    }

    // ------------------------------------------------------------
    // POST /users/unenroll
    // ------------------------------------------------------------
    public async Task<List<string>> UnenrollUserAsync(Guid userId, Guid appId)
    {
        var request = new EnrollUserRequest
        {
            UserId = userId,
            ApplicationId = appId
        };

        var response = await _http.PostAsJsonAsync("users/unenroll", request);
        if (!response.IsSuccessStatusCode)
        {
            var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (payload?.Errors ?? new List<string> { "Unknown error" });
        }
        return new();
    }

    // ------------------------------------------------------------
    // GET /users/{id}/roles/{appId}
    // ------------------------------------------------------------
    public async Task<(List<Guid>?, List<string> Errors)> GetUserRolesAsync(Guid userId, Guid appId)
    {
        var response = await _http.GetAsync($"users/{userId}/roles/{appId}");
        if (!response.IsSuccessStatusCode)
        {
            var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (null, payload?.Errors ?? new List<string> { "Unknown error" });
        }

        var result = await response.Content.ReadFromJsonAsync<List<Guid>>();
        return (result, new());
    }

    // ------------------------------------------------------------
    // POST /users/roles/assign
    // ------------------------------------------------------------
    public async Task<List<string>> AssignRoleAsync(Guid userId, Guid roleId, Guid appId)
    {
        var request = new AssignRoleRequest
        {
            UserId = userId,
            RoleId = roleId,
            ApplicationId = appId
        };

        var response = await _http.PostAsJsonAsync("users/roles/assign", request);
        if (!response.IsSuccessStatusCode)
        {
            var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (payload?.Errors ?? new List<string> { "Unknown error" });
        }
        return new();
    }

    // ------------------------------------------------------------
    // POST /users/roles/remove
    // ------------------------------------------------------------
    public async Task<List<string>> RemoveRoleAsync(Guid userId, Guid roleId, Guid appId)
    {
        var request = new AssignRoleRequest
        {
            UserId = userId,
            RoleId = roleId,
            ApplicationId = appId
        };

        var response = await _http.PostAsJsonAsync("users/roles/remove", request);
        if (!response.IsSuccessStatusCode)
        {
            var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (payload?.Errors ?? new List<string> { "Unknown error" });
        }
        return new();
    }

    // ------------------------------------------------------------
    // POST /users/reset-password
    // ------------------------------------------------------------
    public async Task<List<string>> ResetPasswordAsync(Guid userId, string newPassword)
    {
        var request = new AdminResetPasswordRequest
        {
            UserId = userId,
            NewPassword = newPassword
        };

        var response = await _http.PostAsJsonAsync("users/reset-password", request);
        if (!response.IsSuccessStatusCode)
        {
            var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (payload?.Errors ?? new List<string> { "Unknown error" });
        }
        return new();
    }

    public async Task<(HashSet<Guid>? Values, List<string> Errors)> GetUserApplicationsAsync(Guid userId)
    {
            var response = await _http.GetAsync($"users/{userId}/applications");

            if (!response.IsSuccessStatusCode)
            {
                var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                return (null, payload?.Errors ?? new List<string> { "Unknown error" });
            }

            var list = await response.Content.ReadFromJsonAsync<List<Guid>>();
            return (list?.ToHashSet() ?? new HashSet<Guid>(), new List<string>());
        
    }
}

public class CreateUserResponse
{
    public Guid Id { get; set; }
    public string Message { get; set; } = "";
}