using MatHelperStd;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DTO
{
    public class DTOProductCategory : DTOProduct
    {
        public new DTOProductBrand Brand { get; set; }

        public int Id { get; set; }
        public bool Dsc_PropagateToChildren { get; set; }
        public int InnerPack { get; set; }
        public int OuterPack { get; set; }
        public bool ForzarInnerPack { get; set; }
        public new DTOCodiMercancia CodiMercancia { get; set; }
        public List<DTOProductSku> Skus { get; set; }
        public Codis Codi { get; set; }
        public DTOCnap Cnap { get; set; }
        public bool EnabledxConsumer { get; set; }
        public bool EnabledxPro { get; set; }
        public bool NoStk { get; set; }
        public int Ord { get; set; }
        public bool NoDimensions { get; set; }
        public int DimensionL { get; set; }
        public int DimensionW { get; set; }
        public int DimensionH { get; set; }
        public decimal KgBrut { get; set; }
        public decimal KgNet { get; set; }
        public decimal VolumeM3 { get; set; }
        public DTOEan PackageEan { get; set; }
        public bool IsBundle { get; set; }
        public DTOCountry MadeIn { get; set; }
        public DTOFilter.Item.Collection FilterItems { get; set; }


        public DateTime FchLastEdited { get; set; }


        [JsonIgnore]
        public Byte[] Thumbnail { get; set; }
        [JsonIgnore]
        public Byte[] Image { get; set; }
        public new DTOYearMonth Launchment { get; set; }
        public DateTime HideUntil { get; set; }

        public DTOUsrLog UsrLog { get; set; }

        public const int IMAGEWIDTH = 410;
        public const int IMAGEHEIGHT = 410;
        public const int THUMBNAILWIDTH = 140;
        public const int THUMBNAILHEIGHT = 140;


        public enum Wellknowns
        {
            rockaRoo,
            dualfix_iSize,
            mamaroo5
        }


        public enum Codis
        {
            standard,
            accessories,
            spareparts,
            POS,
            others
        }

        public enum SortOrders
        {
            Alfabetic,
            Custom
        }

        public DTOProductCategory() : base()
        {
            base.SourceCod = SourceCods.Category;
            Skus = new List<DTOProductSku>();
            Nom = new DTOLangText(base.Guid, DTOLangText.Srcs.ProductNom);
            SeoTitle = new DTOLangText(base.Guid, DTOLangText.Srcs.SeoTitle);
            Excerpt = new DTOLangText(base.Guid, DTOLangText.Srcs.ProductExcerpt);
            Content = new DTOLangText(base.Guid, DTOLangText.Srcs.ProductText);
            FilterItems = new DTOFilter.Item.Collection();
        }

        public DTOProductCategory(Guid oGuid) : base(oGuid)
        {
            base.SourceCod = SourceCods.Category;
            Skus = new List<DTOProductSku>();
            Nom = new DTOLangText(base.Guid, DTOLangText.Srcs.ProductNom);
            SeoTitle = new DTOLangText(base.Guid, DTOLangText.Srcs.SeoTitle);
            Excerpt = new DTOLangText(base.Guid, DTOLangText.Srcs.ProductExcerpt);
            Content = new DTOLangText(base.Guid, DTOLangText.Srcs.ProductText);
            FilterItems = new DTOFilter.Item.Collection();
        }

        public static DTOProductCategory Factory(DTOProductBrand oBrand)
        {
            DTOProductCategory retval = new DTOProductCategory();
            {
                var withBlock = retval;
                withBlock.Brand = oBrand;
            }
            return retval;
        }

        public static DTOProductCategory Wellknown(DTOProductCategory.Wellknowns id)
        {
            DTOProductCategory retval = null;
            switch (id)
            {
                case Wellknowns.dualfix_iSize:
                    {
                        retval = new DTOProductCategory(new Guid("7318FF90-5847-4D73-9B5B-4DAFC168810B"));
                        break;
                    }

                case Wellknowns.rockaRoo:
                    {
                        retval = new DTOProductCategory(new Guid("FDCAD204-4EF1-49AE-90A9-537AC04FBD19"));
                        break;
                    }
                case Wellknowns.mamaroo5:
                    {
                        retval = new DTOProductCategory(new Guid("2a01150c-731a-498e-8a7e-4ba821ea9b56"));
                        break;
                    }
            }
            return retval;
        }

        public DTOProductCategory Clon()
        {
            List<Exception> exs = new List<Exception>();
            DTOProductCategory retval = new DTOProductCategory();
            DTOBaseGuid.CopyPropertyValues<DTOProductCategory>(this, retval, exs);
            return retval;
        }

        public bool Matches(string searchTerm)
        {
            return Nom.Contains(searchTerm);
        }


        public static string FullNom(DTOProductCategory oCategory)
        {
            string retval = "";
            if (oCategory.Brand == null)
                retval = oCategory.Nom.Esp;
            else
                retval = string.Format("{0} {1}", oCategory.Brand.Nom.Esp, oCategory.Nom.Esp);
            return retval;
        }

        public string Dimensions(DTOLang lang)
        {
            string retval = "";
            if (this.NoDimensions)
            {
                retval = lang.Tradueix("(no aplicable)", "(no aplicable)", "(not applicable)");
            }
            else
            {
                if (this.DimensionW + this.DimensionL + this.DimensionH > 0)
                {
                    retval = string.Format("{0}mm x {1}mm x {2}mm = {3:N3}m3", this.DimensionW, this.DimensionL, this.DimensionH, (Decimal)(this.DimensionW * this.DimensionL * this.DimensionH) / 1000000000);
                }
            }
            return retval;
        }

        public string getUrl(DTOProduct.Tabs oTab = DTOProduct.Tabs.general, bool AbsoluteUrl = false, DTOLang lang = null)
        {
            var retval = MmoUrl.Factory(AbsoluteUrl, DTOProductBrand.urlSegment(this.Brand), DTOProductCategory.urlSegment(this));

            if (oTab != DTOProduct.Tabs.general)
                retval = retval + "/" + DTOProduct.TabUrlSegment(oTab).Tradueix(lang);

            return retval;
        }




        public string GetUrl(DTOWebDomain domain, DTOProduct.Tabs oTab = DTOProduct.Tabs.general, DTOLang lang = null)
        {
            var retval = "";
            if (this.Brand != null)
                retval = domain.Url(DTOProductBrand.urlSegment(this.Brand), urlSegment(this));
            else
                retval = domain.Url("category", base.Guid.ToString());

            if (oTab != DTOProduct.Tabs.general)
                retval = retval + "/" + DTOProduct.TabUrlSegment(oTab).Tradueix(lang);

            return retval;
        }

        public static string urlSegment(DTOProductCategory oCategory)
        {
            string retval = "";
            if (oCategory != null)
                retval = oCategory.urlSegment();
            return retval;
        }

        public string urlSegment()
        {
            return MatHelperStd.UrlHelper.EncodedUrlSegment(base.Nom.Esp);
        }

        public string UrlFullSegment(DTOProduct.Tabs oTab = DTOProduct.Tabs.general, DTOLang lang = null)
        {
            var retval = string.Format("{0}/{1}", this.Brand.urlSegment(), this.urlSegment());

            if (oTab != DTOProduct.Tabs.general)
                retval = string.Format("{0}/{1}", retval, DTOProduct.TabUrlSegment(oTab).Tradueix(lang));

            return retval;
        }


        public string ThumbnailUrl()
        {
            string retval = MmoUrl.ApiUrl("productCategory/thumbnail", base.Guid.ToString());
            return retval;
        }

        public string ImageUrl()
        {
            return MmoUrl.ApiUrl("productCategory/image", base.Guid.ToString());
        }

        public static DTOCodiMercancia CodiMercanciaOrInherited(DTOProductCategory oCategory)
        {
            DTOCodiMercancia retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oCategory.CodiMercancia == null)
                retval = oCategory.Brand.CodiMercancia;
            else
                retval = oCategory.CodiMercancia;
            return retval;
        }

        public static string SelfOrInheritedCnapFullNom(DTOProductCategory oCategory, DTOLang oLang)
        {
            string retval = "";
            if (oCategory != null)
            {
                if (oCategory.Cnap == null)
                {
                    if (oCategory.Brand != null && oCategory.Brand.Cnap != null)
                        retval = oCategory.Brand.Cnap.FullNom(oLang);
                }
                else
                    retval = oCategory.Cnap.FullNom(oLang);
            }
            return retval;
        }


        public static string ExcerptOrShortDescription(DTOProductCategory oCategory, DTOLang oLang, int MaxLen = 0, bool BlAppendEllipsis = true)
        {
            string retval = "";
            if (oCategory.Excerpt != null)
                retval = oCategory.Excerpt.Tradueix(oLang);
            if (retval == "")
            {
                string sText = oCategory.Content.Tradueix(oLang);
                retval = TextHelper.Excerpt(sText, MaxLen, BlAppendEllipsis);
            }
            return retval;
        }

        public static DTOCountry MadeInOrInherited(DTOProductCategory oCategory)
        {
            DTOCountry retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oCategory == null)
            {
            }
            else if (oCategory.MadeIn == null)
            {
                if (oCategory.Brand == null)
                {
                }
                else
                    retval = oCategory.Brand.MadeIn;
            }
            else
                retval = oCategory.MadeIn;
            return retval;
        }

        public static DTOGuidNom ToGuidNom(DTOProductCategory oCategory)
        {
            DTOGuidNom retval = new DTOGuidNom(oCategory.Guid, DTOProductCategory.FullNom(oCategory));
            return retval;
        }

        public static List<DTOGuidNom> ToGuidNoms(IEnumerable<DTOProductCategory> oCategories)
        {
            List<DTOGuidNom> retval = new List<DTOGuidNom>();
            foreach (var oCategory in oCategories)
            {
                var item = DTOProductCategory.ToGuidNom(oCategory);
                retval.Add(item);
            }
            return retval;
        }

        //builds a plugin to insert into Html content
        //that expands a gallery of all active skus
        public DTOProductPlugin PluginCollection(Models.ClientCache oCache)
        {
            var retval = new DTOProductPlugin(Guid);
            var oActiveSkus = oCache.Skus.Where(x => x.Category.Guid.Equals(Guid) & x.obsoleto == false & x.NoPro == false & x.NoWeb == false).ToList();
            foreach (var sku in oActiveSkus)
            {
                var item = retval.AddItem(sku);
                item.LangNom = sku.Nom;
            }
            return retval;
        }
        public DTOProductPlugin PluginAccessories(Models.ClientCache oCache)
        {
            var retval = new DTOProductPlugin(Guid);
            var accessories = oCache.ProductSpares.Where(x => x.Target == Guid & x.Cod == DTOProductSpare.Cods.Accessories).ToList();
            var oActiveSkus = oCache.Skus.Where(x => accessories.Any(y => x.Guid == y.Product) & x.obsoleto == false & x.NoPro == false & x.NoWeb == false).ToList();
            foreach (var sku in oActiveSkus)
            {
                var item = retval.AddItem(sku);

                if (sku.includeCategoryWithAccessoryNom())
                    item.LangNom = DTOLangText.Concatenate(sku.Category.Nom, new DTOLangText(" "), sku.Nom);
                else
                    item.LangNom = sku.Nom;
            }
            return retval;
        }


        //builds the Html markup to insert into Html content
        //to be expanded with a gallery of all active skus
        //when the user requests the page
        public string PluginCollectionMarkup(DTOProductPlugin.Modes mode)
        {
            return DTOProductPlugin.Snippet(Guid.ToString(), mode);
        }


        public string PluginCustomMarkup()
        {
            return DTOProductPlugin.Snippet(Guid.ToString(), DTOProductPlugin.Modes.Custom);
        }

        public string PluginStoreLocator()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!--Store locator CallToAction Plugin--------------->");
            sb.AppendLine(string.Format("<div class='StoreLocatorCallToAction'><a href = '{0}#StoreLocator'></a></div>", getUrl(Tabs.general)));
            return sb.ToString();
        }


        public string SpriteSkuColorsUrl(int width, int height)
        {
            return MmoUrl.ApiUrl("ProductCategory/skuColors/sprite", base.Guid.ToString(), width.ToString(), height.ToString());
        }

        public override string ToString()
        {
            string retval = "DTOProductCategory Guid:" + this.Guid.ToString();
            if (this.Nom != null)
                retval += " " + this.Nom.Esp;
            return retval;
        }

        public DTOProductCategory Clone()
        {
            DTOProductCategory retval = new DTOProductCategory(Guid);
            retval.Nom = Nom;
            retval.Codi = Codi;
            return retval;
        }



        public class Treenode
        {
            public Guid Guid { get; set; }
            public DTOLangText.Compact Nom { get; set; }
            public decimal KgBrut { get; set; }
            public decimal KgNet { get; set; }
            public int DimensionH { get; set; }
            public int DimensionL { get; set; }
            public int DimensionW { get; set; }
            public int InnerPack { get; set; }
            public Boolean ForzarInnerPack { get; set; }
            public List<DTOProductSku.Treenode> Skus { get; set; }

            public static Treenode Factory(Guid guid)
            {
                Treenode retval = new Treenode();
                retval.Guid = guid;
                retval.Skus = new List<DTOProductSku.Treenode>();
                return retval;
            }

            public override string ToString()
            {
                string retval = "Category.Treenode Guid:" + this.Guid.ToString();
                if (this.Nom != null)
                    retval += " " + this.Nom.Esp;
                return retval;
            }

            public DTOProductCategory ToProductCategory(DTOProductBrand brand)
            {
                DTOProductCategory retval = new DTOProductCategory(this.Guid);
                retval.Brand = brand;
                retval.Nom = this.Nom.toLangText();
                retval.DimensionW = this.DimensionW;
                retval.DimensionL = this.DimensionL;
                retval.DimensionH = this.DimensionH;
                retval.KgBrut = this.KgBrut;
                retval.KgNet = this.KgNet;
                retval.InnerPack = this.InnerPack;
                retval.ForzarInnerPack = this.ForzarInnerPack;
                return retval;
            }

        }
    }
}
