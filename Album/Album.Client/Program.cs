using Album.Client;
using Album.Client.Http;
using Album.Client.Services;
using Album.Client.Utils;
using Identity.Admin.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// -------------------------------
// Configuration
// -------------------------------
var identityAuthority = builder.Configuration["Identity:Authority"]
    ?? "https://localhost:7001";

var albumApiBase = builder.Configuration["AlbumApi:BaseUrl"]
    ?? "https://localhost:7223";

// -------------------------------
// Authentication (OIDC + PKCE)
// -------------------------------
//builder.Services.AddOidcAuthentication(options =>
//{
//    builder.Configuration.Bind("Identity", options.ProviderOptions);

//    options.ProviderOptions.Authority = identityAuthority;
//    options.ProviderOptions.ClientId = "album-client";
//    options.ProviderOptions.ResponseType = "code";

//    options.ProviderOptions.DefaultScopes.Add("openid");
//    options.ProviderOptions.DefaultScopes.Add("profile");
//    options.ProviderOptions.DefaultScopes.Add("email");
//});

// -------------------------------
// Local Storage (custom)
// -------------------------------
builder.Services.AddScoped<TypedLocalStorage>();

// -------------------------------
// Authorization Handler
// -------------------------------
builder.Services.AddScoped<IdentityApiAuthorizationMessageHandler>();

// -------------------------------
// HttpClient for Album.Api
// -------------------------------
builder.Services.AddHttpClient("Album.Api", client =>
{
    client.BaseAddress = new Uri(albumApiBase);
})
.AddHttpMessageHandler<IdentityApiAuthorizationMessageHandler>();

builder.Services.AddScoped(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("Album.Api");
});

// -------------------------------
// SafeHttpClient + ApiClient
// -------------------------------
builder.Services.AddScoped<INotificationService, ToastNotificationService>();
builder.Services.AddScoped<SafeHttpClient>();
builder.Services.AddScoped<ApiClient>();

// -------------------------------
// Services
// -------------------------------
//builder.Services.AddScoped<AlbumService>();
builder.Services.AddScoped<ToastService>();
builder.Services.AddScoped<UserProfileService>();
builder.Services.AddScoped<ProfileCacheService>();
builder.Services.AddScoped<UserSettingsService>();

await builder.Build().RunAsync();