using Identity.Api.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Identity.Api.Application.Auth;

public class AuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // REGISTER A NEW USER
    public async Task<IdentityResult> RegisterAsync(string username, string email, string password)
    {
        var user = new User
        {
            UserName = username,
            Email = email
        };

        return await _userManager.CreateAsync(user, password);
    }

    // VALIDATE CREDENTIALS (NO TOKEN ISSUING)
    public async Task<SignInResult> ValidateCredentialsAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
            return SignInResult.Failed;

        return await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);
    }

    // INTERACTIVE SIGN-IN (COOKIE)
    public async Task<bool> SignInAsync(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(
            username,
            password,
            isPersistent: true,
            lockoutOnFailure: false);

        return result.Succeeded;
    }

    // SIGN OUT (COOKIE)
    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    // GET USER BY USERNAME
    public async Task<User?> GetUserAsync(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }
}