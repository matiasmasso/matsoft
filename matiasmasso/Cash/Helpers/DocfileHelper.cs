using ImageMagick;
using DTO;
using DTO.Helpers;

namespace Cash.Helpers
{
    public class DocfileHelper
    {

        public static DocfileModel CreateDocfile(Media media, int thumbnailWidth = DocfileModel.THUMB_WIDTH, int thumbnailHeight = DocfileModel.THUMB_HEIGHT, string? filename = null)
        {
            DocfileModel retval = new DocfileModel();
            retval.Hash = ASINHelper.GenerateRandomASIN();
            retval.Sha256 = CryptoHelper.Sha256(media.Data!);
            retval.Filename = filename;
            retval.Document = media;
            retval.Size = media.Data?.Length;
            retval.FchCreated = DateTime.Now;
            retval.Fch = retval.FchCreated;


            switch (media.Mime)
            {
                case Media.MimeCods.Pdf:
                    try
                    {
                        using (MagickImageCollection images = new MagickImageCollection())
                        {
                            MagickReadSettings settings = new MagickReadSettings();
                            settings.FrameIndex = 0;
                            settings.FrameCount = 1;

                            images.Ping(media.Data!);
                            retval.Pags = images.Count;

                            images.Read(media.Data!, settings);
                            using (var image = images.First())
                            {
                                image.Format = MagickFormat.Png;
                                image.Alpha(AlphaOption.Remove);
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
                    }
                    catch (Exception ex)
                    {

                    }
                    break;
            }

            return retval;

        }

    }
}
