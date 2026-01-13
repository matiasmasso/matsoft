using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;
using System;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }


        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            try { return Ok(_userService.Find(guid)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpPost("FromHash")]
        public IActionResult FromHash([FromBody] string hash)
        {
            try { 
                UserModel? user = _userService.FromHash(hash);
                return Ok(user); 
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpPost()]
        public IActionResult Update([FromBody] UserModel model)
        {
            try
            {
                var value = _userService.Update(model);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpPost("delete")]
        public IActionResult Delete([FromBody] UserModel value)
        {
            try
            {
               _userService.Delete(value);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


    }


    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }
        [HttpGet]
        public IActionResult All()
        {
            try { return Ok(_usersService.All()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


}
