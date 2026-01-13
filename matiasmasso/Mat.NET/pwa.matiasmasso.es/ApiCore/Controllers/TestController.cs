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


        [HttpGet("magasalfa")]
        public IActionResult Magasalfa()
        {
            try
            {
                var fch = new DateTime(2024, 12, 1);
                var invoice = new Services.Magasalfa(18, fch);
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




    }
}
