using Identity.Api.Domain.Users;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;

namespace Identity.Api.Controllers;

[ApiController]
public class AuthorizationController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AuthorizationController(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet("/connect/authorize")]
    public async Task<IActionResult> Authorize()
    {
        var request = HttpContext.GetOpenIddictServerRequest()
            ?? throw new InvalidOperationException("OIDC request missing");

        // If the user is not authenticated, redirect to login
        if (!User.Identity!.IsAuthenticated)
        {
            return Challenge(
                authenticationSchemes: IdentityConstants.ApplicationScheme,
                properties: new AuthenticationProperties
                {
                    RedirectUri = Request.Path + QueryString.Create(Request.Query)
                });
        }

        // Load the user
        var user = await _userManager.GetUserAsync(User)
            ?? throw new InvalidOperationException("User not found");

        // Create the principal
        var principal = await _signInManager.CreateUserPrincipalAsync(user);

        // (Optional) If your OpenIddict version supports scopes:
        // principal.SetScopes(request.GetScopes());

        return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }
}