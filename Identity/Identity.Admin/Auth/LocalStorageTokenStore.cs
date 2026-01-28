using Microsoft.JSInterop;

namespace Identity.Admin.Auth;

public class LocalStorageTokenStore : ITokenStore
{
    private readonly IJSRuntime _js;
    private const string Key = "jwt_token";

    public LocalStorageTokenStore(IJSRuntime js)
    {
        _js = js;
    }

    public async Task SaveTokenAsync(string token)
    {
        await _js.InvokeVoidAsync("localStorage.setItem", Key, token);
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _js.InvokeAsync<string?>("localStorage.getItem", Key);
    }

    public async Task ClearAsync()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem", Key);
    }
}