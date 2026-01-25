using Microsoft.JSInterop;

namespace Identity.Admin.Auth;

public class JsTokenAccessor
{
    private readonly IJSRuntime _js;
    private IJSObjectReference? _module;

    public JsTokenAccessor(IJSRuntime js)
    {
        _js = js;
    }

    private async Task<IJSObjectReference> GetModuleAsync()
    {
        if (_module is null)
            _module = await _js.InvokeAsync<IJSObjectReference>("import", "./js/tokenStorage.js");

        return _module;
    }

    public async Task<string?> GetTokenAsync(string key)
    {
        var module = await GetModuleAsync();
        return await module.InvokeAsync<string?>("getToken", key);
    }
}