using System.Net;
using System.Net.Http.Headers;
using Wasm.Auth;

namespace Wasm.Auth;

public class TokenRefreshHandler : DelegatingHandler
{
    private readonly ITokenStorage _tokens;
    private readonly AuthApiClient _authApi;
    private readonly HttpClient _bareClient;

    public TokenRefreshHandler(ITokenStorage tokens, AuthApiClient authApi, IHttpClientFactory factory)
    {
        _tokens = tokens;
        _authApi = authApi;
        _bareClient = factory.CreateClient(); // base address set in Program
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var (access, refresh) = await _tokens.GetTokensAsync();

        if (!string.IsNullOrEmpty(access))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", access);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized && !string.IsNullOrEmpty(refresh))
        {
            // Try refresh
            try
            {
                var (newAccess, newRefresh) = await _authApi.RefreshAsync(refresh);

                // Retry once with new access token
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newAccess);
                response.Dispose();
                response = await base.SendAsync(request, cancellationToken);
            }
            catch
            {
                await _tokens.ClearAsync();
            }
        }

        return response;
    }
}
