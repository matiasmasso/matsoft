using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseOrderController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult GetValue(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = PurchaseOrderService.GetValue(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost]
        public IActionResult Update([FromBody] PurchaseOrderModel value)
        {
            IActionResult retval;
            try
            {
                PurchaseOrderService.Update(value);
                retval = Ok();
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpGet("resources/{user}")]
        public IActionResult Resources(Guid user)
        {
            IActionResult retval;
            try
            {
                var value = PurchaseOrderService.Resources(user);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("resources/fromContact/{contact}")]
        public IActionResult ResourcesFromContact(Guid contact)
        {
            IActionResult retval;
            try
            {
                var value = PurchaseOrderService.CustomerResources(contact);
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
    public class PurchaseOrdersController : ControllerBase
    {

        [HttpGet("")]
        //public IActionResult FromUser()
        //{
        //    IActionResult retval;
        //    try
        //    {
        //        //var values = PurchaseOrderListService.FromUser(user);
        //        //retval = Ok(values);
        //    }
        //    catch (Exception ex)
        //    {
        //        retval = BadRequest(ex.ProblemDetails());
        //    }
        //    return retval;
        //}

        [HttpGet("{contact}")]
        public IActionResult FromContact(Guid contact)
        {
            IActionResult retval;
            try
            {
                var values = PurchaseOrdersService.FromContact(contact);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("{emp}/{year}")]
        public IActionResult GetValues(int emp, int year)
        {
            IActionResult retval;
            try
            {
                var values = PurchaseOrdersService.GetValues(emp, year);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost]
        public IActionResult Update([FromBody] List<PurchaseOrderModel> values)
        {
            IActionResult retval;
            try
            {
                PurchaseOrdersService.Update(values);
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