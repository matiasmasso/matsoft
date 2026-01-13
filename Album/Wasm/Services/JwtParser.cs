using System.Security.Claims;
using System.Text.Json;

namespace Wasm.Services
{
    public static class JwtParser
    {
        private static readonly HashSet<string> _ignored = new()
        {
            "exp", "nbf", "iat" // numeric date fields
        };

        public static IEnumerable<Claim> ParseClaims(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);

            using var doc = JsonDocument.Parse(jsonBytes);
            var root = doc.RootElement;

            var claims = new List<Claim>();

            foreach (var property in root.EnumerateObject())
            {
                if (_ignored.Contains(property.Name))
                    continue;

                AddClaims(claims, property.Name, property.Value);
            }

            return claims;
        }

        private static void AddClaims(List<Claim> claims, string key, JsonElement value)
        {
            switch (value.ValueKind)
            {
                case JsonValueKind.Array:
                    foreach (var item in value.EnumerateArray())
                        AddClaims(claims, key, item);
                    break;

                case JsonValueKind.Object:
                    foreach (var prop in value.EnumerateObject())
                        AddClaims(claims, $"{key}:{prop.Name}", prop.Value);
                    break;

                case JsonValueKind.String:
                    claims.Add(new Claim(key, value.GetString()!));
                    break;

                case JsonValueKind.Number:
                    claims.Add(new Claim(key, value.GetRawText()));
                    break;

                case JsonValueKind.True:
                case JsonValueKind.False:
                    claims.Add(new Claim(key, value.GetBoolean().ToString()));
                    break;

                default:
                    // null, undefined, etc.
                    break;
            }
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}