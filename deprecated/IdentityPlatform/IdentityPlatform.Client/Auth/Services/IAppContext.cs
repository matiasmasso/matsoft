namespace IdentityPlatform.Client.Auth.Services;

public interface IAppContext
{
    Guid? CurrentAppId { get; }
    Task SetCurrentAppAsync(Guid appId);
}