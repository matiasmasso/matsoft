using Identity.Contracts.Dashboard;

public interface IDashboardService
{
    Task<DashboardDto> GetAsync();
}

