using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocTargetController : ControllerBase
    {
        private readonly DocTargetService _docTargetService;

        public DocTargetController(DocTargetService docTargetService)
        {
            _docTargetService = docTargetService;
        }

        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            try { return Ok(_docTargetService.Find(guid)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpPost()]
        public IActionResult Update([FromBody] DocTargetModel model)
        {
            try
            {
                var value = _docTargetService.Update(model);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var value = _docTargetService.Delete(guid);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


    [ApiController]
    [Route("[controller]")]
    public class DocTargetsController : ControllerBase
    {
        private DocTargetsService _docTargetsService { get; }

        public DocTargetsController(DocTargetsService docTargetsService)
        {
            _docTargetsService = docTargetsService;
        }

        [HttpGet]
        public IActionResult All()
        {
            try { return Ok(_docTargetsService.All()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpGet("fromDoc/{doc}")]
        public IActionResult FromDoc(Guid doc)
        {
            try { return Ok(_docTargetsService.FromDoc(doc)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpGet("fromTarget/{target}")]
        public IActionResult FromTarget(Guid target)
        {
            try { return Ok(_docTargetsService.FromTarget(target)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


}
