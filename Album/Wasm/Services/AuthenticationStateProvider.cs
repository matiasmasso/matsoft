using System.Security.Claims;
using DTO;
using Microsoft.AspNetCore.Components.Authorization;

namespace Wasm.Services
{
    public class JwtAuthStateProvider : AuthenticationStateProvider
    {
        private readonly TokenStore _tokenStore;

        public JwtAuthStateProvider(TokenStore tokenStore)
        {
            _tokenStore = tokenStore;

            // Stay in sync with TokenStore
            _tokenStore.BundleChanged += OnBundleChanged;
        }

        private void OnBundleChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var bundle = await _tokenStore.GetBundleAsync();

            if (bundle is null || bundle.IsExpired())
            {
                return new AuthenticationState(
                    new ClaimsPrincipal(new ClaimsIdentity())
                );
            }

            var principal = BuildPrincipal(bundle.AccessToken!);
            return new AuthenticationState(principal);
        }

        public async Task MarkUserAsAuthenticatedAsync(TokenBundle bundle)
        {
            // TokenStore event will trigger UI update
            await _tokenStore.SetBundleAsync(bundle);
        }

        public async Task MarkUserAsLoggedOutAsync()
        {
            // TokenStore event will trigger UI update
            await _tokenStore.ClearAsync();
        }

        private ClaimsPrincipal BuildPrincipal(string jwt)
        {
            var claims = JwtParser.ParseClaims(jwt);
            var identity = new ClaimsIdentity(claims, "jwt");
            return new ClaimsPrincipal(identity);
        }
    }
}