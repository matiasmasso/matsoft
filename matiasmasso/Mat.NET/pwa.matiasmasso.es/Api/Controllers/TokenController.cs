using Api.Entities;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DTO;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        public TokenController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpPost("{emp}")]
        public IActionResult Post(int emp, [FromBody] string hash)
        {
            try
            {
                string? value = TokenService.Token(_configuration, emp, hash);
                return Ok(value);

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
