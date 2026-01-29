using Identity.Contracts.Dashboard;
using Identity.Client.Http;

namespace Identity.Admin.Services
{
    public sealed class DashboardService(HttpClient http) : IDashboardService
    {
        public Task<DashboardDto> GetAsync()
            => http.SafeGetAsync<DashboardDto>("dashboard");
    }

}
