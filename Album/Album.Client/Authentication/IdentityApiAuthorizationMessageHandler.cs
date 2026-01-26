using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net;
using System.Net.Http.Headers;

public sealed class IdentityApiAuthorizationMessageHandler : DelegatingHandler
{
    private readonly IAccessTokenProvider _tokenProvider;
    private readonly NavigationManager _nav;
    public List<string> AuthorizedUrls { get; } = new();

    public IdentityApiAuthorizationMessageHandler(
        IAccessTokenProvider tokenProvider,
        NavigationManager nav)
    {
        _tokenProvider = tokenProvider;
        _nav = nav;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (AuthorizedUrls.Any(url =>
            request.RequestUri!.AbsoluteUri.StartsWith(url, StringComparison.OrdinalIgnoreCase)))
        {
            var tokenResult = await _tokenProvider.RequestAccessToken();

            if (tokenResult.TryGetToken(out var token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token.Value);
            }
            else
            {
                _nav.NavigateTo("authentication/login");
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}