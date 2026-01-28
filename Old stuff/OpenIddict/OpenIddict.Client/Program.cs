using Identity.Admin;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure OIDC
builder.Services.AddOidcAuthentication(static options =>
{
    options.ProviderOptions.Authority = "https://localhost:7001/";
    options.ProviderOptions.ClientId = "wasm-client";
    options.ProviderOptions.ResponseType = "code";

    options.ProviderOptions.DefaultScopes.Clear();
    options.ProviderOptions.DefaultScopes.Add("openid");
    options.ProviderOptions.DefaultScopes.Add("profile");
    options.ProviderOptions.DefaultScopes.Add("email");

    options.ProviderOptions.RedirectUri = "authentication/login-callback";
    options.ProviderOptions.PostLogoutRedirectUri = "/";
});


await builder.Build().RunAsync();