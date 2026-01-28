using Identity.Api.Data;
using Identity.Api.Models;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;

namespace Identity.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (string email, string password, AppDbContext db) =>
        {
            var user = await db.TestUsers
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user is null)
                return Results.Unauthorized();

            var claims = new List<Claim>
            {
                new(OpenIddictConstants.Claims.Subject, user.Id.ToString()),
                new(OpenIddictConstants.Claims.Email, user.Email)
            };

            var identity = new ClaimsIdentity(
                claims,
                OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            // Scopes are strings in OpenIddict 7.x
            principal.SetScopes("openid", "profile", "email");

            return Results.SignIn(
                principal,
                authenticationScheme: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme
            );
        });
    }
}