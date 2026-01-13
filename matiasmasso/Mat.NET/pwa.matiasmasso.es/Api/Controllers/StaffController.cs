using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

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
                if (user == null) throw new Exception("User unknown");
                var value = NominasService.All(user);
                retval = Ok(value);
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



        [HttpGet("certificatsIrpf")]
        public IActionResult certificatsIrpf()
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("User unknown");
                var value = CertificatsIrpfService.FromUser(user);
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


        [HttpGet("JornadasLaborals")]
        public IActionResult JornadasLaborals()
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("User unknown");
                var value = StaffService.JornadasLaborals(user);
                retval = Ok(value);
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
                return docfile is null ? new NotFoundResult() : new FileContentResult(docfile!.Document!, docfile.ContentType());
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

    [HttpGet("{emp}")]
    public IActionResult Fetch(int emp)
    {
        IActionResult retval;
        try
        {
            var values = StaffsService.All(emp);
            retval = Ok(values);
        }
        catch (Exception ex)
        {
            retval = BadRequest(ex.ProblemDetails());
        }
        return retval;
    }


}

