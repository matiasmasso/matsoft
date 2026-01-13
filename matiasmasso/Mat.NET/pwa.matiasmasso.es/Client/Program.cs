using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;
using Client;
using Client.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

//this defines the replacement of the #app div tag on Index.htrml
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//added for app state management
builder.Services.AddSingleton(sp => new AppState());

//added 09/05/22 for language management
builder.Services.AddLocalization();

//added 09/05/22 for language management
var host = builder.Build();
var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
var result = await jsInterop.InvokeAsync<string>("appCulture.get");
if (result != null)
{
	var culture = new CultureInfo(result);
	CultureInfo.DefaultThreadCurrentCulture = culture;
	CultureInfo.DefaultThreadCurrentUICulture = culture;
}

await host.RunAsync();
