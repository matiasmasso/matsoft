using Identity.Api.Application.Apps;
using Identity.Api.Application.Auth;
using Identity.Api.Data;
using Identity.Api.Domain.Users;
using Identity.Api.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// EF Core + OpenIddict stores
builder.Services.AddDbContext<IdentityDbContext>(options =>
{
    var cs = builder.Configuration.GetConnectionString("IdentityDatabase")
        ?? throw new InvalidOperationException("Connection string 'IdentityDatabase' not found.");

    options.UseSqlServer(cs);
    options.UseOpenIddict();
});

// ASP.NET Core Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();

// Cookie authentication for interactive login
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
});

//-----------------------------------------------------------------------
// OpenIddict
//-----------------------------------------------------------------------

builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore()
               .UseDbContext<IdentityDbContext>();
    })
    .AddServer(options =>
    {
        options.SetAuthorizationEndpointUris("/connect/authorize");
        options.SetTokenEndpointUris("/connect/token");

        options.AllowAuthorizationCodeFlow()
               .RequireProofKeyForCodeExchange();

        options.RegisterScopes("openid", "profile", "email");

        options.UseAspNetCore()
               .EnableAuthorizationEndpointPassthrough()
               .EnableTokenEndpointPassthrough()
               .EnableStatusCodePagesIntegration();

        options.AddDevelopmentEncryptionCertificate()
               .AddDevelopmentSigningCertificate();
    })
    .AddValidation(options =>
    {
        options.UseLocalServer();
        options.UseAspNetCore();
    });


// Controllers
builder.Services.AddControllers();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(_ => true);
    });
});

// Your application services
builder.Services.AddScoped<AppService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<EnrollmentService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseMiddleware<JsonExceptionMiddleware>();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

async Task RegisterClientsAsync(IServiceProvider services)
{
    Console.WriteLine("RegisterClientsAsync is running...");
    var manager = services.GetRequiredService<IOpenIddictApplicationManager>();

    if (await manager.FindByClientIdAsync("mat-album-wasm") is null)
    {
        await manager.CreateAsync(new OpenIddictApplicationDescriptor
        {
            ClientId = "mat-album-wasm",
            DisplayName = "Album app",
            ConsentType = OpenIddictConstants.ConsentTypes.Explicit,

            RedirectUris =
    {
        new Uri("https://localhost:7297/authentication/login-callback")
    },

            PostLogoutRedirectUris =
    {
        new Uri("https://localhost:7297/authentication/logout-callback")
    },

            Permissions =
    {
        OpenIddictConstants.Permissions.Endpoints.Authorization,
        OpenIddictConstants.Permissions.Endpoints.Token,

        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
        OpenIddictConstants.Permissions.ResponseTypes.Code,

        OpenIddictConstants.Permissions.Prefixes.Scope + "openid",
        OpenIddictConstants.Permissions.Prefixes.Scope + "profile",
        OpenIddictConstants.Permissions.Prefixes.Scope + "email"
    }
        });
    }
}

using (var scope = app.Services.CreateScope())
{
    await RegisterClientsAsync(scope.ServiceProvider);
}

app.Run();
