using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Identity.Manager.Services;

public class UserInfoService
{
    private readonly AuthenticationStateProvider _authStateProvider;

    public UserInfoService(AuthenticationStateProvider authStateProvider)
    {
        _authStateProvider = authStateProvider;
    }

    public async Task<UserInfo> GetUserInfoAsync()
    {
        var state = await _authStateProvider.GetAuthenticationStateAsync();
        var user = state.User;

        if (!user.Identity?.IsAuthenticated ?? true)
            return new UserInfo { IsAuthenticated = false };

        return new UserInfo
        {
            IsAuthenticated = true,
            Name = user.Identity?.Name,
            Claims = user.Claims
                .Select(c => new ClaimInfo(c.Type, c.Value))
                .ToList(),
            Roles = user.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList()
        };
    }
}

public class UserInfo
{
    public bool IsAuthenticated { get; set; }
    public string? Name { get; set; }
    public List<ClaimInfo>? Claims { get; set; }
    public List<string>? Roles { get; set; }
}

public record ClaimInfo(string Type, string Value);