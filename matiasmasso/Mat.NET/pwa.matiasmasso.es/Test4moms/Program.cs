using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using System.Globalization;
using Test4moms.Data;
using Test4moms.Services;
using Test4moms.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

//added to read external posted form data passing it from _host.html to the required component
var postFormService = new PostFormService();
builder.Services.AddScoped(sp=> postFormService);

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddHttpClient();

//added on 24/08/22 see https://stackoverflow.com/questions/73474199/cant-keep-cultureinfo-across-pages-on-net-maui-blazor
builder.Services.AddLocalization();
builder.Services.AddControllers();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICookie, Cookie>();


var state = new AppState();
builder.Services.AddSingleton(sp => state);



//-----------------------------------


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

//added by mat----------------------
var supportedCultures = new[] { "es", "ca", "en", "pt" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);
app.MapControllers();
//------------

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
