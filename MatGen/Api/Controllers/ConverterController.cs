using Api.Extensions;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ImageMagick;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConverterController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Heic2Jpg()
        {
            //var rootFolder = "C:\\Public\\Fotos";
            var rootFolder = "Z:\\Fotos";
            var outputRoot = rootFolder;

            foreach (var heicPath in Directory.EnumerateFiles(
                         rootFolder, "*.heic", SearchOption.AllDirectories))
            {
                var relPath = Path.GetRelativePath(rootFolder, heicPath);
                var outDir = Path.Combine(outputRoot!, Path.GetDirectoryName(relPath) ?? "");
                Directory.CreateDirectory(outDir);

                string outFile = Path.Combine(
                    outDir,
                    Path.GetFileNameWithoutExtension(heicPath) + ".jpg");

                using var image = new MagickImage(heicPath);
                image.Format = MagickFormat.Jpeg;
                image.Quality = 100;              // optional: JPEG quality 0–100
                image.Write(outFile);
                Console.WriteLine($"Converted: {heicPath} -> {outFile}");
               
            }
            return Ok();
        }
    }
}
