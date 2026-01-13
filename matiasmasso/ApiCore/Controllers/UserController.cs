using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Shared;
using Api.Services;
using Api.Entities;
using System.Security.Policy;
using Api.Extensions;
using System;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "Users")]
    public class UserController : ControllerBase
    {

        IOutputCacheStore cache;
        public IConfiguration _configuration;

        public UserController(IOutputCacheStore cache, IConfiguration config)
        {
            this.cache = cache;
            _configuration = config;
        }


        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            IActionResult retval;
            try
            {
                var user = UserService.Find(guid);
                retval = Ok(user);
            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO request)
        {
            IActionResult retval;
            try
            {
                var user = UserService.Login(request);
                retval = Ok(user);
            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("fromHash/{emp}")]
        public IActionResult FromHash(int emp, [FromBody] string hash)
        {
            IActionResult retval;
            try
            {
                var user = UserService.FromHash(emp, hash);
                retval = Ok(user);
            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost("EmailExists")] // TO DEPRECATE against FromEmailAddress
        public IActionResult EmailExists([FromBody] string emailAddress)
        {
            IActionResult retval;
            try
            {
                var value = UserService.FromEmailAddress(1, emailAddress);
                retval = Ok(value != null);
            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        /// <summary>
        /// Checks if an email address is already registered
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="emailAddress"></param>
        /// <returns>user Guid</returns>
        [HttpPost("FromEmailAddress/{emp}")] 
        public IActionResult FromEmailAddress(int emp, [FromBody] string emailAddress)
        {
            IActionResult retval;
            try
            {
                var value = UserService.FromEmailAddress(emp,emailAddress);
                retval = Ok(value);
            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost("checkPwd/{emp}")]
        public IActionResult CheckPwd(int emp, [FromBody] string hash)
        {
            try
            {
                var value = UserService.FromHash(emp, hash); 
                return Ok(value);

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost()]
        public IActionResult Update([FromBody] UserModel model)
        {
            IActionResult retval;
            try
            {
                var value = UserService.Update(model);
                retval = Ok(value);
            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] UserModel user)
        {
            IActionResult retval;
            try
            {
                if(UserService.Update(user))
                {
                    await cache.Clear(OutputCacheExtensions.Tags.Users);
                    var value = TokenService.Token(_configuration, (int)user.Emp , user.Hash!);
                    retval = Ok(value);
                }
                else
                    retval = BadRequest( new ProblemDetails {Title="Unable to create new user" });
            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("ResetPwd/{guid}")] 
        public async Task<IActionResult> PwdReset(Guid guid, [FromBody] string hash)
        {
            IActionResult retval;
            try
            {
                string? value = null;
                if (UserService.ResetPwd(guid, hash)){
                    value = TokenService.Token(_configuration, (int)EmpModel.EmpIds.MatiasMasso, hash);
                await cache.Clear(OutputCacheExtensions.Tags.Users);
                }
                retval = Ok(value);
            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("PwdReset")] // ToDeprecate against ResetPwd
        public async Task<IActionResult> PwdReset([FromBody] LoginRequestDTO payload)
        {
            IActionResult retval;
            try
            {
                UserModel? user = UserService.PwdReset(payload);
                if (user != null)
                {
                    await cache.Clear(OutputCacheExtensions.Tags.Users);
                }
                retval = Ok(user);
            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

    }


    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "Users")]
    public class UsersController : ControllerBase
    {


        [HttpGet("professionals/{emp}")]
        public IActionResult Professionals(int emp)
        {
            IActionResult retval;
            try
            {
                var values = UsersService.Professionals(emp);
                retval = Ok(values);
            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost("search/{emp}")]
        public IActionResult Search(int emp, [FromBody] string searchTerm)
        {
            IActionResult retval;
            try
            {
                var values = UsersService.Search(emp, searchTerm);
                retval = Ok(values);
            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



    }
}