using System.Text;
using Identity.Api.Data;
using Identity.Api.Models;
using Identity.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------
// Configuration
// ----------------------------------------
var configuration = builder.Configuration;

// ----------------------------------------
// DbContext
// ----------------------------------------
builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// ----------------------------------------
// Identity
// ----------------------------------------
builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();

// ----------------------------------------
// Authentication / JWT
// ----------------------------------------
var jwtKey = configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is not configured");
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "identity-api",

            ValidateAudience = true,
            // Audience will be the clientId (per app)
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ClockSkew = TimeSpan.FromMinutes(1)
        };
    });

// ----------------------------------------
// CORS
// ----------------------------------------
var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("wasm", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ----------------------------------------
// DI for services
// ----------------------------------------
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<AppleAuthService>();
builder.Services.AddScoped<GoogleAuthService>();


builder.Services.AddControllers();

var app = builder.Build();

// ----------------------------------------
// Middleware pipeline
// ----------------------------------------

app.UseCors("wasm");

app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();   // <-- activa index.html com a p gina per defecte
app.UseStaticFiles();    // <-- serveix wwwroot

app.MapControllers();

await DbInitializer.InitializeAsync(app.Services, builder.Configuration);

app.Run();