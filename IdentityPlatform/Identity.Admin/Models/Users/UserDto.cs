namespace Identity.Admin.Models.Users;

public record UserDto(
    Guid Id,
    string Username,
    string Email,
    bool EmailConfirmed,
    IEnumerable<string> Roles
);