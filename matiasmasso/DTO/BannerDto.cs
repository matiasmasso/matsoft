using DocumentFormat.OpenXml.Wordprocessing;
using DTO.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class BannerModel : BaseGuid
    {
        public string? Nom { get; set; }
        public DateTime? FchFrom { get; set; }
        public DateTime? FchTo { get; set; }
        public string? NavigateTo { get; set; }
        public Guid? Product { get; set; }
        public LangDTO? Lang { get; set; }
        public DateTime FchCreated { get; set; }

        public const int BANNERWIDTH = 640;
        public const int BANNERHEIGHT = 292;
        public const int THUMBWIDTH = 105;
        public const int THUMBHEIGHT = 48;
        public const string ImageUrlSegment = "banner/image";

        public BannerModel() : base() { }
        public BannerModel(Guid guid) : base(guid) { }

        public string ImageUrl() => Globals.ApiUrl("banner/image", Guid.ToString());
        public bool IsActive(LangDTO lang) => (FchFrom == null || FchFrom <= DateTime.Now) && (FchTo == null || FchTo >= DateTime.Now) && (Lang == null || Lang.Id == lang.Id);

        public Box Box(LangDTO lang)
        {
            var retval = new Box(Guid)
            {
                Caption = Nom ?? "",
                Url = UrlHelper.RelativeUrl(lang, NavigateTo),
                ImageUrl = ImageUrl()
            };
            return retval;
        }

    }


    public class BannerDTO : BaseGuid
    {
        public const int BANNERWIDTH = 640;
        public const int BANNERHEIGHT = 292;
        public const int THUMBWIDTH = 105;
        public const int THUMBHEIGHT = 48;
        public const string ImageUrlSegment = "banner/image";

        public BannerDTO() : base() { }
        public BannerDTO(Guid guid) : base(guid) { }

    }
}
