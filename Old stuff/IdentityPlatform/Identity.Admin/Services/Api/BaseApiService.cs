using Identity.Admin.Http;

namespace Identity.Admin.Services.Api;

public abstract class BaseApiService
{
    protected readonly SafeHttpClient Http;

    protected BaseApiService(SafeHttpClient http)
    {
        Http = http;
    }
}