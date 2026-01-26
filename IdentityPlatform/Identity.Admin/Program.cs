using Identity.Admin;
using Identity.Admin.Http;
using Identity.Admin.Services.Api;
using Identity.Admin.Services.Auth;
using Identity.Admin.Services.UI;
using Identity.Admin.Utils;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// -----------------------------------------------------
// 1. Load API URL from appsettings.json
// -----------------------------------------------------
var apiBaseUrl = builder.Configuration["ApiBaseUrl"]
    ?? throw new Exception("ApiBaseUrl missing in appsettings.json");

// -----------------------------------------------------
// 2. Core storage + token services
// -----------------------------------------------------
builder.Services.AddScoped<LocalStorage>();
builder.Services.AddScoped<ITokenProvider, LocalStorageTokenProvider>();
builder.Services.AddScoped<TokenStorage>();

// -----------------------------------------------------
// 3. Notification system (toasts)
// -----------------------------------------------------
builder.Services.AddSingleton<ToastService>();
builder.Services.AddScoped<INotificationService, ToastNotificationService>();

// -----------------------------------------------------
// 4. Auth handlers (must be BEFORE HttpClient registrations)
// -----------------------------------------------------
builder.Services.AddTransient<AuthHeaderHandler>();
builder.Services.AddScoped<RefreshTokenHandler>();

// -----------------------------------------------------
// 5. ONE authenticated HttpClient for the entire app
// -----------------------------------------------------
builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
})
.AddHttpMessageHandler<RefreshTokenHandler>()
.AddHttpMessageHandler<AuthHeaderHandler>();

// -----------------------------------------------------
// 6. SafeHttpClient wrapper (uses the authenticated HttpClient)
// -----------------------------------------------------
builder.Services.AddScoped<SafeHttpClient>(sp =>
{
    var http = sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api");
    var notify = sp.GetRequiredService<INotificationService>();
    return new SafeHttpClient(http, notify);
});

// -----------------------------------------------------
// 7. API services (all inherit BaseApiService)
// -----------------------------------------------------
builder.Services.AddScoped<AppsService>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<AppEnrollmentsService>();
builder.Services.AddScoped<UserRolesService>();
builder.Services.AddScoped<AppRolesService>();

// -----------------------------------------------------
await builder.Build().RunAsync();