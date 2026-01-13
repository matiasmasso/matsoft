using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Identity.Client.Services
{


    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        private const string AccessTokenKey = "accessToken";
        private const string RefreshTokenKey = "refreshToken";

        public AuthService(
            HttpClient http,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        // ------------------------------------------------------------
        // LOGIN
        // ------------------------------------------------------------
        public async Task<bool> Login(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", new
            {
                Email = email,
                Password = password
            });

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            await SaveTokens(result.AccessToken, result.RefreshToken);

            NotifyAuthStateChanged();

            return true;
        }

        // ------------------------------------------------------------
        // LOGOUT
        // ------------------------------------------------------------
        public async Task Logout()
        {
            var refreshToken = await _localStorage.GetItemAsync<string>(RefreshTokenKey);

            if (refreshToken != null)
            {
                await _http.PostAsJsonAsync("api/auth/logout", new { RefreshToken = refreshToken });
            }

            await ClearTokens();
            NotifyAuthStateChanged();
        }

        // ------------------------------------------------------------
        // REFRESH TOKEN
        // ------------------------------------------------------------
        public async Task<bool> TryRefreshToken()
        {
            var refreshToken = await _localStorage.GetItemAsync<string>(RefreshTokenKey);
            if (string.IsNullOrWhiteSpace(refreshToken))
                return false;

            var response = await _http.PostAsJsonAsync("api/auth/refresh", new
            {
                RefreshToken = refreshToken
            });

            if (!response.IsSuccessStatusCode)
            {
                await Logout();
                return false;
            }

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            await SaveTokens(result.AccessToken, result.RefreshToken);

            NotifyAuthStateChanged();
            return true;
        }

        // ------------------------------------------------------------
        // TOKEN STORAGE
        // ------------------------------------------------------------
        private async Task SaveTokens(string accessToken, string refreshToken)
        {
            await _localStorage.SetItemAsync(AccessTokenKey, accessToken);
            await _localStorage.SetItemAsync(RefreshTokenKey, refreshToken);

            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
        }

        private async Task ClearTokens()
        {
            await _localStorage.RemoveItemAsync(AccessTokenKey);
            await _localStorage.RemoveItemAsync(RefreshTokenKey);

            _http.DefaultRequestHeaders.Authorization = null;
        }

        // ------------------------------------------------------------
        // AUTH STATE NOTIFICATION
        // ------------------------------------------------------------
        private async void NotifyAuthStateChanged()
        {
            if (_authStateProvider is ApiAuthenticationStateProvider api)
            {
                var token = await _localStorage.GetItemAsync<string>("accessToken");
                api.NotifyUserAuthentication(token);
            }
        }


        // ------------------------------------------------------------
        // RESPONSE DTO
        // ------------------------------------------------------------
        private class LoginResponse
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
        }
    }
}
