using MatHelperStd;
using Newtonsoft.Json;

using System;

namespace DTO
{
    public class DTOGalleryItem : DTOBaseGuid
    {
        public string Nom { get; set; }
        [JsonIgnore]
        public Byte[] Image { get; set; }
        [JsonIgnore]
        public Byte[] Thumbnail { get; set; }
        public MimeCods Mime { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Size { get; set; }
        public DateTime FchCreated { get; set; }
        public string Hash { get; set; }

        public const int THUMBNAIL_WIDTH = 100;
        public const int THUMBNAIL_HEIGHT = 65;
        public DTOGalleryItem() : base()
        {
        }

        public DTOGalleryItem(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOGalleryItem Factory()
        {
            DTOGalleryItem retval = new DTOGalleryItem();
            {
                var withBlock = retval;
                withBlock.FchCreated = DTO.GlobalVariables.Now();
                withBlock.Nom = "(nom descriptiu de la nova imatge)";
            }
            return retval;
        }


        public static string MultilineText(DTOGalleryItem value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine(value.Nom);
            sb.AppendLine(value.Width + "x" + value.Height + " " + value.Mime.ToString() + " " + DTODocFile.FeatureFileSize(value.Size));
            sb.AppendLine(TextHelper.VbFormat(value.FchCreated, "dd/MM/yy HH:mm"));
            string retval = sb.ToString();
            return retval;
        }

        public string Url(bool AbsoluteUrl = false)
        {
            var retval = MmoUrl.ApiUrl("GalleryItem/Image", base.Guid.ToString());
            return retval;
        }
    }
}
