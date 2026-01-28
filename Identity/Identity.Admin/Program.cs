using Identity.Admin;
using Identity.Admin.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 1. Authorization system
builder.Services.AddAuthorizationCore();

// 2. Token store (LocalStorage)
builder.Services.AddScoped<ITokenStore, LocalStorageTokenStore>();

// 3. Authentication state provider (reads token, notifies UI)
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();

// 4. JWT handler (injects Authorization header)
builder.Services.AddScoped<JwtHttpHandler>();

// 5. HttpClient using the JWT handler
builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<JwtHttpHandler>();
    handler.InnerHandler = new HttpClientHandler();

    return new HttpClient(handler)
    {
        BaseAddress = new Uri("https://localhost:7001")
    };
});

await builder.Build().RunAsync();