namespace Identity.Contracts.Dashboard;

public sealed class DashboardDto
{
    public int TotalApps { get; set; }
    public int TotalUsers { get; set; }
    public int TotalRoles { get; set; }
    public int TotalSecrets { get; set; }
}