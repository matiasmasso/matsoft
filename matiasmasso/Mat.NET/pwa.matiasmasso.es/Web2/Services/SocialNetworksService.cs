using DTO;

namespace Web.Services
{
    public class SocialNetworksService
    {
        private CultureService culture;

        const string mmo_facebook_es = "www.facebook.com/matiasmasso.sa";
        const string mmo_facebook_pt = "www.facebook.com/matiasmasso.sa.pt";
        const string mmo_instagram_es = "www.instagram.com/matiasmasso.sa";
        const string mmo_instagram_pt = "www.instagram.com/matiasmasso.sa_pt";
        const string britax_facebook_es = "www.facebook.com/BritaxES";
        const string britax_facebook_pt = "www.facebook.com/BritaxPT";
        const string britax_instagram_es = "www.instagram.com/britaxroemer_es";
        const string britax_instagram_pt = "www.instagram.com/britaxroemer_pt";
        const string fourmoms_facebook_es = "www.facebook.com/4momsES";
        const string fourmoms_facebook_pt = "www.facebook.com/4moms.pt";
        const string fourmoms_instagram_es = "www.instagram.com/4moms_es";
        const string fourmoms_instagram_pt = "www.instagram.com/4moms_pt";
        const string tt_facebook_es = "www.facebook.com/TommeeTippeeES";

        public enum RRSS
        {
            Facebook,
            Instagram
        }

        public SocialNetworksService(CultureService culture)
        {
            this.culture = culture;
        }

        public string? Url(RRSS rrss, ProductBrandModel? brand = null)
        {
            string? retval = null;
            switch (rrss)
            {
                case RRSS.Facebook:
                    if (culture.TopLevelDomain() == CultureService.Tlds.pt)
                    {
                        if(brand == null)
                            retval = mmo_facebook_pt;
                        else if (brand?.IsBritax() ?? false)
                            retval = britax_facebook_pt;
                        else if (brand?.Is4moms() ?? false)
                            retval = fourmoms_facebook_pt;
                    }
                    else
                    {
                        if (brand == null)
                            retval = mmo_facebook_es;
                        else if(brand?.IsBritax() ?? false)
                            retval = britax_facebook_es;
                        else if (brand?.Is4moms() ?? false)
                            retval = fourmoms_facebook_es;
                        else if (brand?.IsTommeeTippee() ?? false)
                            retval = tt_facebook_es;
                    }
                    break;

                    case RRSS.Instagram:
                    if (culture.TopLevelDomain() == CultureService.Tlds.pt)
                    {
                        if(brand == null)
                            retval = mmo_instagram_pt;
                        else if(brand?.IsBritax() ?? false)
                            retval = britax_instagram_pt;
                        else if (brand?.Is4moms() ?? false)
                            retval = fourmoms_instagram_pt;
                        
                    }
                    else
                    {
                        if (brand == null)
                            retval = mmo_instagram_es;
                        else if (brand?.IsBritax() ?? false)
                            retval = britax_instagram_es;
                        else if (brand?.Is4moms() ?? false)
                            retval = fourmoms_instagram_es;
                    }
                    break;
            }
            return retval;
        }



    }
}
