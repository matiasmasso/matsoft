namespace Identity.Admin.Models.Apps;

public record AppDto(
    Guid Id,
    string Key,
    string Name,
    string? Description,
    string? ClientSecret
);