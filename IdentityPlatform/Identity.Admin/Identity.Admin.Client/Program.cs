using MatComponents.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Only UI services needed by WASM-rendered components
builder.Services.AddScoped<ToastService>();

await builder.Build().RunAsync();