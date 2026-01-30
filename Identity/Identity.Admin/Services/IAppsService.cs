using Identity.Client.Http;
using Identity.Contracts.Apps;

public interface IAppsService
{
    Task<Result<List<AppDto>>> GetAllAsync();
    Task<Result<AppDto>> GetAsync(Guid id);
    Task<Result<AppDto>> CreateAsync(AppDto dto);
    Task<Result<AppDto>> UpdateAsync(AppDto dto);
    Task<Result<bool>> DeleteAsync(Guid id);
}

