using Api.Services;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NuGet.Common;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            try
            {
                var tokenResponse = _auth.Login(request.Email!, request.Password!);

                if (tokenResponse == null)
                    return Unauthorized();

                return Ok(tokenResponse);

            }
            catch (Exception ex)
            {
                return BadRequest($"Error en login Api {ex.Message}");
            }
        }

        [HttpPost("refresh")]
        public ActionResult<TokenResponse> Refresh([FromBody] RefreshRequest request)
        {
            try
            {
                var tokens = _auth.TokenRefreshRequest(request);
                return Ok(tokens);
            }
            catch (UnauthorizedAccessException authEx)
            {
                return Unauthorized(authEx.Message);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                return BadRequest("Missing refresh token.");

            try
            {
                await _auth.LogoutAsync(request);
            return Ok();
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("CreateFirstUser")]
        public IActionResult CreateFirstUser(LoginRequest request)
        {
            try
            {
                _auth.CreateFirstUser(request);

                return Ok();

            }
            catch(Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                return BadRequest($"Error en auth Api admin {ex.Message}");
            }
            catch (Exception ex2)
            {
                return BadRequest($"Error en auth Api admin {ex2.Message}");
            }
        }
    }

}
