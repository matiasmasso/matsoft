using Identity.Client.Http;
using Identity.Contracts.Apps;

public sealed class AppRolesService
    : ChildCrudServiceBase<AppRoleDto, AppRoleDto, CreateAppRoleRequest, UpdateAppRoleRequest>,
      IAppRolesService
{
    public AppRolesService(SafeHttp http)
        : base(http, "apps/{parentId}/roles")
    {
    }

    protected override Guid GetId(UpdateAppRoleRequest request) => request.Id;
}


