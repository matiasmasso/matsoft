using Wasm.Services;

namespace Wasm.Services
{
    public class DynamicBaseUrlHandler : DelegatingHandler

    {
        private readonly ApiEndpointService _endpointService;

        public DynamicBaseUrlHandler(ApiEndpointService endpointService)
        {
            _endpointService = endpointService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var baseUri = new Uri(_endpointService.CurrentBaseUrl);

            Console.WriteLine($"@@ Incoming URI: {request.RequestUri}");

            if (request.RequestUri!.IsAbsoluteUri)
            {
                // Replace only the host, keep the path/query
                request.RequestUri = new Uri(baseUri, request.RequestUri.PathAndQuery);
            }
            else
            {
                // Relative → combine normally
                request.RequestUri = new Uri(baseUri, request.RequestUri);
            }

            Console.WriteLine($"@@ Final URI: {request.RequestUri}");

            var response = await base.SendAsync(request, cancellationToken);
            Console.WriteLine($"@@ Response: {response.StatusCode}");
            return response;
        }



    }
}
