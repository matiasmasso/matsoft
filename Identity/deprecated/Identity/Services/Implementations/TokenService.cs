namespace Identity.Services.Implementations
{
    using Identity.Configuration;
    using Identity.Data;
    using Identity.Domain.Entities;
    using Identity.DTO;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;

    public class TokenService : ITokenService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtOptions _options;
        private readonly JwtSecurityTokenHandler _tokenHandler = new();

        public TokenService(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            IOptions<JwtOptions> options)
        {
            _db = db;
            _userManager = userManager;
            _options = options.Value;
        }

        public async Task<TokenResponse> GenerateTokensAsync(ApplicationUser user, Guid applicationId)
        {
            var now = DateTime.UtcNow;

            // 1. Build claims
            var claims = await BuildClaimsAsync(user, applicationId);

            // 2. Create access token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var accessTokenExpires = now.AddMinutes(_options.AccessTokenMinutes);

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: accessTokenExpires,
                signingCredentials: creds);

            var accessToken = _tokenHandler.WriteToken(jwt);

            // 3. Create refresh token (per app)
            var refreshToken = GenerateSecureToken();
            var refreshTokenExpires = now.AddDays(_options.RefreshTokenDays);

            var refreshEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                ApplicationId = applicationId,
                Token = refreshToken,
                JwtId = jwt.Id,
                CreatedAt = now,
                ExpiresAt = refreshTokenExpires,
                IsRevoked = false,
                IsUsed = false
            };

            _db.RefreshTokens.Add(refreshEntity);
            await _db.SaveChangesAsync();

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiresAt = accessTokenExpires,
                RefreshTokenExpiresAt = refreshTokenExpires
            };
        }

        public async Task<TokenResponse> RefreshAsync(RefreshRequest request)
        {
            var principal = GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null)
                throw new SecurityTokenException("Invalid access token");

            var userIdString = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
                throw new SecurityTokenException("Invalid user id");

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null || !user.IsActive)
                throw new SecurityTokenException("User not found or inactive");

            var storedToken = await _db.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken);

            if (storedToken == null)
                throw new SecurityTokenException("Refresh token not found");

            if (storedToken.IsUsed)
                throw new SecurityTokenException("Refresh token already used");

            if (storedToken.IsRevoked)
                throw new SecurityTokenException("Refresh token revoked");

            if (storedToken.ExpiresAt < DateTime.UtcNow)
                throw new SecurityTokenException("Refresh token expired");

            if (storedToken.ApplicationId != request.ApplicationId)
                throw new SecurityTokenException("Refresh token does not belong to this application");

            // Optional: validate jti from access token vs storedToken.JwtId

            // Mark old token as used
            storedToken.IsUsed = true;
            storedToken.RevokedAt = DateTime.UtcNow;

            // Issue new tokens (rotation)
            var newTokens = await GenerateTokensAsync(user, request.ApplicationId);

            // Link rotation chain
            storedToken.ReplacedByToken = newTokens.RefreshToken;

            await _db.SaveChangesAsync();

            return newTokens;
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            var storedToken = await _db.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            if (storedToken == null)
                return;

            storedToken.IsRevoked = true;
            storedToken.RevokedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
        }

        private async Task<List<Claim>> BuildClaimsAsync(ApplicationUser user, Guid applicationId)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
        new Claim("app_id", applicationId.ToString())
    };

            // Load roles for this specific application
            var roles = await _db.UserRoles
                .Where(ur => ur.UserId == user.Id && ur.ApplicationId == applicationId)
                .Include(ur => ur.Role)
                .Select(ur => ur.Role.Name)
                .ToListAsync();

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            return claims; // <-- ALWAYS return
        }


        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _options.Issuer,
                ValidateAudience = true,
                ValidAudience = _options.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = false // we allow expired tokens here
            };

            try
            {
                var principal = _tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                if (securityToken is not JwtSecurityToken jwt ||
                    !jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private static string GenerateSecureToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

}
