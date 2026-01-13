using System;

namespace DTO
{
    public class NavViewModel
    {
        public DTOUser User { get; set; }
        public DTOProductBrand Brand { get; set; }
        public DTOFilter.Collection Filters { get; set; }
        public DTOFilter.Item.Collection CheckedFilterItems { get; set; }
        public DTOMenu.Collection GlobalMenu { get; set; }
        public DTOMenu.Collection ProductsMenu { get; set; }
        public DTOMenu.Collection CustomMenu { get; set; }


        public enum MenuGroupCods
        {
            Unknown,
            Global,
            Specific,
            Products,
            Professional,
            Profile,
            Developer
        }

        public NavViewModel(DTOUser user)
        {
            User = user;
            GlobalMenu = new DTOMenu.Collection();
            ProductsMenu = new DTOMenu.Collection();
            CustomMenu = new DTOMenu.Collection();
            Filters = new DTOFilter.Collection();
            CheckedFilterItems = new DTOFilter.Item.Collection();
        }

        public MenuGroupCods Cod(Guid wellknownGuid)
        {
            MenuGroupCods retval = MenuGroupCods.Unknown;
            return retval;
        }

        public static NavViewModel Factory(DTOUser user, DTOMenu.Collection menus)
        {
            NavViewModel retval = new NavViewModel(user);
            foreach (DTOMenu menuitem in menus)
            {
                if (menuitem.Cod == DTOMenu.Cods.Product)
                    retval.ProductsMenu.Add(menuitem);
                else
                    retval.GlobalMenu.Add(menuitem);
            }
            return retval;
        }

        public void ResetCustomMenu()
        {
            Brand = null;
            CustomMenu = new DTOMenu.Collection();
            Filters = new DTOFilter.Collection();
            CheckedFilterItems = new DTOFilter.Item.Collection();
        }

        public void LoadCustomMenu(DTOLang lang, DTOProductBrand product, DTOProduct.Tabs oTab = DTOProduct.Tabs.general)
        {
            ResetCustomMenu();
            Brand = product;
            CustomMenu.Add(StoreLocator(product, lang));
            CustomMenu.Add(ImageGallery(product, lang));
            CustomMenu.Add(Downloads(product, lang));
            CustomMenu.Add(Videos(product, lang));
            CustomMenu.Add(BloggerPosts(product, lang));
        }

        public void LoadCustomMenu(DTODept product, DTOFilter.Collection filters = null, DTOFilter.Item.Collection checkedFilterItems = null)
        {
            ResetCustomMenu();
            Brand = product.Brand;

            if (filters != null)
                Filters = filters;

            if (checkedFilterItems != null)
                CheckedFilterItems = checkedFilterItems;

        }

        public void LoadCustomMenu(DTOProductCategory product, DTOLang lang)
        {
            ResetCustomMenu();
            Brand = product.Brand;
            CustomMenu.Add(Collection(product, lang));
            CustomMenu.Add(StoreLocator(product, lang));
            if (Brand.UnEquals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.TommeeTippee)))
                CustomMenu.Add(Accessories(product, lang));
            CustomMenu.Add(ImageGallery(product, lang));
            CustomMenu.Add(Downloads(product, lang));
            CustomMenu.Add(Videos(product, lang));
            if (Brand.UnEquals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.TommeeTippee)))
                CustomMenu.Add(BloggerPosts(product, lang));
        }

        public void LoadCustomMenu(DTOProductSku product, DTOLang lang)
        {
            ResetCustomMenu();
            Brand = product.Category.Brand;
            CustomMenu.Add(StoreLocator(product, lang));
            if (Brand.UnEquals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.TommeeTippee)))
                CustomMenu.Add(Accessories(product, lang));
            CustomMenu.Add(ImageGallery(product, lang));
            CustomMenu.Add(Downloads(product, lang));
            CustomMenu.Add(Videos(product, lang));
            if (Brand.UnEquals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.TommeeTippee)))
                CustomMenu.Add(BloggerPosts(product, lang));
        }

        private DTOMenu StoreLocator(DTOProduct product, DTOLang lang)
        {
            //string url = product.Url(DTOProduct.Tabs.distribuidores);
            string url = product.GetUrl(lang, DTOProduct.Tabs.distribuidores);
            DTOLangText caption = DTOLangText.Factory("¿Dónde comprar?", "On comprar?", "Where to buy?", "Onde comprar");
            DTOMenu retval = DTOMenu.Factory(caption, url);
            return retval;
        }

        private DTOMenu Collection(DTOProduct product, DTOLang lang)
        {
            //string url = product.RelativeUrl(lang,DTOProduct.Tabs.coleccion);
            string url = product.GetUrl(lang, DTOProduct.Tabs.coleccion);
            DTOLangText caption = DTOLangText.Factory("Colección", "Col·lecció", "Designs", "Coleçao");
            DTOMenu retval = DTOMenu.Factory(caption, url);
            return retval;
        }

        private DTOMenu Accessories(DTOProduct product, DTOLang lang)
        {
            //string url = product.Url(DTOProduct.Tabs.accesorios);
            string url = product.GetUrl(lang, DTOProduct.Tabs.accesorios);
            DTOLangText caption = DTOLangText.Factory("Accesorios", "Accessoris", "Accessories", "Acessórios");
            DTOMenu retval = DTOMenu.Factory(caption, url);
            return retval;
        }
        private DTOMenu ImageGallery(DTOProduct product, DTOLang lang)
        {
            //string url = product.Url(DTOProduct.Tabs.galeria);
            string url = product.GetUrl(lang, DTOProduct.Tabs.galeria);
            DTOLangText caption = DTOLangText.Factory("Galería de imágenes", "Galeria d'imatges", "Image gallery", "Galeria de Imagens");
            DTOMenu retval = DTOMenu.Factory(caption, url);
            return retval;
        }

        private DTOMenu Downloads(DTOProduct product, DTOLang lang)
        {
            //string url = product.Url(DTOProduct.Tabs.descargas);
            string url = product.GetUrl(lang, DTOProduct.Tabs.descargas);
            DTOLangText caption = DTOLangText.Factory("Descargas", "Descàrregues", "Downloads");
            DTOMenu retval = DTOMenu.Factory(caption, url);
            return retval;
        }

        private DTOMenu Videos(DTOProduct product, DTOLang lang)
        {
            //string url = product.Url(DTOProduct.Tabs.videos);
            string url = product.GetUrl(lang, DTOProduct.Tabs.videos);
            DTOLangText caption = DTOLangText.Factory("Vídeos");
            DTOMenu retval = DTOMenu.Factory(caption, url);
            return retval;
        }

        private DTOMenu BloggerPosts(DTOProduct product, DTOLang lang)
        {
            string url = product.GetUrl(lang, DTOProduct.Tabs.bloggerposts);
            DTOLangText caption = DTOLangText.Factory("Publicaciones", "Publicacions", "Related posts", "Publicações");
            DTOMenu retval = DTOMenu.Factory(caption, url);
            return retval;
        }


        private ImageBoxViewModel BrandLogo(DTOProductBrand brand, DTOLang lang)
        {
            ImageBoxViewModel retval = new ImageBoxViewModel();
            retval.ImageUrl = brand.LogoUrl();
            retval.ImageWidth = DTOProductBrand.LogoWidth;
            retval.ImageHeight = DTOProductBrand.LogoHeight;
            retval.NavigateTo = brand.getUrl( DTOProduct.Tabs.general,false,lang);
            retval.Title = string.Format("logo {0}", brand.Nom.Tradueix(lang));
            return retval;
        }

        public TopNavBarViewModel TopNavBar(DTOLang lang)
        {
            TopNavBarViewModel retval = new TopNavBarViewModel();
            //foreach (DTOMenu menuItem in this.ProductsMenu)
            //{
            //    BoxNodeModel node = retval.ProductsMenu.Add(menuItem.Caption.Tradueix(lang), menuItem.LangUrl.Tradueix(lang));
            //    foreach (DTOMenu subMenuitem in menuItem.Items)
            //    {
            //        node.Children.Add(subMenuitem.Caption.Tradueix(lang), subMenuitem.LangUrl.Tradueix(lang));
            //    }
            //}

            foreach (DTOMenu menuItem in this.GlobalMenu)
            {
                BoxNodeModel node = retval.GlobalMenu.Add(menuItem.Caption.Tradueix(lang), menuItem.LangUrl == null ? "" : menuItem.LangUrl.Tradueix(lang));
                foreach (DTOMenu subMenuitem in menuItem.Items)
                {
                    node.Children.Add(subMenuitem.Caption.Tradueix(lang), subMenuitem.LangUrl.Tradueix(lang));
                }
            }

            return retval;
        }

        public SideNavViewModel SideNav(DTOLang lang)
        {
            SideNavViewModel retval = new SideNavViewModel();
            if (this.Brand != null)
                retval.Logo = BrandLogo(this.Brand, lang);

            retval.FiltersCaption = lang.Tradueix("Filtrar por...", "Filtrar per...", "Filter by...");

            foreach (DTOFilter filter in this.Filters)
            {
                BoxNodeModel node = retval.Filters.Add(filter.LangText.Tradueix(lang), "", filter.Guid.ToString());
                foreach (DTOFilter.Item item in filter.Items)
                    node.Children.Add(item.LangText.Tradueix(lang), "", item.Guid.ToString());
            }

            foreach (DTOMenu menuItem in this.CustomMenu)
            {
                BoxNodeModel node = retval.CustomMenu.Add(menuItem.Caption.Tradueix(lang), menuItem.LangUrl.Tradueix(lang));
                foreach (DTOMenu subMenuItem in menuItem.Items)
                    node.Children.Add(subMenuItem.Caption.Tradueix(lang), subMenuItem.LangUrl.Tradueix(lang));
            }

            foreach (DTOMenu menuItem in this.ProductsMenu)
            {
                BoxNodeModel node = retval.ProductsMenu.Add(menuItem.Caption.Tradueix(lang), menuItem.LangUrl.Tradueix(lang));
                foreach (DTOMenu subMenuItem in menuItem.Items)
                    node.Children.Add(subMenuItem.Caption.Tradueix(lang), subMenuItem.LangUrl.Tradueix(lang));
            }


            foreach (DTOMenu menuItem in this.GlobalMenu)
            {
                BoxNodeModel node = retval.GlobalMenu.Add(menuItem.Caption.Tradueix(lang), menuItem.LangUrl == null ? "" : menuItem.LangUrl.Tradueix(lang));
                foreach (DTOMenu subMenuItem in menuItem.Items)
                    node.Children.Add(subMenuItem.Caption.Tradueix(lang), subMenuItem.LangUrl.Tradueix(lang));
            }

            return retval;
        }

        public class TopNavBarViewModel
        {
            public BoxNodeModel.Collection ProductsMenu { get; set; }
            public BoxNodeModel.Collection GlobalMenu { get; set; }

            public TopNavBarViewModel()
            {
                ProductsMenu = new BoxNodeModel.Collection();
                GlobalMenu = new BoxNodeModel.Collection();
            }
        }

        public class SideNavViewModel
        {
            public ImageBoxViewModel Logo { get; set; }
            public BoxNodeModel.Collection Filters { get; set; }
            public BoxNodeModel.Collection CustomMenu { get; set; }
            public BoxNodeModel.Collection ProductsMenu { get; set; }
            public BoxNodeModel.Collection GlobalMenu { get; set; }
            public string FiltersCaption { get; set; }

            public SideNavViewModel()
            {
                Logo = new ImageBoxViewModel();
                Filters = new BoxNodeModel.Collection();
                CustomMenu = new BoxNodeModel.Collection();
                ProductsMenu = new BoxNodeModel.Collection();
                GlobalMenu = new BoxNodeModel.Collection();
            }
        }
    }
}
