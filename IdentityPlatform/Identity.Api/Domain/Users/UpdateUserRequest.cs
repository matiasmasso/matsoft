namespace Identity.Api.Models.Users;


public class UpdateUserRequest
{
    public string Email { get; set; } = default!;
    public bool IsActive { get; set; }
}