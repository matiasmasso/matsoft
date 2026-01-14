public interface IRoleService
{
    Task<List<string>> GetAll();
    Task AssignRolesToUser(Guid userId, List<string> roles);
}
