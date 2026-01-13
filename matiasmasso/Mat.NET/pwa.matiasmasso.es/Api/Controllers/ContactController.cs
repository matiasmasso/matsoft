using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult Fetch(Guid guid)
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("User unknown");
                var lang = HttpHelper.Lang(Request);
                var value = ContactService.ContactDTO(guid, user, lang);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("telefons/{contact}")]
        public IActionResult Telefons(Guid contact)
        {
            IActionResult retval;
            try
            {
                var value = ContactService.Telefons(contact);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("emails/{contact}")]
        public IActionResult Emails(Guid contact)
        {
            IActionResult retval;
            try
            {
                var value = ContactService.Emails(contact);
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
    public class ContactsController : ControllerBase
    {

        [HttpGet("{emp}")]
        public IActionResult Fetch(int emp)
        {
            IActionResult retval;
            try
            {
                var values = ContactsService.Fetch(emp);
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