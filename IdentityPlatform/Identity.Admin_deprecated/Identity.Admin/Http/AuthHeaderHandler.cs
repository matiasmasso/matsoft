using System.Net.Http.Headers;
using Identity.Admin.Auth;

namespace Identity.Admin.Http;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly ITokenProvider _accessTokens;
    private readonly IRefreshTokenProvider _refreshTokens;
    private readonly TokenRefreshService _refreshService;

    public AuthHeaderHandler(
        ITokenProvider accessTokens,
        IRefreshTokenProvider refreshTokens,
        TokenRefreshService refreshService)
    {
        _accessTokens = accessTokens;
        _refreshTokens = refreshTokens;
        _refreshService = refreshService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // 1. Add access token
        var token = await _accessTokens.GetAccessTokenAsync();
        if (!string.IsNullOrWhiteSpace(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // 2. Send request
        var response = await base.SendAsync(request, cancellationToken);

        // 3. If unauthorized, try refresh
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var refreshToken = await _refreshTokens.GetRefreshTokenAsync();
            if (string.IsNullOrWhiteSpace(refreshToken))
                return response;

            var newToken = await _refreshService.RefreshAsync(refreshToken);
            if (string.IsNullOrWhiteSpace(newToken))
                return response;

            // 4. Retry request with new token
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
            return await base.SendAsync(request, cancellationToken);
        }

        return response;
    }
}