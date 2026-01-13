using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitaController : ControllerBase
    {
        private readonly CitaService _citaService;

        public CitaController(CitaService citaService)
        {
            _citaService = citaService;
        }

        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            try { return Ok(_citaService.Find(guid)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpPost()]
        public IActionResult Update([FromBody] CitaModel model)
        {
            try
            {
                var value = _citaService.Update(model);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var value = _citaService.Delete(guid);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


    [ApiController]
    [Route("[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly CitasService _citasService;

        public CitasController(CitasService citasService)
        {
            _citasService = citasService;
        }

        [HttpGet]
        public IActionResult All()
        {
            try { return Ok(_citasService.All()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


}
