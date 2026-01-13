using System;

namespace DTO
{
    using Newtonsoft.Json;

    public class DTOBanner : DTOBaseGuid
    {
        public string Nom { get; set; }
        public Guid SrcGuid { get; set; }
        public Srcs SrcType { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }
        public DTOUrl NavigateTo { get; set; } // <AllowHtml>
        public DTOProduct Product { get; set; }
        public DTOLang Lang { get; set; }
        [JsonIgnore]
        public Byte[] Image { get; set; }
        [JsonIgnore]
        public Byte[] Thumbnail { get; set; }

        public const int BANNERWIDTH = 640;
        public const int BANNERHEIGHT = 292;
        public const int THUMBWIDTH = 105;
        public const int THUMBHEIGHT = 48;

        public enum Srcs
        {
            notSet,
            product,
            news
        }

        public DTOBanner() : base()
        {
        }

        public DTOBanner(Guid oGuid) : base(oGuid)
        {
        }

        public Boolean isActive()
        {
            bool retval = true;
            if (this.FchFrom > DTO.GlobalVariables.Now())
                retval = false;
            if (this.FchTo != DateTime.MinValue && this.FchTo < DTO.GlobalVariables.Now())
                retval = false;
            return retval;
        }

        public string ImageUrl()
        {
            return MmoUrl.ApiUrl("banner/image", Guid.ToString());
        }

    }
}
