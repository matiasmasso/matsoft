using System.Net;
using System.Net.Http.Json;

namespace Identity.Admin.Services.Auth;

public class RefreshTokenHandler : DelegatingHandler
{
    private readonly TokenStorage _tokens;
    private readonly IHttpClientFactory _factory;

    public RefreshTokenHandler(TokenStorage tokens, IHttpClientFactory factory)
    {
        _tokens = tokens;
        _factory = factory;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // First attempt
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode != HttpStatusCode.Unauthorized)
            return response;

        // Try refresh
        var refreshed = await TryRefreshAsync();

        if (!refreshed)
            return response; // Let SafeHttpClient handle the error

        // Retry original request with new token
        var clone = await CloneRequestAsync(request);
        return await base.SendAsync(clone, cancellationToken);
    }

    private async Task<bool> TryRefreshAsync()
    {
        var refreshToken = await _tokens.GetRefreshTokenAsync();
        if (string.IsNullOrWhiteSpace(refreshToken))
            return false;

        var client = _factory.CreateClient("Api");

        var result = await client.PostAsJsonAsync("auth/refresh", new
        {
            refreshToken
        });

        if (!result.IsSuccessStatusCode)
            return false;

        var payload = await result.Content.ReadFromJsonAsync<TokenResponse>();
        if (payload is null)
            return false;

        await _tokens.SaveTokensAsync(payload.AccessToken, payload.RefreshToken);
        return true;
    }

    private static async Task<HttpRequestMessage> CloneRequestAsync(HttpRequestMessage request)
    {
        var clone = new HttpRequestMessage(request.Method, request.RequestUri);

        // Copy content
        if (request.Content != null)
        {
            var body = await request.Content.ReadAsStringAsync();
            clone.Content = new StringContent(body, System.Text.Encoding.UTF8, request.Content.Headers.ContentType?.MediaType);
        }

        // Copy headers
        foreach (var header in request.Headers)
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

        return clone;
    }

    private record TokenResponse(string AccessToken, string RefreshToken);
}