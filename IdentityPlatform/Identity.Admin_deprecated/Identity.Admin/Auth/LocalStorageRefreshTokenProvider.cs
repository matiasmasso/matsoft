using Microsoft.JSInterop;

namespace Identity.Admin.Auth;

public class LocalStorageRefreshTokenProvider : IRefreshTokenProvider
{
    private readonly JsTokenAccessor _js;

    public LocalStorageRefreshTokenProvider(JsTokenAccessor js)
    {
        _js = js;
    }

    public Task<string?> GetRefreshTokenAsync()
        => _js.GetTokenAsync("refresh_token");
}