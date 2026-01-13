using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocRelController : ControllerBase
    {
        private readonly DocRelService _docRelService;

        public DocRelController(DocRelService docRelService)
        {
            _docRelService = docRelService;
        }

        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            try { return Ok(_docRelService.Find(guid)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpPost()]
        public IActionResult Update([FromBody] DocRelModel model)
        {
            try
            {
                var value = _docRelService.Update(model);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var value = _docRelService.Delete(guid);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


    [ApiController]
    [Route("[controller]")]
    public class DocRelsController : ControllerBase
    {
        private readonly DocRelsService _docRelsService;

        public DocRelsController(DocRelsService docRelsService)
        {
            _docRelsService = docRelsService;
        }

        [HttpGet]
        public IActionResult All()
        {
            try { return Ok(_docRelsService.All()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


}
