using ImageMagick;
using Microsoft.AspNetCore.Components.Forms;
using DTO;
using System.Drawing;
using DTO.Helpers;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Versioning;

namespace Shop4moms.Helpers
{
    public class DocfileHelper
    {

        public static DocFileModel CreatePdfDocfile(byte[] bytes, int thumbnailWidth = 350, int thumbnailHeight = 400)
        {
            DocFileModel retval = new DocFileModel();
            retval.Document = new Media(Media.MimeCods.Pdf, bytes);
            retval.Size = bytes.Length;
            retval.Hash = DTO.Helpers.CryptoHelper.HashMD5(bytes);
            retval.FchCreated = DateTime.Now;
            retval.Fch = DateTime.Now;

            using (MagickImageCollection images = new MagickImageCollection())
            {
                MagickReadSettings settings = new MagickReadSettings();
                settings.FrameIndex = 0;
                settings.FrameCount = 1;

                images.Ping(bytes);
                retval.Pags = images.Count;

                var ms = new MemoryStream(bytes);
                images.Read(ms, settings);
                using (var image = images.First())
                {
                    image.Format = MagickFormat.Png;
                    retval.Width = image.Width;
                    retval.Height = image.Height;
                    var landscape = retval.Width / retval.Height > thumbnailWidth / thumbnailHeight;
                    int resizeWidth = landscape ? thumbnailWidth : (int)retval.Width * thumbnailHeight / (int)retval.Height;
                    int resizeHeight = landscape ? (int)retval.Height * thumbnailWidth / (int)retval.Width : thumbnailHeight;
                    int frameWidth = (thumbnailWidth - resizeWidth) / 2;
                    int frameHeight = (thumbnailHeight - resizeHeight) / 2;
                    image.Resize(resizeWidth, resizeHeight);
                    retval.Thumbnail = new Media(Media.MimeCods.Png, image.ToByteArray());
                }
            }
            return retval;
        }

        [SupportedOSPlatform("windows")]
        public static async Task<DocFileModel> CreateDocfileAsync(IBrowserFile file, int thumbnailWidth = 350, int thumbnailHeight = 400)
        {
            var mimeCod = Media.MimeCodFromContentType(file.ContentType);
            Stream stream = file.OpenReadStream(maxAllowedSize: 100000000);
            MemoryStream ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            stream.Close();

            ms.Seek(0, SeekOrigin.Begin);
            var bytes = ms.ToArray();

            DocFileModel retval = new DocFileModel();
            retval.Document = new Media(file.ContentType, bytes);
            retval.Size = bytes.Length;
            retval.Hash = DTO.Helpers.CryptoHelper.HashMD5(bytes);
            retval.Filename = file.Name;
            retval.Nom = file.Name;
            retval.FchCreated = file.LastModified.DateTime;
            retval.Fch = DateTime.Now;

            switch (mimeCod)
            {
                case Media.MimeCods.Jpg:
                case Media.MimeCods.Gif:
                case Media.MimeCods.Png:
                    Image img;
                    using (var ms1 = new MemoryStream(bytes))
                    {
                        img = Image.FromStream(ms1);
                    };
                    retval.Width = img.Width;
                    retval.Height = img.Height;
                    retval.VRes = (int?)img.VerticalResolution;
                    retval.HRes = (int?)img.HorizontalResolution;

                    var thumbnail = DTO.Helpers.ImageHelper.Resize(srcImage: img, width: DocFileModel.THUMB_WIDTH, height: DocFileModel.THUMB_HEIGHT);
                    using (var ms2 = new MemoryStream())
                    {
                        thumbnail.Save(ms2, ImageHelper.ImageFormat(mimeCod));
                        retval.Thumbnail = new Media(mimeCod, ms2.ToArray());
                    }
                    break;

                case Media.MimeCods.Pdf:
                    retval =  CreatePdfDocfile(bytes, thumbnailWidth, thumbnailHeight);
                    break;

                case Media.MimeCods.Mp4:
                case Media.MimeCods.Mov:
                case Media.MimeCods.Mpg:
                    //var thumnailMime = ThumbnailMime("video.png");
                    //retval.Thumbnail = thumnailMime?.Image;
                    //retval.ThumbnailMime = thumnailMime?.Mime ?? Media.MimeCods.NotSet;
                    break;

                default:
                    //var imageMime = ThumbnailMime("cartadeajuste.png");
                    //retval.Thumbnail = imageMime?.Image;
                    //retval.ThumbnailMime = imageMime?.Mime ?? Media.MimeCods.NotSet;
                    break;

            }
            return retval;
        }



    }
}
