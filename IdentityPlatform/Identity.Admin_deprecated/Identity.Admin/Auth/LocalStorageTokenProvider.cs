namespace Identity.Admin.Auth;

public class LocalStorageTokenProvider : ITokenProvider
{
    private readonly JsTokenAccessor _jsAccessor;

    public LocalStorageTokenProvider(JsTokenAccessor jsAccessor)
    {
        _jsAccessor = jsAccessor;
    }

    public Task<string?> GetAccessTokenAsync()
        => _jsAccessor.GetTokenAsync("access_token");
}