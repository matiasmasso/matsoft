using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class TokenBundle
    {
        public string? AccessToken { get; }
        public string? RefreshToken { get; }
        public DateTime ExpiresAtUtc { get; }

        public TokenBundle(string? accessToken, string? refreshToken, DateTime expiresAtUtc)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiresAtUtc = expiresAtUtc;
        }

        public bool HasTokens =>
            !string.IsNullOrWhiteSpace(AccessToken) &&
            !string.IsNullOrWhiteSpace(RefreshToken);

        public bool IsExpired() =>
            DateTime.UtcNow >= ExpiresAtUtc;
    }

}
