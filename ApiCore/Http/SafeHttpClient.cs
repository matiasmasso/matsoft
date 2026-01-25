using System.Net.Http.Json;
using ApiCore.Exceptions;
using ApiCore.Notifications;

namespace ApiCore.Http;

public class SafeHttpClient
{
    private readonly HttpClient _http;
    private readonly INotificationService _notify;

    public SafeHttpClient(HttpClient http, INotificationService notify)
    {
        _http = http;
        _notify = notify;
    }

    // -------------------------
    // GET
    // -------------------------
    public async Task<T?> GetAsync<T>(string url)
    {
        try
        {
            var response = await _http.GetAsync(url);
            return await response.ReadOrThrowAsync<T>();
        }
        catch (ApiException ex)
        {
            Notify(ex);
            return default;
        }
    }

    // -------------------------
    // POST
    // -------------------------
    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest payload)
    {
        try
        {
            var response = await _http.PostAsJsonAsync(url, payload);
            return await response.ReadOrThrowAsync<TResponse>();
        }
        catch (ApiException ex)
        {
            Notify(ex);
            return default;
        }
    }

    // -------------------------
    // PUT
    // -------------------------
    public async Task<TResponse?> PutAsync<TRequest, TResponse>(string url, TRequest payload)
    {
        try
        {
            var response = await _http.PutAsJsonAsync(url, payload);
            return await response.ReadOrThrowAsync<TResponse>();
        }
        catch (ApiException ex)
        {
            Notify(ex);
            return default;
        }
    }

    // -------------------------
    // DELETE
    // -------------------------
    public async Task<bool> DeleteAsync(string url)
    {
        try
        {
            var response = await _http.DeleteAsync(url);
            await response.ThrowIfErrorAsync();
            return true;
        }
        catch (ApiException ex)
        {
            Notify(ex);
            return false;
        }
    }

    // -------------------------
    // NOTIFICATION ROUTING
    // -------------------------
    private void Notify(ApiException ex)
    {
        if (ex.StatusCode >= 500)
            _notify.ShowError(ex.Message);
        else if (ex.StatusCode == 404)
            _notify.ShowWarning(ex.Message);
        else
            _notify.ShowInfo(ex.Message);
    }
}