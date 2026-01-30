using Identity.Client.Http;
using Identity.Contracts.Dashboard;

namespace Identity.Admin.Services;

public sealed class DashboardService(SafeHttp http) : IDashboardService
{
    public Task<Result<DashboardDto>> GetAsync()
        => http.Get<DashboardDto>("dashboard");
}
