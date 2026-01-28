namespace Identity.Admin.Models;

public record ApiError(
    string Code,
    string Message
);