using Identity.Client.Http;
using Identity.Contracts.Apps;

public interface IAppSecretsService
{
    Task<Result<List<AppSecretDto>>> GetAllAsync(Guid appId);
    Task<Result<AppSecretDto>> GetAsync(Guid appId, Guid secretId);
    Task<Result<AppSecretDto>> CreateAsync(Guid appId, CreateAppSecretRequest request);
    Task<Result<AppSecretDto>> UpdateAsync(Guid appId, UpdateAppSecretRequest request);
    Task<Result<bool>> DeleteAsync(Guid appId, Guid secretId);
}
