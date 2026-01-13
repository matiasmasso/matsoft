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
                var value = ContactService.GetValue(guid);
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


        [HttpPost()]
        public IActionResult Update([FromBody] ContactModel model)
        {
            IActionResult retval;
            try
            {
                var value = ContactService.Update(model);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            IActionResult retval;
            try
            {
                ContactService.Delete(guid);
                retval = Ok(true);
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

        [HttpGet]
        public IActionResult Fetch()
        {
            IActionResult retval;
            try
            {
                var values = ContactsService.Fetch();
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("all")]
        public IActionResult FetchAll()
        {
            IActionResult retval;
            try
            {
                var values = ContactsService.FetchAll();
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