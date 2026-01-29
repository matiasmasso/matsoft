using Identity.Contracts.Apps;
using static System.Net.WebRequestMethods;

public interface IAppsService
{
    Task<List<AppDto>> GetAllAsync();
    Task<AppDto> GetAsync(Guid id);
    Task CreateAsync(AppDto dto);
    Task UpdateAsync(AppDto dto);
    Task DeleteAsync(Guid id);


}