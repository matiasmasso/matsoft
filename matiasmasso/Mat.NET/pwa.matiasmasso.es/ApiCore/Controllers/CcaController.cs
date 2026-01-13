using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Api.Helpers;
using Newtonsoft.Json;
using System.Runtime.Versioning;
//using Api.Entities;
using iText.StyledXmlParser.Jsoup.Helper;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CcaController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult Fetch(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = CcaService.Find(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpGet("pdf/{cca}")]
        public IActionResult Pdf(Guid cca)
        {
            IActionResult retval;
            try
            {
                var docfile = CcaService.Docfile(cca);
                return docfile?.Document?.Data == null ? new NotFoundResult() : new FileContentResult(docfile.Document.Data, docfile.Document.ContentType());
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("thumbnail/{cca}")]
        public IActionResult Thumbnail(Guid cca)
        {
            IActionResult retval;
            try
            {
                Media? value = CcaService.Thumbnail(cca);
                return value?.Data == null ? new NotFoundResult() : new FileContentResult(value.Data, value.ContentType());
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost, SupportedOSPlatform("windows")]
        public async Task<IActionResult> UpdateAsync()
        {
            IActionResult retval;
            try
            {
                var form = await Request.ReadFormAsync();
                var data = form["Data"];
                var model = JsonConvert.DeserializeObject<CcaModel>(data);
                if (model != null)
                {
                    if (form.Files.Count > 0)
                        await FileUploadHelper.LoadFileStream(model.Docfile, form.Files);

                    CcaService.Update(model);
                    retval = Ok(true);
                }
                else
                    retval = BadRequest(new Exception("null model"));

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
                CcaService.Delete(guid);
                retval = Ok(true);
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
    public class CcasController : ControllerBase
    {
        [HttpGet("compact/{emp}/{yea}")]
        public IActionResult Compact(EmpModel.EmpIds emp, int yea)
        {
            IActionResult retval;
            try
            {
                var values = CcasService.GetValues(emp, yea);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;

        }

        [HttpGet("{emp}/{yea}")]
        public IActionResult GetValues(EmpModel.EmpIds emp, int yea)
        {
            IActionResult retval;
            try
            {
                var exercici = new ExerciciModel { Emp = emp, Year = yea };
                var values = CcasService.FromExercise(exercici);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;

        }


        [HttpPost("extracte")]
        public IActionResult Extracte([FromBody] PgcCtaModel.Extracte extracte)
        {
            IActionResult retval;
            try
            {
                var value = CcasService.Extracte(extracte);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;

        }

        [HttpGet("LlibreDiari/{emp}/{year}")]
        public IActionResult LlibreDiari(EmpModel.EmpIds emp, int year)
        {
            IActionResult retval;
            try
            {
                var exercici = new ExerciciModel { Emp = emp, Year = year };
                var value = CcasService.LlibreDiari(exercici);
                var mimeType = MimeHelper.ContentType(Media.MimeCods.Xlsx);
                retval = new FileContentResult(value, mimeType);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("LlibreFacturesEmeses/{emp}/{year}")]
        public IActionResult LlibreFacturesEmeses(EmpModel.EmpIds emp, int year)
        {
            IActionResult retval;
            try
            {
                var exercici = new ExerciciModel { Emp = emp, Year = year };
                var value = CcasService.LlibreFacturesEmeses(exercici);
                var mimeType = MimeHelper.ContentType(Media.MimeCods.Xlsx);
                retval = new FileContentResult(value, mimeType);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("LlibreFacturesRebudes/{emp}/{year}")]
        public IActionResult LlibreFacturesRebudes(EmpModel.EmpIds emp, int year)
        {
            IActionResult retval;
            try
            {
                var exercici = new ExerciciModel { Emp = emp, Year = year };
                var value = CcasService.LlibreFacturesRebudes(exercici);
                var mimeType = MimeHelper.ContentType(Media.MimeCods.Xlsx);
                retval = new FileContentResult(value, mimeType);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }
    }
}
