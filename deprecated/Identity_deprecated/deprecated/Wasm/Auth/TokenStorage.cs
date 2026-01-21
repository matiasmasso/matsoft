using Microsoft.JSInterop;

namespace Wasm.Auth;

public interface ITokenStorage
{
    Task SaveTokensAsync(string accessToken, string refreshToken);
    Task<(string? accessToken, string? refreshToken)> GetTokensAsync();
    Task ClearAsync();
}

public class LocalStorageTokenStorage : ITokenStorage
{
    private readonly IJSRuntime _js;
    private const string AccessKey = "access_token";
    private const string RefreshKey = "refresh_token";

    public LocalStorageTokenStorage(IJSRuntime js) => _js = js;

    public async Task SaveTokensAsync(string accessToken, string refreshToken)
    {
        await _js.InvokeVoidAsync("localStorage.setItem", AccessKey, accessToken);
        await _js.InvokeVoidAsync("localStorage.setItem", RefreshKey, refreshToken);
    }

    public async Task<(string? accessToken, string? refreshToken)> GetTokensAsync()
    {
        var access = await _js.InvokeAsync<string?>("localStorage.getItem", AccessKey);
        var refresh = await _js.InvokeAsync<string?>("localStorage.getItem", RefreshKey);
        return (access, refresh);
    }

    public async Task ClearAsync()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem", AccessKey);
        await _js.InvokeVoidAsync("localStorage.removeItem", RefreshKey);
    }
}
