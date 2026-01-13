using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Api.Helpers;
using Newtonsoft.Json;
using System.Runtime.Versioning;

namespace Api.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class ImmobleController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult Fetch(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = ImmobleService.Fetch(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost]
        public IActionResult Update([FromBody] ImmobleModel model)
        {
            IActionResult retval;
            try
            {
                var value = ImmobleService.Update(model);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("fromInventariItem/{inventariItem}")]
        public IActionResult FromInventariItem(Guid inventariItem)
        {
            IActionResult retval;
            try
            {
                var value = ImmobleService.FromInventariItem(inventariItem);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpGet("InventariItem/{guid}")]
        public IActionResult InventariItem(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = ImmobleService.InventariItem(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost("InventariItem")]
        public IActionResult UpdateInventariItem([FromBody] ImmobleModel.InventariItem model)
        {
            IActionResult retval;
            try
            {
                var value = ImmobleService.UpdateItem(model);
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
    public class ImmoblesController : ControllerBase
    {

        [HttpGet]
        public IActionResult Fetch()
        {
            IActionResult retval;
            try
            {
                var values = ImmoblesService.Fetch();
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