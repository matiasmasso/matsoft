using Identity.Api.Configuration;
using Identity.Api.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Server.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// CORS for the WASM client
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7212")
              .AllowAnyHeader()
              .AllowAnyMethod();
        // .AllowCredentials() // not needed for token/discovery
    });
});

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var cs = builder.Configuration.GetConnectionString("IdentityDatabase")
        ?? throw new InvalidOperationException("Connection string 'IdentityDatabase' not found.");

    options.UseSqlServer(cs);
    options.UseOpenIddict();
});

// Controllers (no views needed for now)
builder.Services.AddControllersWithViews();

// OpenIddict
builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore()
               .UseDbContext<AppDbContext>();
    })
    .AddServer(options =>
    {
        options.SetAuthorizationEndpointUris("/connect/authorize");
        options.SetTokenEndpointUris("/connect/token");

        options.AllowAuthorizationCodeFlow()
               .RequireProofKeyForCodeExchange();

        options.RegisterScopes("openid", "profile", "email", "api");

        options.AddEphemeralEncryptionKey()
               .AddEphemeralSigningKey();

        options.UseAspNetCore()
               .EnableAuthorizationEndpointPassthrough();
               //.EnableTokenEndpointPassthrough();
    });

// Cookie auth
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/access-denied";
});

builder.Services.AddAuthorization();

var app = builder.Build();


//temp:
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// CORS must be before auth/authorization and before endpoints
app.UseCors();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Only attribute-routed controllers + OpenIddict
app.MapControllers();

// Seed OpenIddict apps/users
await OpenIddictConfig.SeedAsync(app.Services);

app.Run();

//using Identity.Api.Configuration;
//using Identity.Api.Data;
//using Identity.Api.Endpoints;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.EntityFrameworkCore;
//using OpenIddict.Abstractions;

//var builder = WebApplication.CreateBuilder(args);

//// CORS for the WASM client
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("ClientCorsPolicy", policy =>
//    {
//        policy.WithOrigins("https://localhost:7212")
//              .AllowAnyHeader()
//              .AllowAnyMethod();
//    });
//});

//// Database
//builder.Services.AddDbContext<AppDbContext>(options =>
//{
//    var cs = builder.Configuration.GetConnectionString("IdentityDatabase")
//        ?? throw new InvalidOperationException("Connection string 'IdentityDatabase' not found.");

//    options.UseSqlServer(cs);
//    options.UseOpenIddict();
//});

//// MVC (for AccountController + views)
//builder.Services.AddControllersWithViews();

//// OpenIddict SERVER
//builder.Services.AddOpenIddict()
//    .AddCore(options =>
//    {
//        options.UseEntityFrameworkCore()
//               .UseDbContext<AppDbContext>();
//    })
//    .AddServer(options =>
//    {
//        options.SetAuthorizationEndpointUris("/connect/authorize");
//        options.SetTokenEndpointUris("/connect/token");

//        options.AllowAuthorizationCodeFlow()
//               .RequireProofKeyForCodeExchange();

//        options.RegisterScopes("openid", "profile", "email");
//        options.RegisterScopes("api"); 

//        //replace next AddEphemeralEncryptionKey by dev certificates
//        //options.AddDevelopmentEncryptionCertificate()
//        //       .AddDevelopmentSigningCertificate();

//        options.AddEphemeralEncryptionKey() 
//               .AddEphemeralSigningKey();

//        //options.UseAspNetCore(); // <-- NO passthrough for now
//        options.UseAspNetCore()
//               .EnableAuthorizationEndpointPassthrough()
//               .EnableTokenEndpointPassthrough();
//    });

//// Cookie authentication for interactive login
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
//.AddCookie(options =>
//{
//    options.LoginPath = "/login";
//    options.AccessDeniedPath = "/access-denied";
//});
////builder.Services.AddAuthentication(options =>
////{
////    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
////})
////.AddCookie();

//// Authorization
//builder.Services.AddAuthorization();

//var app = builder.Build();

//app.UseHttpsRedirection();

//app.UseCors("ClientCorsPolicy");

//app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();
//// MVC routes
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//// Your custom endpoints (if they don’t conflict with /connect/*)
//app.MapAuthEndpoints();

//// Seed OpenIddict apps/users
//await OpenIddictConfig.SeedAsync(app.Services);

//app.Run();