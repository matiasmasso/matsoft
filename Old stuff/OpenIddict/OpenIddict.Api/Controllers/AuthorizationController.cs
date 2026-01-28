using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;

namespace Identity.Api.Controllers;

public class AuthorizationController : Controller
{
    [HttpGet("~/connect/authorize")]
    public IActionResult Authorize()
    {
        var request = HttpContext.GetOpenIddictServerRequest()
                     ?? throw new InvalidOperationException("OpenIddict request missing.");

        if (!User.Identity?.IsAuthenticated ?? true)
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = Request.Path + Request.QueryString
            }, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        var principal = new ClaimsPrincipal(
            new ClaimsIdentity(User.Claims,
                OpenIddictServerAspNetCoreDefaults.AuthenticationScheme));

        principal.SetScopes(request.GetScopes());

        principal.SetResources("api"); // ⭐ REQUIRED

        principal.SetDestinations(claim => claim.Type switch
        {
            OpenIddictConstants.Claims.Subject => new[]
            {
            OpenIddictConstants.Destinations.AccessToken,
            OpenIddictConstants.Destinations.IdentityToken
        },
            OpenIddictConstants.Claims.Name => new[]
            {
            OpenIddictConstants.Destinations.AccessToken,
            OpenIddictConstants.Destinations.IdentityToken
        },
            _ => new[] { OpenIddictConstants.Destinations.AccessToken }
        });

        SignInResult? retval=null;
        try
        {
            retval = SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        } catch(Exception ex)
        {
            var msg = ex.Message;
        }
        return retval;
    }
}