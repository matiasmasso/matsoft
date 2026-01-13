// ******
// BLAZOR COOKIE Auth Code (begin)
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
// BLAZOR COOKIE Auth Code (end)
// ******

// Mat additions
using DTO;
using System.Globalization;
using Spa3.Shared;

//
// using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Spa3.Data;

var builder = WebApplication.CreateBuilder(args);


// ******
// BLAZOR COOKIE Auth Code (begin)
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
// BLAZOR COOKIE Auth Code (end)
// ******



// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();


// ******
// BLAZOR COOKIE Auth Code (begin)
// From: https://github.com/aspnet/Blazor/issues/1554
// HttpContextAccessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<HttpContextAccessor>();

builder.Services.AddScoped(sp => new HttpClient { });


//builder.Services.AddHttpClient();
//builder.Services.AddScoped<HttpClient>();
// BLAZOR COOKIE Auth Code (end)
// ******




//added 09/05/22 for language management
var lang = LangDTO.FromCultureInfo(CultureInfo.CurrentCulture.Name);
var appState = new Spa4.Shared.AppState(lang);
builder.Services.AddSingleton(sp => appState);
builder.Services.AddLocalization();


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


// ******
// BLAZOR COOKIE Auth Code (begin)
//app.UseHttpsRedirection();
//app.UseStaticFiles();
app.UseCookiePolicy();
app.UseAuthentication();
// BLAZOR COOKIE Auth Code (end)
// ******




app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
