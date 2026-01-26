using Identity.Api.Domain.Apps;

namespace Identity.Api.Domain.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public bool IsActive { get; set; }
    }

}
