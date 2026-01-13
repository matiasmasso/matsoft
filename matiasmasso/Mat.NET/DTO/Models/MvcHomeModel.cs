using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class MvcHomeModel
    {
        public DTOLang Lang { get; set; }

        public List<PortadaImgModel> PortadaImgs { get; set; }
        public BoxModel.Collection Banners { get; set; }
        public BoxModel.Collection BrandsV { get; set; }
        public BoxModel.Collection BrandsH { get; set; }
        public BoxModel.Collection Noticias { get; set; }
        public BoxModel.Collection BlogPosts { get; set; }
        public BoxModel ActiveRaffle { get; set; }

        public NavViewModel NavViewModel { get; set; }



        public const int BRANDSVWIDTH = 200;
        public const int BRANDSVHEIGHT = 242;
        public const int BRANDSHWIDTH = 600;
        public const int BRANDSHHEIGHT = 338;

        public MvcHomeModel()
        {
            Banners = new BoxModel.Collection();
            PortadaImgs = new List<PortadaImgModel>();
            //BrandsV = new BoxModel.Collection();
            //BrandsH = new BoxModel.Collection();
            Noticias = new BoxModel.Collection();
            BlogPosts = new BoxModel.Collection();
            //LoadBrands();
        }

        public string PortadaImgSrc(PortadaImgModel.Ids id) => PortadaImgs.FirstOrDefault(x => x.Id == id.ToString())?.Src() ?? "";
        public string PortadaImgTitle(PortadaImgModel.Ids id) => PortadaImgs.FirstOrDefault(x => x.Id == id.ToString())?.Title ?? "";
        public string PortadaImgNavigateTo(PortadaImgModel.Ids id) => PortadaImgs.FirstOrDefault(x => x.Id == id.ToString())?.NavigateTo ?? "";


        public static MvcHomeModel Factory(DTOLang oLang, List<DTOBanner> banners, List<PortadaImgModel> portadaImgs, List<DTONoticia> noticias, List<DTOBlogPost> blogposts, DTORaffle oActiveRaffle)
        {
            MvcHomeModel retval = new MvcHomeModel();
            {
                retval.Lang = oLang;
                retval.LoadBanners(banners);
                retval.PortadaImgs = portadaImgs;
                retval.LoadNoticias(noticias);
                retval.LoadBlogPosts(blogposts);
                if (oActiveRaffle != null)
                    retval.loadActiveRaffle(oActiveRaffle);
            }
            return retval;
        }

        private void LoadBanners(List<DTOBanner> oBanners)
        {
            Banners = new BoxModel.Collection();
            foreach (var value in oBanners)
            {
                var oBox = BoxModel.Factory(value.Nom, value.NavigateTo.RelativeUrl(this.Lang), value.ImageUrl());
                Banners.Add(oBox);
            }
        }

        //private void LoadBrands()
        //{
        //    BrandsV = new BoxModel.Collection();
        //    {
        //        BrandsV.Add("Britax Römer", "/britax-roemer", "/Media/Img/Portada/BrandsV/br-home-200x242.jpg");
        //        BrandsV.Add("Tommee Tippee", "/Tommee_Tippee", "/Media/Img/Portada/BrandsV/tt-home-200x242.jpg");
        //        BrandsV.Add("4moms", "/4moms", "/Media/Img/Portada/BrandsV/4moms-home-200x242.jpg");
        //    }

        //    BrandsH = new BoxModel.Collection();
        //    {
        //        BrandsH.Add("Britax Römer", "/britax-roemer", "/Media/Img/Portada/BrandsH/BR-home-600.jpg");
        //        BrandsH.Add("Tommee Tippee", "/Tommee_Tippee", "/Media/Img/Portada/BrandsH/TT-home-600.jpg");
        //        BrandsH.Add("4moms", "/4moms", "/Media/Img/Portada/BrandsH/4moms-home-600.jpg");
        //    }
        //}

        private void LoadNoticias(List<DTONoticia> values)
        {
            var domain = DTOWebDomain.Factory(this.Lang, false);
            foreach (var value in values)
            {
                this.Noticias.Add(value.Title.Tradueix(Lang), value.Url().RelativeUrl(Lang), value.ThumbnailUrl());
            }
        }

        public void LoadBlogPosts(List<DTOBlogPost> values)
        {
            var domain = DTOWebDomain.Factory(this.Lang, false);
            foreach (var value in values)
            {
                this.BlogPosts.Add(value.Title.Tradueix(Lang), value.Url().RelativeUrl(Lang), value.ThumbnailUrl());
            }
        }

        public void loadActiveRaffle(DTORaffle value)
        {
            this.ActiveRaffle = BoxModel.Factory(value.Title, DTORaffle.Collection.Url().RelativeUrl(Lang), value.BannerUrl());
        }
    }

}
