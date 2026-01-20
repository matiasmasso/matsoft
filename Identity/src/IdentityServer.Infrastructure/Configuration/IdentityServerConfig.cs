using Duende.IdentityServer.Models;

namespace IdentityServer.Infrastructure.Configuration;

public static class IdentityServerConfig
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("email", new[] { "email" }),
            new IdentityResource("roles", new[] { "role" })
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("api", "Main API")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "identity-manager",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,

                RedirectUris =
                {
                    "https://local.identitymanager.test:7273/auth/callback"
                },

                PostLogoutRedirectUris =
                {
                    "https://local.identitymanager.test:7273/"
                },

                AllowedCorsOrigins =
                {
                    "https://local.identitymanager.test:7273"
                },

                AllowedScopes =
                {
                    "openid", "profile", "email", "roles"
                },

                AllowAccessTokensViaBrowser = true
            }
        };
}