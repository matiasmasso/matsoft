namespace Identity.Services
{
    public interface IRoleService
    {
        Task AssignRole(Guid userId, Guid appId, Guid roleId);
    }

}
