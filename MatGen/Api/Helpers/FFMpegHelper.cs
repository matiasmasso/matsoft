using DTO;
using FFMpegCore;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Api.Helpers
{
    public class FFMpegHelper
    {
        private readonly IWebHostEnvironment _env;
        public FFMpegHelper(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task SaveVideoThumbnail(AlbumModel.Item item)
        {
            var seconds = 2;
            // Extract thumbnail at given timestamp
            await FFMpegArguments
     .FromFileInput(item.FullPath) // just the path
     .OutputToFile(item.TempPath, overwrite: true, o => o
         .Seek(TimeSpan.FromSeconds(seconds))   // move seek here
         .WithVideoCodec("mjpeg")
         .WithFrameOutputCount(1)
         .ForceFormat("image2")
         .WithVideoFilters(f => f.Scale(200, -1)))
     .ProcessAsynchronously();

            var filmstripPath = Path.Combine(_env.WebRootPath, "img", "FilmStrip.jpg");
            await DecorateWithFilmstrip(item.TempPath, filmstripPath, item.ThumbPath);

            if (System.IO.File.Exists(item.TempPath))
                System.IO.File.Delete(item.TempPath);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        private static async Task DecorateWithFilmstrip(string thumbnailPath, string filmstripPath, string outputPath)
        {
            using var baseImage = Image.FromFile(thumbnailPath);
            using var filmstrip = Image.FromFile(filmstripPath);

            // Escalamos el filmstrip proporcionalmente a la altura del thumbnail
            float scale = (float)baseImage.Height / filmstrip.Height;
            int newWidth = (int)(filmstrip.Width * scale);
            int newHeight = baseImage.Height;

            using var scaledFilmstrip = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(scaledFilmstrip))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(filmstrip, 0, 0, newWidth, newHeight);
            }

            using var graphics = Graphics.FromImage(baseImage);
            graphics.CompositingMode = CompositingMode.SourceOver;

            // Overlay a la izquierda
            graphics.DrawImage(scaledFilmstrip, new Point(0, 0));

            // Overlay a la derecha
            graphics.DrawImage(scaledFilmstrip, new Point(baseImage.Width - newWidth, 0));

            baseImage.Save(outputPath);
        }
    }
}
