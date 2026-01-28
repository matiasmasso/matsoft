namespace Identity.Api.Models.Users;

public class CreateUserRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

