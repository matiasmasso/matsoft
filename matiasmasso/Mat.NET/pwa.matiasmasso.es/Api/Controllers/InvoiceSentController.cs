using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceSentController : ControllerBase
    {
        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = InvoiceSentService.Find(guid);
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
    public class InvoiceSentListController : ControllerBase
    {

        [HttpGet("")]
        public IActionResult FromUser()
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("User unknown");
                var values = InvoiceSentListService.FromUser(user);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("{contact}")]
        public IActionResult FromCustomer(Guid contact)
        {
            IActionResult retval;
            try
            {
                var values = InvoiceSentListService.FromCustomer(contact);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("{emp}/{year}")]
        public IActionResult FromEmpYear(int emp, int year)
        {
            IActionResult retval;
            try
            {
                var values = InvoiceSentListService.FromEmpYear(emp, year);
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
