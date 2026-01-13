using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveryController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult Fetch(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = DeliveryService.Fetch(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


    }





    [ApiController]
    [Route("[controller]")]
    public class DeliveryListController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult FromUser()
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("User unknown");
                var values = DeliveriesService.FromUser(user);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("{contact}")]
        public IActionResult Fetch(Guid contact)
        {
            IActionResult retval;
            try
            {
                var values = DeliveriesService.Fetch(contact);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("test")]
        public IActionResult Test()
        {
            IActionResult retval;
            try
            {
                var values = DeliveriesService.Fetch(new Guid("3500FAB4-C142-4A95-A25B-D5667A848CB4"));
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("{emp}/{year}")]
        public IActionResult FromYear(int emp, int year)
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("User unknown");
                var values = DeliveriesService.FromYear(emp, year);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

    }
}
