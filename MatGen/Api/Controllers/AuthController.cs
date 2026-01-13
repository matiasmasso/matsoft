using Api.Entities;
using Api.Services;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace Api.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserService _userService;


        public AuthController(IConfiguration config, UserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [HttpPost("validate")]
        public IActionResult Validate([FromBody] DTO.LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Hash))
                return BadRequest(new { message = "Login and password hash is required." });

            var user = _userService.FromHash(request.Hash);
            if (user is null)
            {
                return Unauthorized(new LoginResponse
                {
                    IsValid = false,
                    Token = null
                });
            }
            else
            {
                var token = GenerateJwtToken(user.EmailAddress!, user.Rol.ToString(), user.Guid);
                return Ok(new LoginResponse
                {
                    IsValid = true,
                    Token = token
                });
            }

       }

        private string GenerateJwtToken(string username, string role, Guid userGuid)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userGuid.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userGuid.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

}
