public interface IAppService
{
    Task<List<string>> GetAll();
    Task AssignAppsToUser(Guid userId, List<string> apps);
}
