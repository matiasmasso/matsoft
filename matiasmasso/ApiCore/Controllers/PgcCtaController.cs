using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using System;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PgcCtasController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetValues()
        {
            IActionResult retval;
            try
            {
                var values = PgcCtasService.GetValues();
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("Saldos")]
        public IActionResult Saldos([FromBody] PgcCtaModel.Extracte request)
        {
            IActionResult retval;
            try
            {
                var values = PgcCtasService.Saldos(request);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("Saldos/{emp}/{cta}/{contact?}")]
        public IActionResult Saldos(int emp, Guid cta, Guid? contact = null)
        {
            IActionResult retval;
            try
            {
                var values = PgcCtasService.Saldos(emp,cta,contact);
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
