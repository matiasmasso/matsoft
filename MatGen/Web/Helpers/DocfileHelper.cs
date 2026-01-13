using DTO;
using ImageMagick;
using Microsoft.AspNetCore.Components.Forms;
using static DTO.Media;

namespace Web.Helpers
{
    public class DocfileHelper
    {

        public static DocfileModel CreateDocfile(string filename, Media media, int thumbnailWidth = DocfileModel.THUMB_WIDTH, int thumbnailHeight = DocfileModel.THUMB_HEIGHT)
        {
            DocfileModel retval = new DocfileModel();
            retval.Hash = DTO.Helpers.AsinHelper.GenerateRandomASIN();
            retval.Sha256 = DTO.Helpers.CryptoHelper.Sha256(media.Data!);
            retval.Document = media;
            retval.Size = media.Data?.Length;
            retval.Filename = filename;
            retval.FchCreated = DateTime.Now;
            retval.Fch = retval.FchCreated;


            switch (media.Mime)
            {
                case Media.MimeCods.Pdf:
                    using (MagickImageCollection images = new MagickImageCollection())
                    {
                        var defines = new ImageMagick.Formats.PdfReadDefines() { Interpolate = true };
                        var settings = new MagickReadSettings();
                        settings.Density = new Density(300);
                        settings.SetDefines(defines);
                        settings.FrameIndex = 0;
                        settings.FrameCount = 1;

                        images.Ping(media.Data!);
                        retval.Pags = images.Count;

                        images.Read(media.Data!, settings);
                        using (var image = images.First())
                        {
                            image.Format = MagickFormat.Png;
                            image.Density = new Density(600);
                            retval.Width = (int?)image.Width;
                            retval.Height = (int?)image.Height;
                            var landscape = retval.Width / retval.Height > thumbnailWidth / thumbnailHeight;
                            int resizeWidth = landscape ? thumbnailWidth : (int)retval.Width * thumbnailHeight / (int)retval.Height;
                            int resizeHeight = landscape ? (int)retval.Height * thumbnailWidth / (int)retval.Width : thumbnailHeight;
                            int frameWidth = (thumbnailWidth - resizeWidth) / 2;
                            int frameHeight = (thumbnailHeight - resizeHeight) / 2;
                            image.Resize((uint)resizeWidth, (uint)resizeHeight);
                            retval.Thumbnail = new Media(Media.MimeCods.Png, image.ToByteArray());
                        }
                    }

                    break;
            }
            return retval;

        }
    }
}
