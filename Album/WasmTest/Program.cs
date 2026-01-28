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


// 2. Add OIDC authentication (always before IdentityApiAuthorizationMessageHandler) 
builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = "https://localhost:5001";
    options.ProviderOptions.ClientId = "mat-album-wasm";
    options.ProviderOptions.ResponseType = "code";

    // Clear default scopes to avoid duplicates
    options.ProviderOptions.DefaultScopes.Clear();
    options.ProviderOptions.DefaultScopes.Add("openid");
    options.ProviderOptions.DefaultScopes.Add("profile");
    options.ProviderOptions.DefaultScopes.Add("email");
});


// 3. Register the custom authorization handler (DI-configured) (always after Oidc)
builder.Services.AddScoped<IdentityApiAuthorizationMessageHandler>(sp =>
{
    var handler = new IdentityApiAuthorizationMessageHandler(
        sp.GetRequiredService<IAccessTokenProvider>(),
        sp.GetRequiredService<NavigationManager>());

    handler.AuthorizedUrls.Add(albumApiBase);
    return handler;
});

// 4. Register the HttpClient that uses the handler
builder.Services.AddHttpClient("Album.Api", client =>
{
    client.BaseAddress = new Uri(albumApiBase);
})
.AddHttpMessageHandler<IdentityApiAuthorizationMessageHandler>();

// 5. Default HttpClient (no auth)
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("Album.Api"));

await builder.Build().RunAsync();
