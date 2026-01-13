using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using System;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StaffController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult Fetch(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = StaffService.Find(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost]
        public IActionResult Update([FromBody] StaffModel model)
        {
            IActionResult retval;
            try
            {
                var value = StaffService.Update(model);
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
                var value = StaffService.Delete(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpGet("nominas")]
        public IActionResult UserNominas()
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) 
                retval = Ok(NominasService.All());
                else
                    retval = Ok(NominasService.All(user));

            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("nominas/{staff}")]
        public IActionResult Nominas(Guid staff)
        {
            IActionResult retval;
            try
            {
                var value = NominasService.All(staff);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpGet("Positions")]
        public IActionResult Positions()
        {
            IActionResult retval;
            try
            {
                var values = StaffService.Positions();
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("JornadasLaborals/{staffGuid?}")]
        public IActionResult JornadasLaborals(Guid? staffGuid = null)
        {
            IActionResult retval;
            try
            {
                List<JornadaLaboralModel> values = new();
                if (staffGuid == null)
                {
                    var user = HttpHelper.User(Request);
                    if (user == null) throw new Exception("User unknown");
                    values = StaffService.JornadasLaborals(user);
                }
                else
                {
                    var staff = new StaffModel((Guid)staffGuid);
                    values = StaffService.JornadasLaborals(staff);
                }
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("JornadasLaborals")]
        public IActionResult GetAllJornadas()
        {
            IActionResult retval;
            try
            {
                List<JornadaLaboralModel> values = new();
                values = JornadasLaboralsService.All();
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpGet("nomina/pdf/{nomina}")]
        public IActionResult NominaPdf(Guid nomina)
        {
            IActionResult retval;
            try
            {
                var docfile = CcaService.Docfile(nomina);
                return docfile?.Document?.Data == null ? new NotFoundResult() : new FileContentResult(docfile.Document.Data, docfile.Document.ContentType());
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

    }

}

[ApiController]
[Route("[controller]")]
public class StaffsController : ControllerBase
{

    [HttpGet]
    public IActionResult Fetch()
    {
        IActionResult retval;
        try
        {
            var values = StaffsService.All();
            retval = Ok(values);
        }
        catch (Exception ex)
        {
            retval = BadRequest(ex.ProblemDetails());
        }
        return retval;
    }


    [HttpGet("transfers/pending")]
    public IActionResult PendingTransfers()
    {
        IActionResult retval;
        try
        {
            var values = StaffsService.PendingTransfers();
            retval = Ok(values);
        }
        catch (Exception ex)
        {
            retval = BadRequest(ex.ProblemDetails());
        }
        return retval;
    }


}

