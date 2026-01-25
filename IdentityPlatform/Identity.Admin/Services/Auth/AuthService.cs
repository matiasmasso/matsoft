using System.Net.Http.Json;
using Identity.Admin.Models.Auth;

namespace Identity.Admin.Services.Auth;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly TokenStorage _tokens;

    public AuthService(HttpClient http, TokenStorage tokens)
    {
        _http = http;
        _tokens = tokens;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        var response = await _http.PostAsJsonAsync("auth/login",
            new LoginRequest(username, password));

        if (!response.IsSuccessStatusCode)
            return false;

        var data = await response.Content.ReadFromJsonAsync<LoginResponse>();

        await _tokens.SaveTokensAsync(data!.AccessToken, data.RefreshToken);
        return true;
    }
}