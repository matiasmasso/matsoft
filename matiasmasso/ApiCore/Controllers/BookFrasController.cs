using Api.Extensions;
using Api.Services;
using Api.Services.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookFraController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult GetValues(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = BookfraService.GetValue(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost()]
        public IActionResult Update([FromBody] BookfraModel model)
        {
            IActionResult retval;
            try
            {
                var value = BookfraService.Update(model);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = BookfraService.Delete(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }
    }


    [Route("[controller]")]
    [ApiController]
    public class BookFrasController : ControllerBase
    {

        private readonly IExcelBookfrasService _excelBookfrasService;

        public BookFrasController(IExcelBookfrasService excelBookfrasService)
        {
            _excelBookfrasService = excelBookfrasService;
        }

        [HttpGet("{emp}/{year}")]
        public IActionResult GetValues(int emp, int year)
        {
            IActionResult retval;
            try
            {
                var value = BookfrasService.GetValues(emp, year);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("excel/{emp}/{year}")]
        public IActionResult GetExcel(EmpModel.EmpIds emp, int year)
        {
            var request = HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            var bytes = _excelBookfrasService.Excel(emp, year, baseUrl);

            return File(bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "IVA.xlsx");
        }

        [HttpGet("missingValues/{emp}/{year}")]
        public IActionResult MissingValues(int emp, int year)
        {
            IActionResult retval;
            try
            {
                var value = BookfrasService.MissingValues(emp, year);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }
    }
}
