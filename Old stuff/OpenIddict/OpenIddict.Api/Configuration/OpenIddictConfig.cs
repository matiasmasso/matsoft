using Identity.Api.Data;
using Identity.Api.Models;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;

namespace Identity.Api.Configuration;

public static class OpenIddictConfig
{
    public static void AddOpenIddictServer(this IServiceCollection services)
    {
        services.AddOpenIddict()
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

                // Scopes are strings in OpenIddict 7.x
                options.RegisterScopes("openid", "profile", "email");

                options.AcceptAnonymousClients();

                options.AddDevelopmentEncryptionCertificate()
                       .AddDevelopmentSigningCertificate();

                options.UseAspNetCore()
                       .EnableAuthorizationEndpointPassthrough()
                       .EnableTokenEndpointPassthrough();
            })
            .AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();
            });
    }

    public static async Task SeedAsync(IServiceProvider provider)
    {
        using var scope = provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.EnsureCreatedAsync();

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (await manager.FindByClientIdAsync("wasm-client") is null)
        {
            var descriptor = new OpenIddictApplicationDescriptor
            {
                ClientId = "wasm-client",
                DisplayName = "Blazor WASM Test Client",

                // ⭐ Public client (no client_secret, PKCE)
                ClientType = OpenIddictConstants.ClientTypes.Public,

                RedirectUris =
            {
                new Uri("https://localhost:7212/authentication/login-callback")
            },

                PostLogoutRedirectUris =
            {
                new Uri("https://localhost:7212/")
            },

                Permissions =
            {
                // Endpoints
                OpenIddictConstants.Permissions.Endpoints.Authorization,
                OpenIddictConstants.Permissions.Endpoints.Token,

                // Grant types
                OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,

                // Response types
                OpenIddictConstants.Permissions.ResponseTypes.Code,

                // Scopes
                        OpenIddictConstants.Permissions.Prefixes.Scope + "openid",
                        OpenIddictConstants.Permissions.Prefixes.Scope + "profile",
                        OpenIddictConstants.Permissions.Prefixes.Scope + "email"
            },

                Requirements =
            {
                // PKCE required
                OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
            }
            };

            await manager.CreateAsync(descriptor);
        }
    }

    //public static async Task SeedAsync(IServiceProvider provider)
    //{
    //    using var scope = provider.CreateScope();
    //    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //    await context.Database.EnsureCreatedAsync();

    //    // Seed test user
    //    if (!context.TestUsers.Any())
    //    {
    //        context.TestUsers.Add(new TestUser
    //        {
    //            Email = "test@example.com",
    //            Password = "123456"
    //        });

    //        await context.SaveChangesAsync();
    //    }

    //    // Seed test client
    //    var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();


    //    if (await manager.FindByClientIdAsync("wasm-client") is null)
    //    {
    //        await manager.CreateAsync(new OpenIddictApplicationDescriptor
    //        {
    //            ClientId = "wasm-client",
    //            DisplayName = "Blazor WASM Test Client",

    //            RedirectUris =
    //                {
    //                    new Uri("https://localhost:7212/authentication/login-callback")
    //                },

    //            PostLogoutRedirectUris =
    //                {
    //                    new Uri("https://localhost:7212/")
    //                },


    //            Permissions =
    //                {
    //                    // Endpoints
    //                    OpenIddictConstants.Permissions.Endpoints.Authorization,
    //                    OpenIddictConstants.Permissions.Endpoints.Token,

    //                    // Grant types
    //                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,

    //                    // Response types
    //                    OpenIddictConstants.Permissions.ResponseTypes.Code,

    //                    // Scopes
    //                    OpenIddictConstants.Permissions.Prefixes.Scope + "openid",
    //                    OpenIddictConstants.Permissions.Prefixes.Scope + "profile",
    //                    OpenIddictConstants.Permissions.Prefixes.Scope + "email"

    //                },

    //            Requirements =
    //            {
    //                OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
    //            }
    //        });
    //    }
    //}
}