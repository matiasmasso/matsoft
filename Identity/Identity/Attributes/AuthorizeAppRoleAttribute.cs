    using Microsoft.AspNetCore.Authorization;
namespace Identity.Attributes;


public class AuthorizeAppRoleAttribute : AuthorizeAttribute
{
    public AuthorizeAppRoleAttribute(string applicationName, string roleName)
    {
        Policy = $"{applicationName}:{roleName}";
    }
}
