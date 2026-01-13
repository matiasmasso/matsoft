using System.Net;
using System.Net.Http.Headers;

namespace Wasm.Services
{
    public class TokenHttpHandler : DelegatingHandler
    {
        private readonly ITokenStore _tokenStore;
        private readonly AuthService _authService;
        private static readonly SemaphoreSlim _refreshLock = new(1, 1);

        public TokenHttpHandler(ITokenStore tokenStore, AuthService authService)
        {
            _tokenStore = tokenStore;
            _authService = authService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // ---------------------------------------------------------
            // 1. Attach access token if present
            // ---------------------------------------------------------
            var bundle = await _tokenStore.GetBundleAsync();
            if (!string.IsNullOrWhiteSpace(bundle?.AccessToken))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", bundle.AccessToken);
            }

            // ---------------------------------------------------------
            // 2. First attempt
            // ---------------------------------------------------------
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode != HttpStatusCode.Unauthorized)
                return response;

            // ---------------------------------------------------------
            // 3. 401 → try refresh (only once, globally locked)
            // ---------------------------------------------------------
            await _refreshLock.WaitAsync(cancellationToken);
            try
            {
                var refreshed = await _authService.TryRefreshAsync();

                if (!refreshed)
                {
                    // Refresh failed → clear tokens → return original 401
                    await _tokenStore.ClearAsync();
                    return response;
                }
            }
            finally
            {
                _refreshLock.Release();
            }

            // ---------------------------------------------------------
            // 4. Refresh succeeded → retry request with new token
            // ---------------------------------------------------------
            var retry = await CloneRequestAsync(request);

            var newBundle = await _tokenStore.GetBundleAsync();
            if (!string.IsNullOrWhiteSpace(newBundle?.AccessToken))
            {
                retry.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", newBundle.AccessToken);
            }

            return await base.SendAsync(retry, cancellationToken);
        }

        // ---------------------------------------------------------
        // Clone request for safe retry (required in .NET 8–10)
        // ---------------------------------------------------------
        private static async Task<HttpRequestMessage> CloneRequestAsync(HttpRequestMessage original)
        {
            var clone = new HttpRequestMessage(original.Method, original.RequestUri);

            // Copy content
            if (original.Content != null)
            {
                var ms = new MemoryStream();
                await original.Content.CopyToAsync(ms);
                ms.Position = 0;

                clone.Content = new StreamContent(ms);

                foreach (var header in original.Content.Headers)
                    clone.Content.Headers.Add(header.Key, header.Value);
            }

            // Copy headers
            foreach (var header in original.Headers)
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

            return clone;
        }
    }
}
