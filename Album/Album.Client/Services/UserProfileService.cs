using Album.Client.Models;
using System.Net.Http.Json;

namespace Album.Client.Services;

public class UserProfileService
{
    private readonly HttpClient _http;

    public UserProfileService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("Identity.Api");
    }

    public Task<UserProfileDto?> GetAsync()
        => _http.GetFromJsonAsync<UserProfileDto>("me");

    public async Task<bool> UpdateAsync(UserProfileUpdateDto dto)
    {
        var response = await _http.PutAsJsonAsync("me", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UploadAvatarAsync(Stream fileStream, string fileName)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new StreamContent(fileStream), "file", fileName);

        var response = await _http.PostAsync("me/avatar", content);
        return response.IsSuccessStatusCode;
    }
}