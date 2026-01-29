using Identity.Contracts.Apps;

public interface IAppSecretsService
{
    Task<List<AppSecretDto>> GetAllAsync(Guid appId);
    Task<AppSecretDto> GetAsync(Guid appId, Guid secretId);
    Task CreateAsync(CreateAppSecretRequest request);
    Task UpdateAsync(UpdateAppSecretRequest request);
    Task DeleteAsync(Guid appId, Guid secretId);
}