using Identity.Client.Http;
using Identity.Contracts.Users;

namespace Identity.Admin.Services;

public interface IUsersService
{
    Task<Result<List<UserDto>>> GetAllAsync();
    Task<Result<UserDto>> GetAsync(Guid id);
    Task<Result<UserDto>> CreateAsync(CreateUserRequest request);
    Task<Result<UserDto>> UpdateAsync(UpdateUserRequest request);
    Task<Result<bool>> DeleteAsync(Guid id);
    Task<Result<bool>> ToggleEnabledAsync(Guid id);
}
