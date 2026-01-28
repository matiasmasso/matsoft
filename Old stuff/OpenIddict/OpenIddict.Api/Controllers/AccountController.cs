using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using System.Security.Claims;

namespace Identity.Api.Controllers;

public class AccountController : Controller
{
    [HttpGet("/login")]
    public IActionResult Login(string? returnUrl)
    {
        // If OpenIddict didn't provide a returnUrl, fallback to "/"
        if (string.IsNullOrEmpty(returnUrl))
            returnUrl = "/";

        return View(model: returnUrl);
    }

    [HttpPost("/login/submit")]
    public async Task<IActionResult> LoginPost(string returnUrl)
    {
        Console.WriteLine(">>> returnUrl received in POST = " + returnUrl);

        var claims = new List<Claim>
        {
            new Claim(OpenIddictConstants.Claims.Subject, "1"),
            new Claim(OpenIddictConstants.Claims.Name, "matias")
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        // Redirect back to OpenIddict authorize endpoint
        return Redirect(returnUrl);
    }
}