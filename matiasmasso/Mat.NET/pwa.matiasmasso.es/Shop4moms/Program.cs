//4moms

using Microsoft.AspNetCore.Components.Authorization;
using Shop4moms.Services;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Blazor.Analytics;
using Microsoft.AspNetCore.Rewrite;
using DTO;
using Shop4moms.Extensions;
using DTO.Helpers;
using Microsoft.Net.Http.Headers;

Globals.UseLocalApi = false ; //-----------------------


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

//added on 2023.03.28 
builder.Services.AddGoogleAnalytics("G-MCQ1J0Y4SH");

//added to store session data as currently logged user
builder.Services.AddScoped<ProtectedSessionStorage>();

//added to improve performance measured on Google lighthouse tool
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

//added to read external posted form data passing it from _host.html to the required component
//builder.Services.AddScoped<PostFormService>();
var postFormService = new PostFormService();
builder.Services.AddScoped(sp => postFormService);


//added on 24/08/22

builder.Services.AddControllers();
builder.Services.AddRedsysTpv( DTO.Integracions.Redsys.Tpv.Ids.Shop4moms);
builder.Services.AddBlazorContextMenu();

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ICatalogService, CatalogService>();
builder.Services.AddSingleton<GeocodeService>();
builder.Services.AddSingleton<AppStateService>();
builder.Services.AddSingleton<StringsLocalizerService>();
builder.Services.AddSingleton<RoutesService>();
builder.Services.AddSingleton<ProductCategoriesService>();
builder.Services.AddSingleton<IncidenciasService>();
builder.Services.AddSingleton<GravatarService>();

builder.Services.AddScoped<SessionStateService>();
builder.Services.AddScoped<CookieService>();
builder.Services.AddScoped<CultureService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<SitemapService> ();
builder.Services.AddScoped<NavService>();
builder.Services.AddScoped<BasketService>();
builder.Services.AddOutputCache(options => options.AddCustomPolicies());



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

//set it before any resources which should be compressed
app.UseResponseCompression();


// Add your rule as shown here:
var options = new RewriteOptions();
options.AddRedirectToHttps();
options.Rules.Add(new RedirectToWwwRule());
app.UseRewriter(options);

// app.UseHttpsRedirection(); <-- Remove this line since it's already part of the rewrite options

app.UseStaticFiles();

//app.UseStaticFiles(new StaticFileOptions
//{
//    ServeUnknownFileTypes = false,
//    OnPrepareResponse = ctx =>
//    {
//        const int durationInSeconds = 60 * 60 * 24;
//        ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=" + durationInSeconds;
//        ctx.Context.Response.Headers[HeaderNames.Expires] = new[] { DateTime.UtcNow.AddYears(1).ToString("R") }; // Format RFC1123
//    }
//});

//app.Use(async (context, next) =>
//{
//    string path = context.Request.Path;

//    if (path.EndsWith(".css") || path.EndsWith(".js"))
//    {
//        //Set css and js files to be cached for 1 days
//        TimeSpan maxAge = new TimeSpan(1, 0, 0, 0);     //1 days
//        context.Response.Headers.Append("Cache-Control", "max-age=" + maxAge.TotalSeconds.ToString("0"));

//    }
//    else if (path.EndsWith(".gif") || path.EndsWith(".jpg") || path.EndsWith(".png") || path.EndsWith(".svg"))
//    {
//        TimeSpan maxAge = new TimeSpan(1, 0, 0, 0);     //1 days
//        context.Response.Headers.Append("Cache-Control", "max-age=" + maxAge.TotalSeconds.ToString("0"));
//    }
//    else
//    {
//        //Request for views fall here.
//        context.Response.Headers.Append("Cache-Control", "no-cache");
//        context.Response.Headers.Append("Cache-Control", "private, no-store");

//    }
//    await next();
//});

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

app.MapGet("/robots.txt", RobotsService.GetFile);
//app.MapGet("/sitemap.xml", SitemapService.GetSitemapAsync);

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
