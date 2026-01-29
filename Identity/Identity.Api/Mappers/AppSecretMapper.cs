using Identity.Api.Entities;
using Identity.Contracts.Apps;

namespace Identity.Api.Mappers;

public static class AppSecretMapper
{
    public static AppSecretDto ToDto(this AppSecret s)
        => new()
        {
            Id = s.Id,
            Provider = s.Provider,
            ClientId = s.ClientId
        };

    public static AppSecret ToEntity(this CreateAppSecretRequest r)
        => new()
        {
            Id = Guid.NewGuid(),
            AppId = r.AppId,
            Provider = r.Provider,
            ClientId = r.ClientId,
            ClientSecret = r.ClientSecret
        };

    public static void UpdateFrom(this AppSecret s, UpdateAppSecretRequest r)
    {
        s.Provider = r.Provider;
        s.ClientId = r.ClientId;
        s.ClientSecret = r.ClientSecret;
    }
}