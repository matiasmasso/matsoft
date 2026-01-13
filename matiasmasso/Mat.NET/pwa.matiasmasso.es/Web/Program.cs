using Microsoft.AspNetCore.Components.Authorization;
using Web.Services;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Components;
using Components.Services;
using Blazor.Analytics;
using DTO.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

//added on 2023.03.28 
builder.Services.AddGoogleAnalytics(); // TODO: add TrackingKeyId

//added to read external posted form data passing it from _host.html to the required component
var postFormService = new PostFormService();
builder.Services.AddScoped(sp => postFormService);

//added on 24/08/22
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllers();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICookie, Cookie>();
builder.Services.AddSingleton<ICatalogService, CatalogService>();
builder.Services.AddScoped<NavService>();

builder.Services.AddSingleton<IAppState, AppState>();
builder.Services.AddSingleton<StringsLocalizerService>();
builder.Services.AddCulture(builder.Environment, UrlHelper.Domains.matiasmasso);

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddHttpClient();


//builder.Services.AddOutputCache();

//--- prevents Required field exceptions when serializing
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

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

//builder.Services.AddOutputCache(options =>
//{
//    options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(20);
//});

app.Run();
