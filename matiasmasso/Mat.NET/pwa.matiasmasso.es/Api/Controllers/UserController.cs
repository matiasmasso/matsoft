using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Shared;
using Api.Services;
using Api.Entities;
using System.Security.Policy;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        public IConfiguration _configuration;



        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IConfiguration config)
        {
            _logger = logger;
            _configuration = config;
        }



        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            IActionResult retval;
            try
            {
                var user = Cache.Users.FirstOrDefault(x => x.Guid == guid);
                if (user == null)
                {
                    user = UserService.Find(guid);
                    if (user != null) Cache.Users.Add(user);
                }
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
                Cache.AddUserIfMissing(user);
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
                Cache.AddUserIfMissing(user);
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

        [HttpPost("ResetPwd/{guid}")] // ToDeprecate against ResetPwd
        public IActionResult PwdReset(Guid guid, [FromBody] string hash)
        {
            IActionResult retval;
            try
            {
                Cache.Users.RemoveAll(x => x.Guid == guid);
                var value = UserService.ResetPwd(guid, hash);
                retval = Ok(value);
            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("PwdReset")] // ToDeprecate against ResetPwd
        public IActionResult PwdReset([FromBody] LoginRequestDTO payload)
        {
            IActionResult retval;
            try
            {
                UserModel? user = UserService.PwdReset(payload);
                if (user != null)
                {
                    Cache.Users.RemoveAll(x => x.Guid == user.Guid);
                    Cache.Users.Add(user);
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
    public class UsersController : ControllerBase
    {

        public IConfiguration _configuration;



        private readonly ILogger<UserController> _logger;

        public UsersController(ILogger<UserController> logger, IConfiguration config)
        {
            _logger = logger;
            _configuration = config;
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