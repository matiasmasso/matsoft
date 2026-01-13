using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TestController : ControllerBase
    {
        [HttpGet("HelloWorld")]
        public IActionResult HelloWorld()
        {
            //IActionResult retval

            try
            {
                //throw new Exception("this is my custom exception message");
                return Ok("Hello World!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ProblemDetails());
            }
        }


        [HttpGet]
        public IActionResult Test()
        {
            //IActionResult retval

            try
            {
                //throw new Exception("this is my custom exception message");
                return Ok("Hello World!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ProblemDetails());
            }
        }
    }
}
