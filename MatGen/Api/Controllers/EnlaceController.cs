using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnlaceController : ControllerBase
    {
        private readonly EnlaceService _enlaceService;
        public EnlaceController(EnlaceService enlaceService)
        {
            _enlaceService = enlaceService;
        }


        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            try { return Ok(_enlaceService.Find(guid)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpPost()]
        public IActionResult Update([FromBody] EnlaceModel model)
        {
            try
            {
                var value = _enlaceService.Update(model);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var value = _enlaceService.Delete(guid);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


 
    [ApiController]
    [Route("[controller]")]
    public class EnlacesController : ControllerBase
    {
        private readonly EnlacesService _enlacesService;

        public EnlacesController(EnlacesService enlacesService)
        {
            _enlacesService = enlacesService;
        }

        [HttpGet]
        public IActionResult All()
        {
            try { return Ok(value: _enlacesService.All()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }
}
