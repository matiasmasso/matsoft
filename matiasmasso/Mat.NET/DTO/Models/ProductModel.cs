using System;
using System.Collections.Generic;

namespace DTO
{
    public class ProductModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Excerpt { get; set; }

        public NavViewModel NavViewModel { get; set; }

        public DTOProduct Product { get; set; }
        public string Tag { get; set; }

        public string ImageUrl { get; set; }

        public string BrandLogoUrl { get; set; }

        public DTOAmt Retail { get; set; }

        public GalleryModes GalleryMode { get; set; }

        public List<DTOFilter> Filters { get; set; }

        public List<DTOFilter.Item> CheckedFilterItems { get; set; }
        public ImageBoxViewModel.Collection Items { get; set; }

        public enum GalleryModes
        {
            NotSet,
            Categories,
            Depts
        }


        public ProductModel() : base()
        {
            Items = new ImageBoxViewModel.Collection();
            Filters = new DTOFilter.Collection();
            CheckedFilterItems = new DTOFilter.Item.Collection();
        }

        public static ProductModel Factory(DTOProductBrand oBrand, DTOLang oLang, DTOProduct.Tabs oTab)
        {
            ProductModel retval = new ProductModel();
            {
                var withBlock = retval;
                withBlock.Product = oBrand;
                withBlock.BrandLogoUrl = oBrand.LogoUrl();
                withBlock.Title = oBrand.Nom.Tradueix(oLang);
                withBlock.Tag = oBrand.Guid.ToString();
                //withBlock.NavViewModel = NavViewModel.Factory(oBrand, oTab, oLang);
            }
            return retval;
        }

        public static ProductModel Factory(DTODept oDept, DTOLang oLang, DTOProduct.Tabs oTab)
        {
            ProductModel retval = new ProductModel();
            {
                var withBlock = retval;
                withBlock.Product = oDept;
                withBlock.BrandLogoUrl = oDept.Brand.LogoUrl();
                withBlock.Title = String.Format("{0} {1}", oDept.Brand.Nom, oDept.Nom.Tradueix(oLang));
                withBlock.Tag = oDept.Guid.ToString();
                //withBlock.NavViewModel = NavViewModel.Factory(oDept, oTab, oLang);
            }
            return retval;
        }


        public string BrandNom(DTOLang oLang)
        {
            return DTOProduct.Brand(Product).Nom.Tradueix(oLang);
        }

        public string BrandUrl()
        {
            return DTOProduct.Brand(Product).getUrl();
        }


        public string CategoryNom(DTOLang oLang)
        {
            return DTOProduct.Category(Product).Nom.Tradueix(oLang);
        }

        public string CategoryUrl()
        {
            return DTOProduct.Category(Product).getUrl();
        }

        public string SkuNom(DTOLang oLang)
        {
            return DTOProduct.Sku(Product).Nom.Tradueix(oLang);
        }

        public bool TextHasImages()
        {
            bool retval = false;
            if (Text != null)
                retval = Text.Contains("<img ");
            return retval;
        }

        public string FacebookImgUrl()
        {
            return DTOFacebook.FbImg(Text);
        }

        public string TextBeforeMoreOrDefault()
        {
            var retval = string.Empty;
            if (!string.IsNullOrEmpty(Text))
            {
                var label = "<more>";
                var pos = Text.IndexOf(label);
                retval = pos > 0 ? Text.Substring(0, pos) : Text;
            }
            return retval;
        }
        public string TextAfterMoreOrDefault()
        {
            var retval = string.Empty;
            if (!string.IsNullOrEmpty(Text))
            {
                var label = "<more>";
                var pos = Text.IndexOf(label);
                retval = pos > 0 ? Text.Substring(pos + label.Length) : string.Empty;
            }

            return retval;
        }
    }

}