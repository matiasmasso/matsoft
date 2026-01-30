using Identity.Admin.Services;
using Identity.Client.Http;
using Identity.Client.Services;
using Identity.Contracts.Users;


public sealed class UsersService
    : CrudServiceBase<UserDto, UserDto, CreateUserRequest, UpdateUserRequest>, IUsersService
{
    public UsersService(SafeHttp http)
        : base(http, "users")
    {
    }

    protected override Guid GetId(UpdateUserRequest request) => request.Id;

    public Task<Result<bool>> ToggleEnabledAsync(Guid id)
        => _http.Post<bool>($"users/{id}/toggle-enabled", new { }, "User status updated");
}

