using System.Net.Http.Json;

namespace Identity.Admin.Auth;

public class TokenRefreshService
{
    private readonly HttpClient _http;

    public TokenRefreshService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string?> RefreshAsync(string refreshToken)
    {
        var response = await _http.PostAsJsonAsync("auth/refresh", new { refreshToken });

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<RefreshResponse>();
        return result?.AccessToken;
    }

    private class RefreshResponse
    {
        public string? AccessToken { get; set; }
    }
}