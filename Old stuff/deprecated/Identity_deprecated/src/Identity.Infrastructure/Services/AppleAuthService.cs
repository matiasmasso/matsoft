using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Identity.Infrastructure.Services;

public class AppleAuthService
{
    private readonly IConfiguration _config;
    private readonly UserManager<ApplicationUser> _users;

    public AppleAuthService(IConfiguration config, UserManager<ApplicationUser> users)
    {
        _config = config;
        _users = users;
    }

    public string BuildAppleLoginUrl(string redirectUri)
    {
        var clientId = _config["Apple:ClientId"];
        var state = Guid.NewGuid().ToString("N");

        return $"https://appleid.apple.com/auth/authorize" +
               $"?response_type=code" +
               $"&client_id={clientId}" +
               $"&redirect_uri={Uri.EscapeDataString(redirectUri)}" +
               $"&scope=name%20email" +
               $"&state={state}";
    }

    public async Task<ApplicationUser> HandleAppleCallbackAsync(string code)
    {
        var token = await ExchangeCodeForAppleTokenAsync(code);
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token.IdToken);

        var email = jwt.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        var appleId = jwt.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

        if (appleId == null)
            throw new Exception("Invalid Apple token");

        var user = await _users.FindByLoginAsync("Apple", appleId);

        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = email ?? appleId,
                Email = email
            };

            await _users.CreateAsync(user);
            await _users.AddLoginAsync(user, new UserLoginInfo("Apple", appleId, "Apple"));
        }

        return user;
    }

    private async Task<(string IdToken, string AccessToken)> ExchangeCodeForAppleTokenAsync(string code)
    {
        var clientId = _config["Apple:ClientId"];
        var teamId = _config["Apple:TeamId"];
        var keyId = _config["Apple:KeyId"];
        var privateKey = _config["Apple:PrivateKey"];

        var clientSecret = AppleJwtGenerator.GenerateClientSecret(teamId, clientId, keyId, privateKey);

        using var http = new HttpClient();

        var request = new HttpRequestMessage(HttpMethod.Post, "https://appleid.apple.com/auth/token");
        request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["client_id"] = clientId!,
            ["client_secret"] = clientSecret,
            ["code"] = code,
            ["grant_type"] = "authorization_code"
        });

        var response = await http.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();

        var doc = JsonDocument.Parse(json);
        var idToken = doc.RootElement.GetProperty("id_token").GetString()!;
        var accessToken = doc.RootElement.GetProperty("access_token").GetString()!;

        return (idToken, accessToken);
    }
}