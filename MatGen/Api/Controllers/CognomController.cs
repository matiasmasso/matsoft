using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CognomController : ControllerBase
    {
        private readonly CognomService _cognomService;

        public CognomController(CognomService cognomService)
        {
            _cognomService = cognomService;
        }

        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            try { return Ok(_cognomService.Find(guid)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpPost()]
        public IActionResult Update([FromBody] CognomModel model)
        {
            try { 
                var value = _cognomService.Update(model);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var value = _cognomService.Delete(guid);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


    [ApiController]
    [Route("[controller]")]
    public class CognomsController : ControllerBase
    {
        private readonly CognomsService _cognomsService;

        public CognomsController(CognomsService cognomsService)
        {
            _cognomsService = cognomsService;
        }

        [HttpGet]
        public IActionResult All()
        {
            try { return Ok(_cognomsService.All()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


}
