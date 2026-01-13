using Microsoft.Extensions.Options;

namespace Wasm.Services
{
    public class ApiEndpointService
    {
        private readonly List<(string Name, string Url)> _endpoints;
        private int _index = 1;

        public event Action? OnChanged;

        public ApiEndpointService(IOptions<ApiEndpointOptions> options)
        {
            var o = options.Value;

            _endpoints = new List<(string Name, string Url)>
    {
        ("LocalApi", o.LocalApi),
        ("RemoteApi", o.RemoteApi)
    };
        }


        public string CurrentClientName => _endpoints[_index].Name;

        public string CurrentBaseUrl => _endpoints[_index].Url.TrimEnd('/') + "/";

        public void Toggle()
        {
            _index = (_index + 1) % _endpoints.Count;
            OnChanged?.Invoke();
        }

        public void Use(string name)
        {
            var idx = _endpoints.FindIndex(e => e.Name == name);
            if (idx >= 0)
            {
                _index = idx;
                OnChanged?.Invoke();
            }
        }
    }
}