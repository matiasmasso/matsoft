using Identity.Api.Domain.Users;

namespace Identity.Api.Mappings;

public static class UserProfileMappings
{
    public static UserProfileDto ToProfileDto(this User user)
    {
        return new UserProfileDto
        {
            Email = user.Email,
            Name = user.Name,
            AvatarUrl = user.AvatarUrl,
            Preferences = user.Preferences
        };
    }
}