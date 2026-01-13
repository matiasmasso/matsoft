using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOProduct : DTOBaseGuid
    {
        public SourceCods SourceCod { get; set; }
        public DTOLangText Nom { get; set; }
        public DTOLangText NomLlarg { get; set; } // exclusiu de Sku
        public DTOLangText SeoTitle { get; set; } 
        public DTOLangText Excerpt { get; set; } // <AllowHtml>
        public DTOLangText Content { get; set; } // <AllowHtml>
        public DTOUrlSegment.Collection UrlSegments { get; set; }
        public DTOUrl UrlCanonicas { get; set; }
        public bool obsoleto { get; set; }


        public enum SourceCods
        {
            NotSet,
            Catalog,
            Brand,
            Category,
            Sku,
            Dept
        }

        // important en minuscules:
        public enum Tabs
        {
            general,
            coleccion,
            distribuidores,
            galeria,
            descargas,
            accesorios,
            videos,
            bloggerposts,
            descripcion,
            imagen
        }

        public enum Relateds
        {
            NotSet,
            Accessories,
            Spares,
            Relateds
        }

        public enum SelectionModes
        {
            Browse,
            SelectAny,
            Selectbrand,
            SelectCategory,
            SelectSku,
            SelectMany,
            SelectBrands,
            SelectCategories,
            SelectSkus
        }

        public DTOProduct() : base()
        {
            Nom = new DTOLangText(base.Guid, DTOLangText.Srcs.ProductNom);
            NomLlarg = new DTOLangText(base.Guid, DTOLangText.Srcs.SkuNomLlarg);
            SeoTitle = new DTOLangText(base.Guid, DTOLangText.Srcs.SeoTitle);
            Excerpt = new DTOLangText(base.Guid, DTOLangText.Srcs.ProductExcerpt);
            Content = new DTOLangText(base.Guid, DTOLangText.Srcs.ProductText);
            UrlSegments = new DTOUrlSegment.Collection();
            UrlCanonicas = new DTOUrl();
        }

        public DTOProduct(Guid oGuid) : base(oGuid)
        {
            Nom = new DTOLangText(base.Guid, DTOLangText.Srcs.ProductNom);
            NomLlarg = new DTOLangText(base.Guid, DTOLangText.Srcs.SkuNomLlarg);
            SeoTitle = new DTOLangText(base.Guid, DTOLangText.Srcs.SeoTitle);
            Excerpt = new DTOLangText(base.Guid, DTOLangText.Srcs.ProductExcerpt);
            Content = new DTOLangText(base.Guid, DTOLangText.Srcs.ProductText);
            UrlSegments = new DTOUrlSegment.Collection();
            UrlCanonicas = new DTOUrl();
        }

        public static DTOProduct Factory(Guid oGuid, DTOProduct.SourceCods oCod, string sNom = "")
        {
            DTOProduct retval = null;
            switch (oCod)
            {
                case SourceCods.Brand
               :
                    {
                        retval = new DTOProductBrand(oGuid);
                        retval.Nom.Esp = sNom;
                        break;
                    }

                case SourceCods.Dept
         :
                    {
                        retval = new DTODept(oGuid);
                        retval.Nom.Esp = sNom;
                        break;
                    }

                case SourceCods.Category
         :
                    {
                        retval = new DTOProductCategory(oGuid);
                        retval.Nom.Esp = sNom;
                        break;
                    }

                case SourceCods.Sku
         :
                    {
                        retval = new DTOProductSku(oGuid);
                        retval.Nom.Esp = sNom;
                        break;
                    }
            }
            return retval;
        }


        public static DTOProduct fromJObject(JObject jProduct)
        {
            DTOProduct retval = null;
            if (jProduct != null)
            {
                DTOProduct oProduct = jProduct.ToObject<DTOProduct>();
                switch (oProduct.SourceCod)
                {
                    case DTOProduct.SourceCods.Brand:
                        retval = jProduct.ToObject<DTOProductBrand>();
                        break;
                    case DTOProduct.SourceCods.Category:
                        retval = jProduct.ToObject<DTOProductCategory>();
                        break;
                    case DTOProduct.SourceCods.Sku:
                        retval = jProduct.ToObject<DTOProductSku>();
                        break;
                    default:
                        break;
                }
            }
            return retval;
        }


        public static DTOProduct FromObject(object oObject)
        {
            DTOProduct retval = null;
            if (oObject != null)
            {
                if (oObject.GetType().IsSubclassOf(typeof(DTOProduct)))
                    retval = (DTOProduct)oObject;
                else if (oObject is DTOProduct)
                    retval = (DTOProduct)oObject;
                else

                {
                    DTOProduct oProduct = (DTOProduct)oObject;
                    switch (oProduct.SourceCod)
                    {
                        case DTOProduct.SourceCods.Sku:
                            {
                                retval = (DTOProductSku)oObject;
                                break;
                            }

                        case DTOProduct.SourceCods.Category:
                            {
                                retval = (DTOProductCategory)oObject;
                                break;
                            }

                        case DTOProduct.SourceCods.Brand:
                            {
                                retval = (DTOProductBrand)oObject;
                                break;
                            }

                        default:
                            {
                                retval = oProduct;
                                break;
                            }
                    }
                }
            }
            return retval;
        }

        public string DomainUrl(DTOWebDomain domain, DTOProduct.Tabs tab = DTOProduct.Tabs.general)
        {
            string retval = this.UrlCanonicas.DomainUrl(domain.DefaultLang());
            if (tab != Tabs.general)
                retval = retval + "/" + tab.ToString();
            return retval;
        }

        public static DTOLangText TabUrlSegment(DTOProduct.Tabs tab)
        {
            DTOLangText retval = null;

            switch (tab)
            {
                case Tabs.coleccion:
                    retval = DTOLangText.Factory("colección", "col·lecció", "designs", "coleçao");
                    break;
                case Tabs.distribuidores:
                    retval =DTOLangText.Factory("distribuidores", "distribuidors", "distributors");
                    break;
                case Tabs.galeria:
                    retval = DTOLangText.Factory("galeria", "galeria", "gallery", "galeria");
                    break;
                case Tabs.descargas:
                    retval = DTOLangText.Factory("descargas", "descàrregues", "downloads");
                    break;
                case Tabs.accesorios:
                    retval = DTOLangText.Factory("accesorios", "accessoris", "accessories", "acessorios");
                    break;
                case Tabs.videos:
                    retval = DTOLangText.Factory("videos");
                    break;
                case Tabs.bloggerposts:
                    retval = DTOLangText.Factory("publicaciones", "publicacions", "posts", "publicaçoes");
                    break;
                case Tabs.descripcion:
                    retval = DTOLangText.Factory("descripcion", "descripcio", "description");
                    break;
                case Tabs.imagen:
                    retval = DTOLangText.Factory("imagen","imatge","image");
                    break;
                default:
                    retval = new DTOLangText();
                    break;
            }
            return retval;
        }

        public static string TabCaption(DTOProduct.Tabs tab, DTOLang lang)
        {
            string retval = "";
            switch (tab)
            {
                case Tabs.general:
                    retval = DTOLangText.Factory("General").Tradueix(lang);
                    break;
                case Tabs.coleccion:
                    retval = DTOLangText.Factory("Colección", "Col·lecció", "Designs", "Coleçao").Tradueix(lang);
                    break;
                case Tabs.distribuidores:
                    retval = DTOLangText.Factory("¿Dónde comprar?", "On comprar?", "Where to buy?", "Onde comprar").Tradueix(lang);
                    break;
                case Tabs.galeria:
                    retval = DTOLangText.Factory("Galería de imágenes", "Galeria d'imatges", "Image gallery", "Galeria de Imagens").Tradueix(lang);
                    break;
                case Tabs.descargas:
                    retval = DTOLangText.Factory("Descargas", "Descàrregues", "Downloads").Tradueix(lang);
                    break;
                case Tabs.accesorios:
                    retval = DTOLangText.Factory("Accesorios", "Accessoris", "Accessories", "Acessórios").Tradueix(lang);
                    break;
                case Tabs.videos:
                    retval = DTOLangText.Factory("Videos").Tradueix(lang);
                    break;
                case Tabs.bloggerposts:
                    retval = DTOLangText.Factory("Publicaciones", "Publicacions", "Related posts", "Publicações").Tradueix(lang);
                    break;
                case Tabs.descripcion:
                    retval = DTOLangText.Factory("Descripción", "Descripció", "Description").Tradueix(lang);
                    break;
                case Tabs.imagen:
                    retval = DTOLangText.Factory("Imagen", "Imatge", "Image").Tradueix(lang);
                    break;
                default:
                    retval = tab.ToString();
                    break;
            }
            return retval;
        }

        public static DTOProduct.Tabs Tab(string segment)
        {
            DTOProduct.Tabs retval = DTOProduct.Tabs.general;
            if (Enum.IsDefined(typeof(DTOProduct.Tabs), segment))
                retval = (DTOProduct.Tabs)Enum.Parse(typeof(DTOProduct.Tabs), segment);
            else
            {
                var tabUrlsegment = TabUrlSegments().FirstOrDefault(x => x.Item1.Matches(segment));
                if (tabUrlsegment != null) retval = tabUrlsegment.Item2;
            }

            return retval;
        }

        public static List<Tuple<DTOLangText, DTOProduct.Tabs>> TabUrlSegments()
        {
            var retval = new List<Tuple<DTOLangText, DTOProduct.Tabs>>();
            foreach (DTOProduct.Tabs tab in (DTOProduct.Tabs[])Enum.GetValues(typeof(DTOProduct.Tabs)))
            {
                var item = new Tuple<DTOLangText, DTOProduct.Tabs>(TabUrlSegment(tab), tab);
                retval.Add(item);
            }
            return retval;
        }

        public string GetUrl(DTOLang lang = null, DTOProduct.Tabs tab = DTOProduct.Tabs.general, bool AbsoluteUrl = false)
        {
            string retval = "";
                if (lang == null)
                    lang = DTOLang.ESP();
            if (this.UrlCanonicas == null || (this.UrlCanonicas.MainSegment == null && this.UrlCanonicas.Path == null))
                retval = DTOWebDomain.Default(AbsoluteUrl).Url("product", this.Guid.ToString());
            else
                retval = AbsoluteUrl ? this.UrlCanonicas.AbsoluteUrl(lang) : this.UrlCanonicas.RelativeUrl(lang);

            if (tab != Tabs.general)
                retval = retval + "/" + TabUrlSegment(tab).Tradueix(lang);
            return retval;
        }

        public string CanonicalUrlSegment(DTOLang lang)
        {
            string retval = "";
            DTOUrlSegment segment = this.UrlSegments.FirstOrDefault(x => x.Canonical && x.Lang.Equals(lang));
            if (segment != null)
            {
                retval = segment.Segment;
            }
            return retval;
        }

        public DTOProduct ToDerivedClass()
        {
            DTOProduct retval = null;
            switch (SourceCod)
            {
                case DTOProduct.SourceCods.Brand:
                    {
                        retval = new DTOProductBrand(base.Guid);
                        retval.Nom = Nom;
                        retval.obsoleto = obsoleto;
                        break;
                    }

                case DTOProduct.SourceCods.Dept:
                    {
                        retval = new DTODept(base.Guid);
                        retval.Nom = Nom;
                        retval.obsoleto = obsoleto;
                        break;
                    }

                case DTOProduct.SourceCods.Category:
                    {
                        retval = new DTOProductCategory(base.Guid);
                        retval.Nom = Nom;
                        retval.obsoleto = obsoleto;
                        break;
                    }

                case DTOProduct.SourceCods.Sku:
                    {
                        retval = new DTOProductSku(base.Guid);
                        retval.Nom = Nom;
                        retval.obsoleto = obsoleto;
                        break;
                    }
            }
            return retval;
        }

        public string FullNom(DTOLang oLang = null)
        {
            if (oLang == null)
                oLang = DTOLang.ESP();
            string retval = "";
            if (this is DTOProductBrand)
            {
                DTOProductBrand obrand = (DTOProductBrand)this;
                retval = obrand.Nom.Esp;
            }
            else if (this is DTODept)
            {
                DTODept oDept = (DTODept)this;
                retval = string.Format("{0} {1}", oDept.Brand.Nom.Tradueix(oLang), oDept.Nom.Tradueix(oLang));
            }
            else if (this is DTOProductCategory)
            {
                DTOProductCategory oCategory = (DTOProductCategory)this;
                retval = string.Format("{0} {1}", oCategory.Brand.Nom.Tradueix(oLang), oCategory.Nom.Tradueix(oLang));
            }
            else if (this is DTOProductSku)
            {
                DTOProductSku oSku = (DTOProductSku)this;
                retval = oSku.NomLlarg.Tradueix(oLang);
                if (string.IsNullOrEmpty(retval))
                    retval = string.Format("{0} {1} {2}", oSku.Category.Brand.Nom.Tradueix(oLang), oSku.Category.Nom.Tradueix(oLang), oSku.Nom.Tradueix(oLang));
            }
            else
                if (NomLlarg != null)
                retval = NomLlarg.Esp;
            if(string.IsNullOrEmpty(retval))
                retval = Nom.Esp;
            return retval;
        }

        public DTOGuidNom.Compact ToGuidNom(DTOLang oLang = null)
        {
            return DTOGuidNom.Compact.Factory(this.Guid, this.FullNom(oLang));
        }


        public string NomCurt(DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (oLang == null)
                oLang = DTOLang.ESP();
            string retval = "";
            if (this is DTOProductBrand)
            {
                DTOProductBrand obrand = (DTOProductBrand)this;
                retval = obrand.Nom.Tradueix(oLang);
            }
            else if (this is DTODept)
            {
                DTODept oDept = (DTODept)this;
                retval = oDept.Nom.Tradueix(oLang);
            }
            else if (this is DTOProductCategory)
            {
                DTOProductCategory oCategory = (DTOProductCategory)this;
                retval = oCategory.Nom.Tradueix(oLang);
            }
            else if (this is DTOProductSku)
            {
                DTOProductSku oSku = (DTOProductSku)this;
                retval = oSku.Nom.Tradueix(oLang);
            }
            return retval;
        }

        public static string GetNom(DTOProduct src)
        {
            string retval = "";
            if (src != null)
            {
                if (src is DTOProductBrand)
                    retval = ((DTOProductBrand)src).Nom.Esp;
                else if (src is DTOProductCategory)
                    retval = ((DTOProductCategory)src).Nom.Esp;
                else if (src is DTOProductSku)
                    retval = ((DTOProductSku)src).Nom.Esp;
                else
                    retval = src.Nom.Esp;

            }
            return retval;
        }

        public static string GetNomLlargOrNom(DTOProduct src, DTOLang lang)
        {
            string retval = "";
            if (src != null)
            {
                if (src is DTOProductBrand)
                    retval = ((DTOProductBrand)src).Nom.Tradueix(lang);
                else if (src is DTOProductCategory)
                    retval = ((DTOProductCategory)src).Nom.Tradueix(lang);
                else if (src is DTOProductSku)
                    retval = ((DTOProductSku)src).NomLlarg.Tradueix(lang);
                else
                    retval = src.Nom.Esp;

            }
            return retval;
        }

        public static DTOProductBrand Brand(DTOProduct oProduct)
        {
            DTOProductBrand retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oProduct != null)
            {
                switch (oProduct.SourceCod)
                {
                    case SourceCods.Brand:
                        {
                            retval = (DTOProductBrand)oProduct;
                            break;
                        }

                    case SourceCods.Dept:
                        {
                            DTODept oDept = (DTODept)oProduct;
                            retval = oDept.Brand;
                            break;
                        }

                    case SourceCods.Category:
                        {
                            DTOProductCategory oCategory = (DTOProductCategory)oProduct;
                            retval = oCategory.Brand;
                            break;
                        }

                    case SourceCods.Sku:
                        {
                            DTOProductSku oSku = (DTOProductSku)oProduct;
                            if (oSku.Category != null)
                                retval = oSku.Category.Brand;
                            break;
                        }
                }
            }
            return retval;
        }

        public static DTOProduct CategoryOrbrand(DTOProduct oProduct)
        {
            DTOProduct retval = oProduct;
            if (retval is DTOProductSku)
                retval = ((DTOProductSku)oProduct).Category;
            return retval;
        }

        public bool Is4moms()
        {
            var o4moms = DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.FourMoms);
            bool retval = o4moms.Guid.Equals(base.Guid);
            return retval;
        }
        public bool IsBritaxRoemer()
        {
            var oBritaxRoemer = DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.Romer);
            bool retval = oBritaxRoemer.Guid.Equals(base.Guid);
            return retval;
        }

        public static Guid brandGuid(DTOProduct src)
        {
            Guid retval;
            var obrand = DTOProduct.Brand(src);
            if (obrand != null)
                retval = obrand.Guid;
            return retval;
        }

        public static string BrandNom(DTOProduct src)
        {
            string retval = "";
            var obrand = DTOProduct.Brand(src);
            if (obrand != null)
                retval = obrand.Nom.Esp;
            return retval;
        }

        public static DTOProductBrand.CodDists BrandCodDist(DTOProduct src)
        {
            var retval = DTOProductBrand.CodDists.Free;
            var obrand = DTOProduct.Brand(src);
            if (obrand != null)
                retval = obrand.CodDist;
            return retval;
        }

        public static DTOProductCategory Category(DTOProduct src)
        {
            DTOProductCategory retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (src != null)
            {
                if (src is DTOProductCategory)
                    retval = (DTOProductCategory)src;
                else if (src is DTOProductSku)
                    retval = ((DTOProductSku)src).Category;
            }
            return retval;
        }
        public static Guid CategoryGuid(DTOProduct src)
        {
            Guid retval;
            var oCategory = DTOProduct.Category(src);
            if (oCategory != null)
                retval = oCategory.Guid;
            return retval;
        }


        public static string CategoryNom(DTOProduct src)
        {
            string retval = "";
            DTOProductCategory oCategory = Category(src);
            if (oCategory != null)
                retval = oCategory.Nom.Esp;
            return retval;
        }

        public static DTOProductSku Sku(DTOProduct src)
        {
            DTOProductSku retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (src is DTOProductSku)
                retval = (DTOProductSku)src;
            return retval;
        }

        public static Guid SkuGuid(DTOProduct src)
        {
            Guid retval;
            var oSku = DTOProduct.Sku(src);
            if (oSku != null)
                retval = oSku.Guid;
            return retval;
        }

        public static string SkuNom(DTOProduct src)
        {
            string retval = "";
            DTOProductSku oSku = Sku(src);
            if (oSku != null)
            {
                retval = oSku.Nom.Esp;
                if (retval == "")
                    retval = oSku.Nom.Esp;
                if (retval == "")
                    retval = oSku.NomLlarg.Esp;
            }
            return retval;
        }

        public DTOLangText SeoTitleOrDefault()
        {
            DTOLangText retval = SeoTitle.isEmpty() ? DefaultSeoTitle() : SeoTitle;
            return retval;
        }

        public DTOLangText DefaultSeoTitle()
        {
            string esp = default, cat=default, eng=default, por=default;

            switch (SourceCod)
            {
                case SourceCods.Brand:
                    {
                        esp = Nom.Esp;
                        cat = Nom.Cat;
                        eng = Nom.Eng;
                        por = Nom.Por;
                        break;
                    }

                case SourceCods.Dept:
                    {
                        var dept = (DTODept)this;
                        esp = string.Format("{0} {1}", dept.Brand.Nom.Esp, dept.Nom.Esp);
                        cat = string.Format("{0} {1}", dept.Brand.Nom.Cat, dept.Nom.Cat);
                        eng = string.Format("{0} {1}", dept.Brand.Nom.Eng, dept.Nom.Eng);
                        por = string.Format("{0} {1}", dept.Brand.Nom.Por, dept.Nom.Por);
                        break;
                    }

                case SourceCods.Category:
                    {
                        var category = (DTOProductCategory)this;
                        esp = string.Format("{0} {1}", category.Brand.Nom.Esp, category.Nom.Esp);
                        cat = string.Format("{0} {1}", category.Brand.Nom.Cat, category.Nom.Cat);
                        eng = string.Format("{0} {1}", category.Brand.Nom.Eng, category.Nom.Eng);
                        por = string.Format("{0} {1}", category.Brand.Nom.Por, category.Nom.Por);
                        break;
                    }

                case SourceCods.Sku:
                    {
                        var sku = (DTOProductSku)this;
                        esp = string.Format("{0} {1} {2}", sku.Category.Brand.Nom.Esp, sku.Category.Nom.Esp, Nom.Esp);
                        cat = string.Format("{0} {1} {2}", sku.Category.Brand.Nom.Cat, sku.Category.Nom.Cat, Nom.Cat);
                        eng = string.Format("{0} {1} {2}", sku.Category.Brand.Nom.Eng, sku.Category.Nom.Eng, Nom.Eng);
                        por = string.Format("{0} {1} {2}", sku.Category.Brand.Nom.Por, sku.Category.Nom.Por, Nom.Por);
                        break;
                    }
            }

            var retval = new DTOLangText(Guid, DTOLangText.Srcs.SeoTitle, esp, cat, eng, por);
            return retval;
        }


        public static decimal SkuCostEur(DTOProduct src)
        {
            decimal retval = 0;
            DTOProductSku oSku = Sku(src);
            if (oSku != null)
            {
                if (oSku.Cost != null)
                    retval = oSku.Cost.Eur;
            }
            return retval;
        }


        public static DTOCodiMercancia CodiMercancia(DTOProduct src)
        {
            DTOCodiMercancia retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (src != null)
            {
                if (src is DTOProductBrand)
                {
                    DTOProductBrand obrand = (DTOProductBrand)src;
                    retval = obrand.CodiMercancia;
                }
                else if (src is DTOProductCategory)
                {
                    DTOProductCategory oCategory = (DTOProductCategory)src;
                    retval = DTOProductCategory.CodiMercanciaOrInherited(oCategory);
                }
                else if (src is DTOProductSku)
                {
                    DTOProductSku oSku = (DTOProductSku)src;
                    retval = DTOProductSku.CodiMercanciaOrInherited(oSku);
                }
            }
            return retval;
        }

        public static string WebPageTitle(DTOProduct oProductBase, DTOLang oLang, DTOProduct.Tabs oTab = DTOProduct.Tabs.general, DTOLocation oLocation = null/* TODO Change to default(_) if this is not a reference type */)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("M+O | ");

            if (oTab != DTOProduct.Tabs.general)
                sb.Append(TabCaption(oTab, oLang) + " ");

            switch (oProductBase.SourceCod)
            {
                case DTOProduct.SourceCods.Brand:
                    {
                        DTOProductBrand obrand = (DTOProductBrand)oProductBase;
                        sb.Append(obrand.Nom.Tradueix(oLang));
                        break;
                    }

                case DTOProduct.SourceCods.Category:
                    {
                        DTOProductCategory oCategory = (DTOProductCategory)oProductBase;
                        sb.Append(oCategory.Brand.Nom.Tradueix(oLang));
                        sb.Append(" ");
                        sb.Append(oCategory.Nom.Tradueix(oLang));
                        break;
                    }

                case DTOProduct.SourceCods.Sku:
                    {
                        DTOProductSku oSKU = (DTOProductSku)oProductBase;
                        sb.Append(oSKU.Category.Brand.Nom.Tradueix(oLang));
                        sb.Append(" ");
                        sb.Append(oSKU.Category.Nom.Tradueix(oLang));
                        sb.Append(" ");
                        sb.Append(oSKU.Nom.Tradueix(oLang));
                        break;
                    }
            }

            if (oLocation != null)
            {
                sb.Append(oLang.Tradueix(" en ", " a ", " on "));
                sb.Append(oLocation.Nom);
            }
            string retval = sb.ToString();
            return retval;
        }



        public static string brandDownloadsRef(int iQty, DTOProductDownload.Srcs oSrc, DTOLang oLang)
        {
            string retval = "";
            if (iQty == 1)
            {
                switch (oSrc)
                {
                    case DTOProductDownload.Srcs.catalogos:
                        {
                            retval = string.Format("{0} {1}", iQty, oLang.Tradueix("catálogo", "cataleg", "catalog"));
                            break;
                        }

                    case DTOProductDownload.Srcs.instrucciones:
                        {
                            retval = string.Format("{0} {1}", iQty, oLang.Tradueix("manual", "manual", "user manual"));
                            break;
                        }

                    case DTOProductDownload.Srcs.compatibilidad:
                        {
                            retval = string.Format("{0} {1}", iQty, oLang.Tradueix("lista", "llista", "list"));
                            break;
                        }
                }
            }
            else
                switch (oSrc)
                {
                    case DTOProductDownload.Srcs.catalogos:
                        {
                            retval = string.Format("{0} {1}", iQty, oLang.Tradueix("catálogos", "catalegs", "catalogues"));
                            break;
                        }

                    case DTOProductDownload.Srcs.instrucciones:
                        {
                            retval = string.Format("{0} {1}", iQty, oLang.Tradueix("manuales", "manuals", "user manuals"));
                            break;
                        }

                    case DTOProductDownload.Srcs.compatibilidad:
                        {
                            retval = string.Format("{0} {1}", iQty, oLang.Tradueix("listas", "llistes", "lists"));
                            break;
                        }
                }
            return retval;
        }

        public static string Launchment(DTOProduct oProduct, DTOLang oLang)
        {
            string retval = "";
            if (oProduct != null)
            {
                try
                {
                    if (oLang == null)
                        oLang = DTOLang.ESP();

                    if (oProduct.obsoleto == true)
                    {
                        retval = oLang.Tradueix("Este producto ya no está disponible", "aquest producte ja no está disponible", "This product is no longer available");
                    }
                    else
                    {
                        DTOYearMonth oYearMonth = null;
                        switch (oProduct.SourceCod)
                        {
                            case DTOProduct.SourceCods.Category:
                                {
                                    DTOProductCategory oCategory = (DTOProductCategory)oProduct;
                                    oYearMonth = oCategory.Launchment;
                                    break;
                                }

                            case DTOProduct.SourceCods.Sku:
                                {
                                    DTOProductSku oSku = (DTOProductSku)oProduct;
                                    oYearMonth = oSku.Category.Launchment;
                                    break;
                                }
                        }

                        if (oYearMonth != null)
                        {
                            if (DTOYearMonth.Current().Tag().isLowerOrEqualThan(oYearMonth.Tag()))
                                retval = string.Format("{0} {1} {2}", oLang.Tradueix("disponible a partir de", "disponible a partir de", "availability"), oLang.Mes((int)oYearMonth.Month), oYearMonth.Year);
                            else if (oYearMonth.isOutdated6monthsOrMore())
                            {
                            }
                            else
                                retval = string.Format("{0} {1} {2}", oLang.Tradueix("novedad", "novetat", "new from"), oLang.Mes((int)oYearMonth.Month), oYearMonth.Year);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            return retval;
        }

        public static DTOProveidor proveidor(DTOProduct src)
        {
            DTOProveidor retval = null/* TODO Change to default(_) if this is not a reference type */;
            DTOProductBrand obrand = Brand(src);
            if (obrand != null)
                retval = obrand.Proveidor;
            return retval;
        }

        public static string EncodedUrlSegment(string nom)
        {
            string retval = nom.ToLower();
            retval = retval.Replace(" ", "_");
            retval = retval.Replace("&", "|");
            retval = retval.Replace("á", "a");
            retval = retval.Replace("é", "e");
            retval = retval.Replace("í", "i");
            retval = retval.Replace("ó", "o");
            retval = retval.Replace("ö", "o");
            retval = retval.Replace("ú", "u");
            retval = retval.Replace("ü", "u");
            return retval;
        }

        public static string DecodedUrlSegment(string encodedNom)
        {
            string retval = encodedNom;
            retval = retval.Replace("_", " ");
            retval = retval.Replace("|", "&");
            return retval;
        }

        public string BrandLogoUrl()
        {
            string retval = "";
            DTOProductBrand brand = DTOProduct.Brand(this);
            if (brand != null)
                retval = brand.LogoUrl();
            return retval;
        }



        public string RelativeUrl(DTOLang lang, Tabs oTab = DTOProduct.Tabs.general)
        {
            //string retval = (this.UrlCanonicas == null) ? this.UrlCanonicas.RelativeUrl(lang) : this.Url(oTab, false);
            string retval = this.UrlCanonicas.RelativeUrl(lang);
            if (oTab != Tabs.general)
                retval += "/" + oTab.ToString();
            return retval;
        }

        public string ThumbnailUrl(bool absoluteUrl = false)
        {
            string retval = "";
            switch (SourceCod)
            {
                case SourceCods.Brand:
                    {
                        retval = ((DTOProductBrand)this).ThumbnailUrl();
                        break;
                    }

                case SourceCods.Category:
                    {
                        retval = ((DTOProductCategory)this).ImageUrl();
                        break;
                    }

                case SourceCods.Sku:
                    {
                        retval = MmoUrl.image(Defaults.ImgTypes.art150, base.Guid, absoluteUrl);
                        break;
                    }
            }
            return retval;
        }

        public string UrlWithFilters(DTOProduct product, DTOLang lang, List<DTOFilter.Item> filterItems)
        {
            string retval = product.GetUrl(lang, Tabs.general, true);
            if (filterItems.Count > 0)
            {
                string[] sGuids = filterItems.Select(x => x.Guid.ToString()).ToArray();
                string sGuidChain = String.Join(",", sGuids);
                string paramValue = String.Format("[{0}]", sGuidChain);
                MmoUrl.addQueryStringParam(ref retval, "filters", paramValue);
            }
            return retval;
        }

        public BreadcrumbViewModel Breadcrumbs(DTOLang lang, string esp = "", string cat = "", string eng = "", string por = "")
        {
            BreadcrumbViewModel retval = new BreadcrumbViewModel();
            switch (this.SourceCod)
            {
                case SourceCods.Brand:
                    DTOProductBrand brand = (DTOProductBrand)this;
                    retval.BrandNom = brand.Nom.Tradueix(lang);
                    if (esp.isNotEmpty())
                        retval.BrandUrl = brand.UrlCanonicas.CanonicalUrl(lang);
                    break;
                case SourceCods.Dept:
                    DTODept dept = (DTODept)this;
                    retval.BrandNom = dept.Brand.Nom.Tradueix(lang);
                    retval.BrandUrl = dept.Brand.UrlCanonicas.CanonicalUrl(lang);
                    retval.DeptNom = dept.Nom.Tradueix(lang);
                    if (esp.isNotEmpty())
                        retval.DeptUrl = dept.UrlCanonicas.CanonicalUrl(lang);
                    break;
                case SourceCods.Category:
                    DTOProductCategory category = (DTOProductCategory)this;
                    retval.BrandNom = category.Brand.Nom.Tradueix(lang);
                    retval.BrandUrl = category.Brand.UrlCanonicas.CanonicalUrl(lang);
                    retval.CategoryNom = category.Nom.Tradueix(lang);
                    if (esp.isNotEmpty())
                        retval.CategoryUrl = category.UrlCanonicas.CanonicalUrl(lang);
                    break;
                case SourceCods.Sku:
                    DTOProductSku sku = (DTOProductSku)this;
                    retval.BrandNom = sku.Category.Brand.Nom.Tradueix(lang);
                    retval.BrandUrl = sku.Category.Brand.UrlCanonicas.CanonicalUrl(lang);
                    retval.CategoryNom = sku.Category.Nom.Tradueix(lang);
                    retval.CategoryUrl = sku.Category.UrlCanonicas.CanonicalUrl(lang);
                    retval.SkuNom = sku.Nom.Tradueix(lang);
                    if (esp.isNotEmpty())
                        retval.SkuUrl = sku.UrlCanonicas.CanonicalUrl(lang);
                    break;
            }

            if (esp != null)
            {
                DTOLangText subtitle = new DTOLangText(esp, cat, eng, por);
                retval.Subtiltle = subtitle.Tradueix(lang);
            }

            return retval;
        }

        public DTOProduct Trim()
        {
            var retval = new DTOProduct(Guid);
            retval.Nom = Nom;
            retval.SourceCod = SourceCod;
            return retval;
        }
        public override string ToString()
        {
            string retval = "DTOProduct Guid:" + this.Guid.ToString();
            if (this.Nom != null)
                retval += " " + this.Nom.Esp;
            return retval;
        }

        public class Collection : List<DTOProduct>
        {

        }

        public class BreadcrumbViewModel
        {
            public string BrandNom { get; set; }
            public string BrandUrl { get; set; }
            public string DeptNom { get; set; }
            public string DeptUrl { get; set; }
            public string CategoryNom { get; set; }
            public string CategoryUrl { get; set; }
            public string SkuNom { get; set; }
            public string SkuUrl { get; set; }
            public string Subtiltle { get; set; }
        }

        public class ProductAndTab
        {
            public DTOProduct Product { get; set; }
            public Tabs Tab { get; set; }
        }

    }
}
