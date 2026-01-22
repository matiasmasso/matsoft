using System.Net.Http.Headers;
using System.Net.Http.Json;
using IdentityPlatform.Client.Dtos;

namespace IdentityPlatform.Client.Auth.Services;

public class RefreshTokenHandler : DelegatingHandler
{
    private readonly ITokenStorage _tokens;
    private readonly IHttpClientFactory _factory;

    public RefreshTokenHandler(ITokenStorage tokens, IHttpClientFactory factory)
    {
        _tokens = tokens;
        _factory = factory;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
    {
        var accessToken = await _tokens.GetAccessTokenAsync();
        if (!string.IsNullOrWhiteSpace(accessToken))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await base.SendAsync(request, ct);

        if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            return response;

        // Try refresh
        var refreshToken = await _tokens.GetRefreshTokenAsync();
        if (string.IsNullOrWhiteSpace(refreshToken))
            return response;

        var client = _factory.CreateClient("auth");
        var refreshResult = await client.PostAsJsonAsync("auth/refresh",
            new UserRefreshRequest(refreshToken, Guid.Parse("00000000-0000-0000-0000-000000000001")), ct);

        if (!refreshResult.IsSuccessStatusCode)
            return response;

        var auth = await refreshResult.Content.ReadFromJsonAsync<AuthResult>(cancellationToken: ct);
        if (auth is null)
            return response;

        await _tokens.SaveTokensAsync(auth.AccessToken, auth.RefreshToken);

        // Retry original request
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", auth.AccessToken);
        return await base.SendAsync(request, ct);
    }
}