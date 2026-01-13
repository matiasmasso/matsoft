using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CertificatIrpfController : ControllerBase
    {

        [HttpGet("pdf/{cert}")]
        public IActionResult Pdf(Guid cert)
        {
            IActionResult retval;
            try
            {
                var docfile = CertificatIrpfService.Docfile(cert);
                return docfile is null ? new NotFoundResult() : new FileContentResult(docfile!.Document!, docfile.ContentType());
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }
    }


    [ApiController]
    [Route("[controller]")]
    public class CertificatsIrpfController : ControllerBase
    {

        [HttpGet("{guid?}")]
        public IActionResult CertificatsIrpfFromContact(Guid? guid = null)
        {
            IActionResult retval;
            try
            {
                if (guid == null)
                {
                    var user = HttpHelper.User(Request);
                    if (user is null) throw new Exception("user is null");
                    retval = Ok(CertificatsIrpfService.FromUser(user));
                } else
                    retval =Ok( CertificatsIrpfService.FromContact((Guid)guid!));
                return retval;
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



    }
}
