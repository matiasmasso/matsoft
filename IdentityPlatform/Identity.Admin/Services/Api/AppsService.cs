using Identity.Admin.Http;
using Identity.Admin.Models.Apps;
using Identity.Admin.Services.Api;

namespace Identity.Admin.Services.Api;

public class AppsService : BaseApiService
{
    public AppsService(SafeHttpClient http) : base(http) { }

    public Task<List<AppDto>?> GetAllAsync()
        => Http.GetAsync<List<AppDto>>("apps");

    public Task<AppDto?> GetByIdAsync(Guid id)
        => Http.GetAsync<AppDto>($"apps/{id}");

    public Task<AppDto?> CreateAsync(CreateAppRequest request)
        => Http.PostAsync<CreateAppRequest, AppDto>("apps", request);

    public Task<AppDto?> UpdateAsync(Guid id, UpdateAppRequest request)
        => Http.PutAsync<UpdateAppRequest, AppDto>($"apps/{id}", request);

    public Task<bool> DeleteAsync(Guid id)
        => Http.DeleteAsync($"apps/{id}");
}