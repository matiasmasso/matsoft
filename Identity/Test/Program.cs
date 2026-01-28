using Identity.Admin;
using Identity.Client.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddIdentityClient("https://localhost:7001");

// App-specific services
//builder.Services.AddScoped<UsersService>();
//builder.Services.AddScoped<RolesService>();

await builder.Build().RunAsync();