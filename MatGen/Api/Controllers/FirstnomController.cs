using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FirstnomController : ControllerBase
    {
        private readonly FirstnomService _firstnomService;

        public FirstnomController(FirstnomService firstnomService)
        {
            _firstnomService = firstnomService;
        }

        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            try { return Ok(_firstnomService.Find(guid)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpPost()]
        public IActionResult Update([FromBody] FirstnomModel model)
        {
            try
            {
                var value = _firstnomService.Update(model);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var value = _firstnomService.Delete(guid);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


    [ApiController]
    [Route("[controller]")]
    public class FirstnomsController : ControllerBase
    {
        private readonly FirstnomsService _firstnomsService;

        public FirstnomsController(FirstnomsService firstnomsService)
        {
            _firstnomsService = firstnomsService;
        }

        [HttpGet]
        public IActionResult All()
        {
            try { return Ok(_firstnomsService.All()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


}
