using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocCodController : ControllerBase
    {

        private readonly DocCodService _docCodService;

        public DocCodController(DocCodService docCodService)
        {
            _docCodService = docCodService;
        }


        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            try { return Ok(_docCodService.Find(guid)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpPost()]
        public IActionResult Update([FromBody] DocCodModel model)
        {
            try
            {
                var value = _docCodService.Update(model);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var value = _docCodService.Delete(guid);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


    [ApiController]
    [Route("[controller]")]
    public class DocCodsController : ControllerBase
    {
        private readonly DocCodsService _docCodsService;

        public DocCodsController(DocCodsService docCodsService)
        {
            _docCodsService = docCodsService;
        }

        [HttpGet]
        public IActionResult All()
        {
            try { return Ok(_docCodsService.All()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


}
