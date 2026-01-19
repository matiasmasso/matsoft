using System.Net.Http.Json;
using System.Text.Json;
using Wasm.Auth;

namespace Wasm.Auth;

public class AuthApiClient
{
    private readonly HttpClient _http;
    private readonly ITokenStorage _tokens;

    public AuthApiClient(HttpClient http, ITokenStorage tokens)
    {
        _http = http;
        _tokens = tokens;
    }

    public record RegisterRequest(string Email, string Password);
    public record RegisterResponse(Guid UserId, string Email);

    public record LoginRequest(string Email, string Password);
    public record LoginResponse(string AccessToken, string RefreshToken);

    public record RefreshTokenRequest(string RefreshToken);
    public record ForgotPasswordRequest(string Email, string ClientUrl);
    public record ResetPasswordRequest(string Email, string Token, string NewPassword);

    public async Task<RegisterResponse?> RegisterAsync(string email, string password)
    {
        var res = await _http.PostAsJsonAsync("api/auth/register", new RegisterRequest(email, password));
        res.EnsureSuccessStatusCode();
        var json = await res.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<RegisterResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task LoginAsync(string email, string password)
    {
        var res = await _http.PostAsJsonAsync("api/auth/login", new LoginRequest(email, password));
        if (!res.IsSuccessStatusCode)
            throw new Exception("Invalid credentials");

        var json = await res.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<LoginResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        await _tokens.SaveTokensAsync(data.AccessToken, data.RefreshToken);
    }

    public async Task<(string accessToken, string refreshToken)> RefreshAsync(string refreshToken)
    {
        var res = await _http.PostAsJsonAsync("api/auth/refresh", new RefreshTokenRequest(refreshToken));
        if (!res.IsSuccessStatusCode)
            throw new Exception("Refresh failed");

        var json = await res.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<LoginResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        await _tokens.SaveTokensAsync(data.AccessToken, data.RefreshToken);
        return (data.AccessToken, data.RefreshToken);
    }

    public async Task LogoutAsync()
    {
        var (_, refresh) = await _tokens.GetTokensAsync();
        if (!string.IsNullOrEmpty(refresh))
        {
            await _http.PostAsJsonAsync("api/auth/logout", new RefreshTokenRequest(refresh));
        }
        await _tokens.ClearAsync();
    }

    public async Task ForgotPasswordAsync(string email, string clientUrl)
    {
        await _http.PostAsJsonAsync("api/auth/forgot-password", new ForgotPasswordRequest(email, clientUrl));
        // Controller always returns OK even if user doesn't exist
    }

    public async Task ResetPasswordAsync(string email, string token, string newPassword)
    {
        var res = await _http.PostAsJsonAsync("api/auth/reset-password", new ResetPasswordRequest(email, token, newPassword));
        if (!res.IsSuccessStatusCode)
            throw new Exception("Reset failed");
    }
}
