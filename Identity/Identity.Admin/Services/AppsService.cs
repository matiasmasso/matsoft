using Identity.Client.Http;
using Identity.Client.Services;
using Identity.Contracts.Apps;

public sealed class AppsService
    : CrudServiceBase<AppDto, AppDto, AppDto, AppDto>, IAppsService
{
    public AppsService(SafeHttp http)
        : base(http, "apps")
    {
    }

    protected override Guid GetId(AppDto dto) => dto.Id;
}
