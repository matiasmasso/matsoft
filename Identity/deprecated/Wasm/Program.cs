using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Headers;
using Wasm;
using Wasm.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Base API URL (point to your Identity API)
var apiBase = new Uri("https://localhost:5001/"); // adjust

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = apiBase });

// Token storage + auth services
builder.Services.AddScoped<ITokenStorage, LocalStorageTokenStorage>();
builder.Services.AddScoped<AuthApiClient>();
builder.Services.AddScoped<TokenRefreshHandler>();

// HttpClient that automatically attaches JWT and refreshes
builder.Services.AddHttpClient("AuthorizedApi", client =>
{
    client.BaseAddress = apiBase;
}).AddHttpMessageHandler<TokenRefreshHandler>();

// Auth state
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthStateProvider>());

await builder.Build().RunAsync();
