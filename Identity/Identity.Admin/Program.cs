using Identity.Admin;
using Identity.Admin.Services;
using Identity.Client.Auth;
using Identity.Client.Extensions;
using Identity.Client.Http;
using Identity.Client.Notifications;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ToastService>();
builder.Services.AddScoped<IErrorNotifier, ToastErrorNotifier>();
builder.Services.AddIdentityClient("https://localhost:7001");

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<JwtAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<JwtAuthenticationStateProvider>());

// Token store
builder.Services.AddScoped<ITokenStore, LocalStorageTokenStore>();


// App-specific services
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IAppRolesService, AppRolesService>();
builder.Services.AddScoped<IAppsService, AppsService>();
builder.Services.AddScoped<IAppSecretsService, AppSecretsService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<ModalService>();

var host = builder.Build().RunAsync();



