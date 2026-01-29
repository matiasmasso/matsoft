    using Identity.Contracts.Users;
namespace Identity.Admin.Services
{

    public interface IUsersService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> GetAsync(Guid id);
        Task CreateAsync(CreateUserRequest request);
        Task UpdateAsync(UpdateUserRequest request);
        Task DeleteAsync(Guid id);
        Task ToggleEnabledAsync(Guid id);
    }
}
