namespace Identity.Admin.Models.Users;

public record UpdateUserRequest(
    string Email,
    bool EmailConfirmed
);