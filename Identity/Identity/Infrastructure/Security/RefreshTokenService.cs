using Identity.Data;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Identity.Infrastructure.Security
{
    public class RefreshTokenService
    {
        private readonly IdentityDbContext _db;

        public RefreshTokenService(IdentityDbContext db)
        {
            _db = db;
        }

        public async Task<RefreshToken> CreateRefreshToken(Guid userId)
        {
            var token = new RefreshToken
            {
                TokenId = Guid.NewGuid(),
                UserId = userId,
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            _db.RefreshTokens.Add(token);
            await _db.SaveChangesAsync();

            return token;
        }

        public async Task<RefreshToken?> GetValidToken(string token)
        {
            return await _db.RefreshTokens
                .Where(t => t.Token == token && t.RevokedAt == null && t.ExpiresAt > DateTime.UtcNow)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateToken(RefreshToken token)
        {
            _db.RefreshTokens.Update(token);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateExpiration(Guid tokenId, DateTime newExpiration)
        {
            var token = await _db.RefreshTokens.FindAsync(tokenId);
            if (token == null)
                return;

            token.ExpiresAt = newExpiration;
            await _db.SaveChangesAsync();
        }

        public async Task RevokeToken(Guid tokenId)
        {
            var token = await _db.RefreshTokens.FindAsync(tokenId);
            if (token != null)
            {
                token.RevokedAt = DateTime.UtcNow;
                await _db.SaveChangesAsync();
            }
        }
    }

}
