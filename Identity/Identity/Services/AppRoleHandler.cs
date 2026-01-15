using Identity.Attributes;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Identity.Services;


public class AppRoleHandler : AuthorizationHandler<AppRoleRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AppRoleRequirement requirement)
    {
        if (context.Resource is not Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext mvcContext)
        {
            return Task.CompletedTask;
        }

        var endpoint = mvcContext.HttpContext.GetEndpoint();
        var attribute = endpoint?.Metadata.GetMetadata<AuthorizeAppRoleAttribute>();
        if (attribute == null)
            return Task.CompletedTask;

        var (appName, roleName) = Parse(attribute.Policy);

        // 1. Check app_id claim
        var appIdClaim = context.User.FindFirst("app_id");
        if (appIdClaim == null)
            return Task.CompletedTask;

        // 2. Check role claim
        var hasRole = context.User.IsInRole(roleName);
        if (!hasRole)
            return Task.CompletedTask;

        context.Succeed(requirement);
        return Task.CompletedTask;
    }

    private (string app, string role) Parse(string policy)
    {
        var parts = policy.Split(':');
        return (parts[0], parts[1]);
    }
}
