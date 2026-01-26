using Identity.Api.Domain.Apps;

public static class AppRoleMappings
{
    public static AppRoleDto ToDto(this AppRole role)
    {
        return new AppRoleDto
        {
            Id = role.Id,
            AppId = role.AppId,
            Name = role.Name
        };
    }
}