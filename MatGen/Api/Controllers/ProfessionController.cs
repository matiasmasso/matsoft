using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfessionController : ControllerBase
    {
        private readonly ProfessionService _professionService;

    public ProfessionController(ProfessionService professionService)
        {
            _professionService = professionService;
        }


        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            try { return Ok(_professionService.Find(guid)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpPost()]
        public IActionResult Update([FromBody] ProfessionModel model)
        {
            try
            {
                var value = _professionService.Update(model);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var value = _professionService.Delete(guid);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


    [ApiController]
    [Route("[controller]")]
    public class ProfessionsController : ControllerBase
    {
        private readonly ProfessionsService _professionsService;

        public ProfessionsController(ProfessionsService professionsService)
        {
            _professionsService = professionsService;
        }

        [HttpGet]
        public IActionResult All()
        {
            try { return Ok(_professionsService.All()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


}
