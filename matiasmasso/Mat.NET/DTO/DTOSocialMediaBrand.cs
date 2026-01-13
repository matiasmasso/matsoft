using System;

namespace DTO
{
    public class DTOSocialMediaWidget : DTOBaseGuid
    {
        public Platforms Platform { get; set; }
        public string Titular { get; set; }
        public string WidgetId { get; set; }
        public DTOProductBrand Brand { get; set; }

        public enum Platforms
        {
            NotSet,
            Facebook,
            Twitter
        }

        public DTOSocialMediaWidget() : base()
        {
        }

        public DTOSocialMediaWidget(Guid oGuid) : base(oGuid)
        {
        }

        public static string Url(DTOSocialMediaWidget oSocialMediaWidget)
        {
            string retval = "";
            switch (oSocialMediaWidget.Platform)
            {
                case DTOSocialMediaWidget.Platforms.Facebook:
                    {
                        retval = "https://www.facebook.com/" + oSocialMediaWidget.Titular;
                        break;
                    }

                case DTOSocialMediaWidget.Platforms.Twitter:
                    {
                        retval = "https://twitter.com/" + oSocialMediaWidget.Titular;
                        break;
                    }
            }
            return retval;
        }

        public static string BrandNom(DTOSocialMediaWidget oSocialMediaWidget)
        {
            string retval = "M+O";
            if (oSocialMediaWidget.Brand != null)
                retval = oSocialMediaWidget.Brand.Nom.Esp;
            return retval;
        }
    }
}
