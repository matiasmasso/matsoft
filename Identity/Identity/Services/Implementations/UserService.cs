using Identity.Data;
using Identity.Domain.Entities;
using Identity.Services;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly IdentityDbContext _db;

    public UserService(IdentityDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Exists(string email)
        => await _db.Users.AnyAsync(u => u.Email == email);

    public async Task<User> Create(string email, string passwordHash)
    {
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Email = email,
            PasswordHash = passwordHash
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<List<User>> GetAll()
        => await _db.Users.OrderBy(u => u.Email).ToListAsync();

    public async Task<User?> GetById(Guid id)
        => await _db.Users.FirstOrDefaultAsync(u => u.UserId == id);

    public async Task<User?> GetByEmail(string email)
        => await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task Update(User user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var user = await GetById(id);
        if (user == null) return;

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }

    public async Task<List<string>> GetRoles(Guid userId)
    {
        return await _db.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.Role.Name)
            .ToListAsync();
    }

    public async Task<List<string>> GetApps(Guid userId)
    {
        return await _db.UserApps
            .Where(ua => ua.UserId == userId)
            .Select(ua => ua.App.Name)
            .ToListAsync();
    }
}
