using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure.Services;

public static class AppleJwtGenerator
{
    public static string GenerateClientSecret(
        string teamId,
        string clientId,
        string keyId,
        string privateKeyP8)
    {
        // Load EC private key from .p8 content
        using var ecdsa = ECDsa.Create();
        ecdsa.ImportFromPem(privateKeyP8.AsSpan());

        var securityKey = new ECDsaSecurityKey(ecdsa)
        {
            KeyId = keyId
        };

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.EcdsaSha256);

        var now = DateTimeOffset.UtcNow;

        var token = new JwtSecurityToken(
            issuer: teamId,                         // iss
            audience: "https://appleid.apple.com",  // aud
            claims: new[]
            {
                new Claim("sub", clientId)          // sub
            },
            notBefore: now.UtcDateTime,
            expires: now.AddMinutes(5).UtcDateTime, // Apple recommends short-lived
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}