namespace Identity.Api.Models.Users;

public class ResetPasswordRequest
{
    public string NewPassword { get; set; } = default!;
}