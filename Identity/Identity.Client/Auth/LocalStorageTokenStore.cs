using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Identity.Client.Auth;

public class LocalStorageTokenStore : ITokenStore
{
    private readonly IJSRuntime _js;
    private const string Key = "jwt_token";

    public LocalStorageTokenStore(IJSRuntime js)
    {
        _js = js;
    }

    public Task SetToken(string token)
        => _js.InvokeVoidAsync("localStorage.setItem", Key, token).AsTask();

    public async Task<string?> GetToken()
        => await _js.InvokeAsync<string?>("localStorage.getItem", Key);

    public Task ClearToken()
        => _js.InvokeVoidAsync("localStorage.removeItem", Key).AsTask();
}