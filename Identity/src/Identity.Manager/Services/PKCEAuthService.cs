using Identity.Manager.Config;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Identity.Manager.Services;

public class PKCEAuthService
{
    private readonly HttpClient _http;
    private readonly NavigationManager _nav;
    private readonly SessionStorageService _storage;
    private readonly CustomAuthStateProvider _authState;

    private const string ClientId = "identity-manager";
    private const string IdentityServerBaseUrl = "https://localhost:7105";
    //private const string IdentityServerBaseUrl = "https://localhost:5000";
    private const string RedirectUri = "https://localhost:7273/auth/callback";
    //private const string RedirectUri = "https://localhost:5001/auth/callback";
    private const string Scope = "openid profile email roles";

    private const string VerifierKey = "pkce_verifier";
    private const string AccessTokenKey = "access_token";
    private const string RefreshTokenKey = "refresh_token";

    public PKCEAuthService(
        HttpClient http,
        NavigationManager nav,
        SessionStorageService storage,
        CustomAuthStateProvider authState)
    {
        _http = http;
        _nav = nav;
        _storage = storage;
        _authState = authState;
    }

    public async Task StartLoginAsync()
    {
        var (verifier, challenge) = GeneratePkce();

        await _storage.SetAsync(VerifierKey, verifier);

        var query = new Dictionary<string, string?>
        {
            ["client_id"] = ClientId,
            ["redirect_uri"] = RedirectUri,
            ["response_type"] = "code",
            ["scope"] = Scope,
            ["code_challenge"] = challenge,
            ["code_challenge_method"] = "S256"
        };

        var url = $"{IdentityServerBaseUrl}/connect/authorize?{ToQueryString(query)}";
        _nav.NavigateTo(url, forceLoad: true);
    }

    public async Task HandleCallbackAsync(string code)
    {
        var verifier = await _storage.GetAsync(VerifierKey);
        if (string.IsNullOrWhiteSpace(verifier))
            throw new InvalidOperationException("Missing PKCE verifier.");

        var body = new Dictionary<string, string?>
        {
            ["grant_type"] = "authorization_code",
            ["client_id"] = ClientId,
            ["redirect_uri"] = RedirectUri,
            ["code"] = code,
            ["code_verifier"] = verifier
        };

        var response = await _http.PostAsync(
            $"{IdentityServerBaseUrl}/connect/token",
            new FormUrlEncodedContent(body!)
        );

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var accessToken = root.GetProperty("access_token").GetString()!;
        var refreshToken = root.GetProperty("refresh_token").GetString()!;

        await _storage.SetAsync(AccessTokenKey, accessToken);
        await _storage.SetAsync(RefreshTokenKey, refreshToken);

        await _authState.NotifyUserAuthenticationAsync(accessToken);
    }

    public async Task LogoutAsync()
    {
        await _storage.RemoveAsync(AccessTokenKey);
        await _storage.RemoveAsync(RefreshTokenKey);
        await _storage.RemoveAsync(VerifierKey);

        await _authState.NotifyUserLogoutAsync();

        _nav.NavigateTo("/", forceLoad: true);
    }

    private static (string verifier, string challenge) GeneratePkce()
    {
        var bytes = RandomNumberGenerator.GetBytes(32);
        var verifier = Base64UrlEncode(bytes);

        using var sha = SHA256.Create();
        var challengeBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(verifier));
        var challenge = Base64UrlEncode(challengeBytes);

        return (verifier, challenge);
    }

    private static string Base64UrlEncode(byte[] bytes) =>
        Convert.ToBase64String(bytes)
            .Replace("+", "-")
            .Replace("/", "_")
            .TrimEnd('=');

    private static string ToQueryString(Dictionary<string, string?> parameters) =>
        string.Join("&", parameters
            .Where(kv => kv.Value is not null)
            .Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value!)}"));

    public async Task<string?> GetAccessTokenAsync()
    {
        return await _storage.GetAsync("access_token");
    }
}