using Identity.Client.Auth;
using Identity.Client.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Authorization;

namespace Identity.Client.Extensions;

public static class IdentityClientExtensions
{
    public static IServiceCollection AddIdentityClient(
        this IServiceCollection services,
        string apiBaseUrl)
    {
        services.AddAuthorizationCore();

        services.AddScoped<ITokenStore, LocalStorageTokenStore>();
        services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
        services.AddScoped<JwtHttpHandler>();
        services.AddScoped<SafeHttp>();

        services.AddScoped(sp =>
        {
            var handler = sp.GetRequiredService<JwtHttpHandler>();
            handler.InnerHandler = new HttpClientHandler();

            return new HttpClient(handler)
            {
                BaseAddress = new Uri(apiBaseUrl)
            };
        });

        return services;
    }
}