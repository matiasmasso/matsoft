using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using Wasm;
using Wasm.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");

// ---------------------------------------------
// Local storage
// ---------------------------------------------
builder.Services.AddBlazoredLocalStorage();

// ---------------------------------------------
// Auth
// ---------------------------------------------
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<JwtAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<JwtAuthStateProvider>());

// ---------------------------------------------
// API endpoint selector
// ---------------------------------------------
builder.Services.Configure<ApiEndpointOptions>(
    builder.Configuration.GetSection("ApiEndpoints"));
builder.Services.AddSingleton<ApiEndpointService>();

// ---------------------------------------------
// TokenStore
// ---------------------------------------------
builder.Services.AddScoped<TokenStore>();
builder.Services.AddScoped<ITokenStore>(sp => sp.GetRequiredService<TokenStore>());

// HttpMessageHandlers MUST be transient
builder.Services.AddTransient<TokenHttpHandler>();

// ---------------------------------------------
// NAMED HTTP CLIENTS
// ---------------------------------------------

// 1. UNAUTHENTICATED CLIENT (login, refresh, logout)
builder.Services.AddHttpClient("AuthApi", (sp, client) =>
{
    var opts = sp.GetRequiredService<IOptions<ApiEndpointOptions>>().Value;
    client.BaseAddress = new Uri(opts.RemoteApi);
});

// 2. AUTHENTICATED CLIENT (normal API calls)
builder.Services.AddHttpClient("RemoteApi", (sp, client) =>
{
    var opts = sp.GetRequiredService<IOptions<ApiEndpointOptions>>().Value;
    client.BaseAddress = new Uri(opts.RemoteApi);
})
.AddHttpMessageHandler<TokenHttpHandler>();

builder.Services.AddHttpClient("LocalApi", (sp, client) =>
{
    var opts = sp.GetRequiredService<IOptions<ApiEndpointOptions>>().Value;
    client.BaseAddress = new Uri(opts.LocalApi);
})
.AddHttpMessageHandler<TokenHttpHandler>();

// ---------------------------------------------
// SAFE HttpClient provider
// ---------------------------------------------
builder.Services.AddScoped<ApiHttpClient>();

// ---------------------------------------------
// Custom services
// ---------------------------------------------
builder.Services.AddScoped<AlbumsService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<HeicService>();
builder.Services.AddScoped<ContextMenuService>();


var host = builder.Build();

// ---------------------------------------------
// AUTO REFRESH ON APP START
// ---------------------------------------------
var auth = host.Services.GetRequiredService<AuthService>();
await auth.TryAutoRefreshAsync();

await host.RunAsync();