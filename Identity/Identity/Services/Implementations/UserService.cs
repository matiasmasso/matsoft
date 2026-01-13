using Identity.Data;
using Identity.Domain.Entities;
using Identity.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

namespace Identity.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IdentityDbContext _db;
        private readonly PasswordHasher _hasher;

        public UserService(IdentityDbContext db, PasswordHasher hasher)
        {
            _db = db;
            _hasher = hasher;
        }

        public Task<bool> Exists(string email) =>
            _db.Users.AnyAsync(u => u.Email == email);

        public async Task<User> Create(string email, string password)
        {
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Email = email,
                PasswordHash = _hasher.Hash(password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetById(Guid userId)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }


        public Task<User?> GetByEmail(string email) =>
            _db.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<IEnumerable<string>> GetRoles(Guid userId)
        {
            return await _db.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.AppRole.Role.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetApps(Guid userId)
        {
            return await _db.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.AppRole.App.Name)
                .Distinct()
                .ToListAsync();
        }

        public async Task Update(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }

    }

}
