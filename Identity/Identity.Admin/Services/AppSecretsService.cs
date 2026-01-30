using Identity.Client.Http;
using Identity.Client.Services;
using Identity.Contracts.Apps;

public sealed class AppSecretsService
    : ChildCrudServiceBase<AppSecretDto, AppSecretDto, CreateAppSecretRequest, UpdateAppSecretRequest>,
      IAppSecretsService
{
    public AppSecretsService(SafeHttp http)
        : base(http, "apps/{parentId}/secrets")
    {
    }

    protected override Guid GetId(UpdateAppSecretRequest request) => request.Id;
}
