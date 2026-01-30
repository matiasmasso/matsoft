using Identity.Client.Http;
using Identity.Contracts.Dashboard;

public interface IDashboardService
{
    Task<Result<DashboardDto>> GetAsync();
}
