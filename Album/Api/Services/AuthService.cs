using Api.Data;
using Api.Entities;
using DTO;
using DTO.Helpers;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Services
{
    public class AuthService
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _db;
        private readonly UsersService _usersService;

        public AuthService(AppDbContext db, IConfiguration config, UsersService usersService)
        {
            _db = db;
            _config = config;
            _usersService = usersService;
        }


        public TokenResponse? Login(string email, string password)
        {
            var user = FindUserFromEmailAddress(email);
            if (user == null)
                return null;

            var computed = CryptoHelper.ComputePasswordHash(password, user.PasswordSalt!);

            if (!CryptographicOperations.FixedTimeEquals(computed, user.PasswordHash))
                return null;

            return GenerateTokens(user);
        }

        public async Task LogoutAsync(RefreshRequest request)
        {
            var stored = await _db.RefreshTokens
                .FirstOrDefaultAsync(r => r.Token == request.RefreshToken);

            if (stored != null)
            {

                var tokens = _db.RefreshTokens
                    .Where(r => r.UserId == stored.UserId && r.RevokedAt == null);
                foreach (var t in tokens)
                    t.RevokedAt = DateTime.UtcNow;

                await _db.SaveChangesAsync();
            }
        }

        public UserModel? FindUserFromEmailAddress(string emailAddress)
        {
            return _db.UserAccounts
                .Where(x => x.EmailAddress == emailAddress)
                .Select(x => new UserModel(x.Guid)
                {
                    EmailAddress = x.EmailAddress,
                    Nickname = x.Nickname,
                    Rol = (UserModel.Rols)x.Rol,
                    PasswordHash = x.PasswordHash,
                    PasswordSalt = x.PasswordSalt
                }).FirstOrDefault();
        }


        public TokenResponse TokenRefreshRequest(RefreshRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                // return BadRequest("Missing refresh token.");
                throw new Exception("Missing refresh token.");

            var stored = _db.RefreshTokens
                .FirstOrDefault(r => r.Token == request.RefreshToken);

            if (stored == null || DateTime.UtcNow >= stored.ExpiresAt || stored.RevokedAt != null)
                //return Unauthorized("Invalid refresh token.");
                throw new UnauthorizedAccessException("Invalid refresh token.");

            // Token rotation
            stored.RevokedAt = DateTime.UtcNow;
            _db.SaveChanges();

            var user = _usersService.GetValue(stored.UserId);

            if (user == null)
                throw new UnauthorizedAccessException("User no longer exists.");

            var tokens = GenerateTokens(user);
            return tokens;
        }

        public TokenResponse GenerateTokens(UserModel user)
        {
            var accessToken = GenerateJwt(user);

            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var refreshExpires = DateTime.UtcNow.AddDays(30);

            var entity = new RefreshToken
            {
                Id=Guid.NewGuid(),
                Token = refreshToken,
                UserId = user.Guid,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = refreshExpires
            };

            _db.RefreshTokens.Add(entity);
            _db.SaveChanges();

            return new TokenResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30)
            };
        }

        private string GenerateJwt(UserModel user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Guid.ToString()),
            new Claim(ClaimTypes.Email, user.EmailAddress!),
            new Claim(ClaimTypes.Role, user.Rol.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void CreateFirstUser(LoginRequest request)
        {

            CreatePasswordHash(request.Password!, out var hash, out var salt);
            var user = new UserModel()
            {
                EmailAddress = request.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                Rol = UserModel.Rols.Admin

            };

            var usr = new UserAccount
            {
                Guid = user.Guid,
                EmailAddress = user.EmailAddress,
                Rol = (int)UserModel.Rols.Admin,
                PasswordSalt = user.PasswordSalt,
                PasswordHash = user.PasswordHash
            };

            _db.UserAccounts.Add(usr);
            _db.SaveChanges();



        }

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }




    }
}


