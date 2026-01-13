using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using DocumentFormat.OpenXml.Spreadsheet;
using Api.Extensions;
using Api.Helpers;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Geom;
using iText.Layout.Element;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Colors;
using Color = iText.Kernel.Colors.Color;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using Text = iText.Layout.Element.Text;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.InkML;
using System.Runtime.Intrinsics.X86;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Vml;
using iText.IO.Image;
using ImageData = iText.IO.Image.ImageData;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TestController : ControllerBase
    {
        [HttpGet("HelloWorld")]
        public IActionResult HelloWorld()
        {
            //IActionResult retval

            try
            {
                //throw new Exception("this is my custom exception message");
                return Ok("Hello World!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ProblemDetails());
            }
        }


        [HttpGet("magasalfa/{year}/{mes}/{franum}")]
        public IActionResult Magasalfa(int year, int mes, int franum)
        {
            try
            {
                var fch = new DateTime(year, mes, 1);
                var invoice = new Services.Magasalfa(franum, fch);
                byte[] bytes = invoice.ByteArray();
                var ms = new MemoryStream();
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return new FileStreamResult(ms, "application/pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ProblemDetails());
            }

        }

        [HttpGet("UUID/{guid}")]
        public IActionResult UUID(Guid guid)
        {
            var retval = GuidToAsin(guid);
            return Ok("Hello World!");
        }


        public string GuidToAsin(Guid guid)
        {
            var sGuid = guid.ToString().Replace("-", "");
            string uuid = "1234567890ABCDEF1234567890ABCDEF";
            char[] letters = Enumerable.Range(0, uuid.Length / 2)
                .Select(i => (char)Convert.ToInt32(uuid.Substring(i * 2, 2), 16))
                .ToArray();
            string str = string.Join("", letters);
            return str;
        }




    }
}
