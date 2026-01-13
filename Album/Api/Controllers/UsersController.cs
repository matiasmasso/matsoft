using Api.Entities;
using Api.Services;
using DTO;
using DTO.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;
        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllValues()
        {
            try
            {
                var values = _usersService.GetAllValues();
                return Ok(values);

            }
            catch (Exception ex)
            {
                return BadRequest($"error on Api.GetAllValues: {ex.Message}");
            }
        }


        [HttpGet("{guid}")]
        public IActionResult GetValue(Guid guid)
        {
            try
            {
                var values = _usersService.GetValue(guid);
                return Ok(values);

            }
            catch (Exception ex)
            {
                return BadRequest($"error on Api.GetValue: {ex.Message}");
            }
        }



        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update([FromBody] UserModel value)
        {
            if (_usersService.IsDuplicated(value))
                return BadRequest("Email already exists");

            _usersService.Update(value);
            return Ok();
        }
    }
}
