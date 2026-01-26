using System.Text.Json;
using Microsoft.JSInterop;

namespace WasmTest.Utils;

public class TypedLocalStorage
{
    private readonly IJSRuntime _js;

    public TypedLocalStorage(IJSRuntime js) => _js = js;

    public async ValueTask SetAsync<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);
        await _js.InvokeVoidAsync("localStorage.setItem", key, json);
    }

    public async ValueTask<T?> GetAsync<T>(string key)
    {
        var json = await _js.InvokeAsync<string?>("localStorage.getItem", key);
        return json is null ? default : JsonSerializer.Deserialize<T>(json);
    }

    public ValueTask RemoveAsync(string key) =>
        _js.InvokeVoidAsync("localStorage.removeItem", key);
}
