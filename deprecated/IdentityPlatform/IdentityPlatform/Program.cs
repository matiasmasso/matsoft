using IdentityPlatform.Auth.Models;
using IdentityPlatform.Auth.Services;
using IdentityPlatform.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity core (password hashing, user validation)
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// Auth services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"]!;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// External providers (skeleton)
//builder.Services.AddAuthentication()
//    .AddGoogle("Google", options =>
//    {
//        options.ClientId = builder.Configuration["Google:ClientId"]!;
//        options.ClientSecret = builder.Configuration["Google:ClientSecret"]!;
//    })
    //.AddApple(options =>
    //{
    //    options.ClientId = builder.Configuration["Apple:ClientId"]!;
    //    options.KeyId = builder.Configuration["Apple:KeyId"]!;
    //    options.TeamId = builder.Configuration["Apple:TeamId"]!;
    //    options.PrivateKey = (keyId, cancellationToken) =>
    //    {
    //        var privateKey = builder.Configuration["Apple:PrivateKey"]!;
    //        return Task.FromResult((ReadOnlyMemory<char>)privateKey.AsMemory());
    //    };

    //});

builder.Services.AddControllers();
var app = builder.Build();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();   // ⭐ REQUIRED for WASM
app.UseStaticFiles();            // ⭐ REQUIRED for WASM

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();