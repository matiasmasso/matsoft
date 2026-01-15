namespace Identity.Controllers;

using Identity.Data;
using Identity.Domain.Entities;
using Identity.DTO;
using Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("auth")]
public class EmailVerificationController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _db;
    private readonly IEmailService _emailService; // your email sender

    public EmailVerificationController(
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext db,
        IEmailService emailService)
    {
        _userManager = userManager;
        _db = db;
        _emailService = emailService;
    }

    // ------------------------------------------------------------
    // POST /auth/request-email-verification
    // Sends a verification email to the user
    // ------------------------------------------------------------
    [HttpPost("request-email-verification")]
    public async Task<IActionResult> RequestEmailVerification([FromBody] RequestEmailVerificationRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Ok(); // Do NOT reveal user existence

        if (user.EmailConfirmed)
            return Ok(); // Already confirmed

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = Uri.EscapeDataString(token);

        var verificationUrl = $"{request.VerificationUrl}?userId={user.Id}&token={encodedToken}";

        await _emailService.SendEmailVerificationAsync(user.Email, verificationUrl);

        return Ok();
    }

    // ------------------------------------------------------------
    // POST /auth/confirm-email
    // Confirms the user's email using token + userId
    // ------------------------------------------------------------
    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            return BadRequest("Invalid user");

        var result = await _userManager.ConfirmEmailAsync(user, request.Token);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok("Email confirmed successfully");
    }

    // ------------------------------------------------------------
    // POST /auth/resend-verification
    // Resends the verification email
    // ------------------------------------------------------------
    [HttpPost("resend-verification")]
    public async Task<IActionResult> ResendVerification([FromBody] ResendVerificationRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Ok();

        if (user.EmailConfirmed)
            return Ok();

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = Uri.EscapeDataString(token);

        var verificationUrl = $"{request.VerificationUrl}?userId={user.Id}&token={encodedToken}";

        await _emailService.SendEmailVerificationAsync(user.Email, verificationUrl);

        return Ok();
    }
}
