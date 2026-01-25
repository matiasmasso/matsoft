namespace Identity.Admin.Models.Users;

public record CreateUserRequest(
    string Username,
    string Email,
    string Password
);