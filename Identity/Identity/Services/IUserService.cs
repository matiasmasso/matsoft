using Identity.Domain.Entities;

namespace Identity.Services
{
    public interface IUserService
    {
        Task<bool> Exists(string email);
        Task<User> Create(string email, string password);
        Task<User?> GetByEmail(string email);
        Task<User?> GetById(Guid userId);   // ← ADD THIS
        Task<IEnumerable<string>> GetRoles(Guid userId);
        Task<IEnumerable<string>> GetApps(Guid userId);

        Task Update(User user);
    }


}
