using Microsoft.JSInterop;

namespace Identity.Manager.Services;

public class SessionStorageService
{
    private readonly IJSRuntime _js;

    public SessionStorageService(IJSRuntime js)
    {
        _js = js;
    }

    public ValueTask SetAsync(string key, string value) =>
        _js.InvokeVoidAsync("sessionStorage.setItem", key, value);

    public ValueTask<string?> GetAsync(string key) =>
        _js.InvokeAsync<string?>("sessionStorage.getItem", key);

    public ValueTask RemoveAsync(string key) =>
        _js.InvokeVoidAsync("sessionStorage.removeItem", key);
}