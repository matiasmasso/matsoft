using DTO;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace Api.Helpers
{
    [SupportedOSPlatform("windows")]
    public class ImageHelper
    {

        public static Image Resize(Image srcImage, int width, int height)
        {
            var retval = new System.Drawing.Bitmap(width, height);
            int imgWidth = width;
            int imgHeight = srcImage.Height * imgWidth / srcImage.Width;
            if (imgHeight > height)
            {
                imgHeight = height;
                imgWidth = srcImage.Width * imgHeight / srcImage.Height;
            }
            using (var graphics = Graphics.FromImage(retval))
            {
                graphics.Clear(Color.White);
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.DrawImage(srcImage, 0, 0, imgWidth, imgHeight);
            }
            return retval;
        }

        private static Byte[]? JpgBytes(Image? oBitmap)
        {
            Byte[]? retval = null;
            if (oBitmap != null)
            {
                ImageCodecInfo? myImageCodecInfo;
                Encoder myEncoder;
                EncoderParameter myEncoderParameter;
                EncoderParameters myEncoderParameters;

                // Get an ImageCodecInfo object that represents the JPEG codec.
                myImageCodecInfo = GetEncoderInfo("image/jpeg");

                // Create an Encoder object based on the GUID
                // for the Quality parameter category.
                myEncoder = Encoder.Quality;

                // Create an EncoderParameters object.
                // An EncoderParameters object has an array of EncoderParameter
                // objects. In this case, there is only one
                // EncoderParameter object in the array.
                myEncoderParameters = new EncoderParameters(1);

                // Save the bitmap as a JPEG file with quality level 100.
                MemoryStream stream = new MemoryStream();
                myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                oBitmap.Save(stream, myImageCodecInfo!, myEncoderParameters);
                retval = stream?.ToArray();
            }
            return retval;

        }

        private static ImageCodecInfo? GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        public static ImageFormat ImageFormat(Media.MimeCods mime)
        {
            ImageFormat retval;
            switch (mime)
            {
                case Media.MimeCods.Jpg:
                    retval = System.Drawing.Imaging.ImageFormat.Jpeg;
                    break;
                case Media.MimeCods.Gif:
                    retval = System.Drawing.Imaging.ImageFormat.Gif;
                    break;
                case Media.MimeCods.Png:
                    retval = System.Drawing.Imaging.ImageFormat.Png;
                    break;
                case Media.MimeCods.Tif:
                case Media.MimeCods.Tiff:
                    retval = System.Drawing.Imaging.ImageFormat.Tiff;
                    break;
                case Media.MimeCods.Bmp:
                    retval = System.Drawing.Imaging.ImageFormat.Bmp;
                    break;
                default:
                    retval = System.Drawing.Imaging.ImageFormat.Jpeg;
                    break;

            }
            return retval;
        }
    }
}
