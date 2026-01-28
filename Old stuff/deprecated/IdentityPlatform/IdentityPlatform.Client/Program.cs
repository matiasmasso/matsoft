using Blazored.LocalStorage;
using IdentityPlatform.Client.Auth.Providers;
using IdentityPlatform.Client.Auth.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<ITokenStorage, LocalTokenStorage>();
builder.Services.AddScoped<RefreshTokenHandler>();
builder.Services.AddScoped<IAppContext, IdentityPlatform.Client.Auth.Services.AppContext>();
builder.Services.AddHttpClient("auth", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

builder.Services.AddHttpClient("api", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
})
.AddHttpMessageHandler<RefreshTokenHandler>();

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("api"));

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthStateProvider>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();