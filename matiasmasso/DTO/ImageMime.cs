using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ImageMime
    {
        public Byte[] Image { get; set; } = new byte[] { };
        public Media.MimeCods Mime { get; set; } = Media.MimeCods.NotSet;

        public ImageMime() { }
        public ImageMime(byte[] bytes, Media.MimeCods mime) {
            Image = bytes;
            Mime = mime;
        }

        public string ContentType() => MimeHelper.ContentType(Mime);

        public static ImageMime Default(Media.MimeCods? mimeCod = Media.MimeCods.NotSet) => new ImageMime
        {
            Mime = (Media.MimeCods)mimeCod!
        };
    }
}
