using Identity.Domain.Entities;

public interface IUserService
{
    Task<bool> Exists(string email);
    Task<User?> GetById(Guid id);
    Task<User?> GetByEmail(string email);
    Task<List<User>> GetAll();
    Task<User> Create(string email, string passwordHash);
    Task Update(User user);
    Task Delete(Guid id);

    Task<List<string>> GetRoles(Guid userId);
    Task<List<string>> GetApps(Guid userId);
}
