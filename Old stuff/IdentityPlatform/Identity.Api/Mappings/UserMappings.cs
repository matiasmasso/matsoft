using Identity.Api.Domain.Users;

public static class UserMappings
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Id = new Guid(user.Id),
            Email = user.Email,
            IsActive = user.IsActive
        };
    }
}