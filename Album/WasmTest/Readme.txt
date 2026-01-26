//---------------------------------------------------------------------------
1.- wwwroot/appsettings.json
//---------------------------------------------------------------------------
{
  "Identity": {
    "Authority": "https://localhost:7001",
    "ClientId": "album-client",
    "PostLogoutRedirectUri": "/",
    "RedirectUri": "/authentication/login-callback"
  },
  "AlbumApi": {
    "BaseUrl": "https://localhost:7223"
  }
}

//---------------------------------------------------------------------------
2.- Nuget
//---------------------------------------------------------------------------
cd C:\Users\matia\source\repos\...
dotnet add package Microsoft.AspNetCore.Components.WebAssembly.Authentication
dotnet add package Microsoft.Extensions.Http
dotnet add package Microsoft.Extensions.Http.Polly
dotnet add package System.Net.Http.Json

//---------------------------------------------------------------------------
3.- Folders & files
//---------------------------------------------------------------------------
mkdir Authentication
mkdir Http
mkdir Services
mkdir Models
mkdir -p Pages
mkdir -p Pages\Albums
mkdir -p Pages\Account
mkdir Shared
mkdir Utils

ni Authentication\IdentityApiAuthorizationMessageHandler.cs
ni Authentication\TokenProvider.cs
ni Http\ApiClient.cs
ni Http\SafeHttpClient.cs
ni Http\HttpExtensions.cs
ni Services\AlbumService.cs
ni Services\UserProfileService.cs
ni Models\AlbumDto.cs
ni Models\PhotoDto.cs
ni Models\UserProfileDto.cs
ni Pages\Index.razor
ni Pages\Albums\AlbumsPage.razor
ni Pages\Albums\AlbumDetailPage.razor
ni Pages\Account\Login.razor
ni Pages\Account\Logout.razor
ni Pages\Account\Profile.razor
ni Shared\MainLayout.razor
ni Shared\NavMenu.razor
ni Shared\Loading.razor
ni Utils\TypedLocalStorage.cs

Scaffold Utils/TypedLocalStorage
Scaffold Authentication/IdentityApiAuthorizationMessageHandler
Scaffold Program.cs
Scaffold App.razor

MainLayout with login/logout links


// ------------------------------------------------------------------------------
TypedLocalStorage.cs
// ------------------------------------------------------------------------------

using System.Text.Json;
using Microsoft.JSInterop;

namespace Album.Client.Utils;

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

// ------------------------------------------------------------------------------
IdentityApiAuthorizationMessageHandler.cs
// ------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net;
using System.Net.Http.Headers;

namespace WasmTest.Authentication;

public sealed class IdentityApiAuthorizationMessageHandler : DelegatingHandler
{
    private readonly IAccessTokenProvider _tokenProvider;
    private readonly NavigationManager _nav;

    public List<string> AuthorizedUrls { get; } = new();

    public IdentityApiAuthorizationMessageHandler(
        IAccessTokenProvider tokenProvider,
        NavigationManager nav)
    {
        _tokenProvider = tokenProvider;
        _nav = nav;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (AuthorizedUrls.Any(url =>
            request.RequestUri!.AbsoluteUri.StartsWith(url, StringComparison.OrdinalIgnoreCase)))
        {
            var tokenResult = await _tokenProvider.RequestAccessToken();

            if (tokenResult.TryGetToken(out var token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token.Value);
            }
            else
            {
                _nav.NavigateTo("authentication/login");
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
//---------------------------------------------------------------------------
Program.cs
//---------------------------------------------------------------------------

using WasmTest;
using WasmTest.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 1. Read API base URL from appsettings.json
var albumApiBase = builder.Configuration["AlbumApi:BaseUrl"]
    ?? throw new InvalidOperationException("AlbumApi:BaseUrl is missing");

// 2. Register the custom authorization handler (DI-configured)
builder.Services.AddScoped<IdentityApiAuthorizationMessageHandler>(sp =>
{
    var handler = new IdentityApiAuthorizationMessageHandler(
        sp.GetRequiredService<IAccessTokenProvider>(),
        sp.GetRequiredService<NavigationManager>());

    handler.AuthorizedUrls.Add(albumApiBase);
    return handler;
});

// 3. Register the HttpClient that uses the handler
builder.Services.AddHttpClient("Album.Api", client =>
{
    client.BaseAddress = new Uri(albumApiBase);
})
.AddHttpMessageHandler<IdentityApiAuthorizationMessageHandler>();

// 4. Default HttpClient (no auth)
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("Album.Api"));

// 5. Add OIDC authentication
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Identity", options.ProviderOptions);
});

await builder.Build().RunAsync();
