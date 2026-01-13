using MatHelperStd;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOProductBrand : DTOProduct
    {

        public class Treenode
        {
            public Guid Guid { get; set; }
            public DTOLangText.Compact Nom { get; set; }
            public List<DTOProductCategory.Treenode> Categories { get; set; }

            public static Treenode Factory(Guid guid)
            {
                Treenode retval = new Treenode();
                retval.Guid = guid;
                retval.Categories = new List<DTOProductCategory.Treenode>();
                return retval;
            }

            public override string ToString()
            {
                string retval = "Brand.Treenode Guid:" + this.Guid.ToString();
                if (this.Nom != null)
                    retval += " " + this.Nom.Esp;
                return retval;
            }

            public DTOProductBrand ToProductBrand()
            {
                DTOProductBrand retval = new DTOProductBrand(this.Guid);
                retval.Nom = this.Nom.toLangText();
                return retval;
            }

        }

        public const int LogoWidth = 150;
        public const int LogoHeight = 48;

        public DTOEmp Emp { get; set; }
        public int Id { get; set; }
        public DTOProveidor Proveidor { get; set; }
        public CodDists CodDist { get; set; }
        [JsonIgnore]
        public Byte[] Logo { get; set; }
        [JsonIgnore]
        public Byte[] LogoDistribuidorOficial { get; set; }
        public bool ShowAtlas { get; set; }
        public bool EnabledxConsumer { get; set; }
        public bool EnabledxPro { get; set; }

        public DateTime FchLastEdited { get; set; } // per sitemap.xml

        public List<DTOProductCategory> Categories { get; set; }
        public DTODept.Collection Depts { get; set; }

        public DTOCliProductBlocked.Codis CliProductBlockedCodi { get; set; }

        public DTOCnap Cnap { get; set; }

        public DTOCountry MadeIn { get; set; }
        public new DTOCodiMercancia CodiMercancia { get; set; }

        public int WebAtlasDeadline { get; set; } // Max Days from last order to be published as distributor
        public int WebAtlasRafflesDeadline { get; set; } // Max Days from last order to be published as raffle sale point
        public bool EnLiquidacio { get; set; }

        public enum CodDists
        {
            Free,
            DistribuidorsOficials
        }

        public enum CodStks
        {
            NotSet,
            Intern // nosaltres estoquem. Entra quan comprem, surt quan venem
    ,
            Extern // ells estoquen; entra automáticament tot allo i nomes allo que surt
        }

        public enum Wellknowns
        {
            Britax,
            Romer,
            Bob,
            Inglesina,
            FourMoms,
            TommeeTippee,
            FisherPrice,
            JBimbi,
            Gro,
            Varios,
            None
        }

        public DTOProductBrand() : base()
        {
            base.SourceCod = SourceCods.Brand;
            this.Categories = new List<DTOProductCategory>();
            this.Depts = new DTODept.Collection();
        }

        public DTOProductBrand(Guid oGuid) : base(oGuid)
        {
            base.SourceCod = SourceCods.Brand;
            this.Categories = new List<DTOProductCategory>();
            this.Depts = new DTODept.Collection();
        }

        public static DTOProductBrand Wellknown(DTOProductBrand.Wellknowns id)
        {
            DTOProductBrand retval = null;
            string sGuid = "";
            switch (id)
            {
                case DTOProductBrand.Wellknowns.Inglesina:
                    {
                        sGuid = "B1A0FB03-0C18-4607-9091-DF5A6A635BB0";
                        break;
                    }

                case DTOProductBrand.Wellknowns.Britax:
                    {
                        sGuid = "D56CE172-3C98-48E0-A378-8718BE8622F7";
                        break;
                    }

                case DTOProductBrand.Wellknowns.Romer:
                    {
                        sGuid = "D4C2BC59-046D-42D3-86E3-BDCA91FB473F";
                        break;
                    }

                case DTOProductBrand.Wellknowns.Bob:
                    {
                        sGuid = "63F67FDB-812F-49F9-B06C-023EE8A984EC";
                        break;
                    }

                case DTOProductBrand.Wellknowns.FisherPrice:
                    {
                        sGuid = "7C097674-233E-4899-92A7-37F37DD6D1F4";
                        break;
                    }

                case DTOProductBrand.Wellknowns.FourMoms:
                    {
                        sGuid = "67058F90-1FD6-4AE6-82ED-78447779B358";
                        break;
                    }

                case DTOProductBrand.Wellknowns.TommeeTippee:
                    {
                        sGuid = "B55B006D-3322-4E41-8CF7-9A02C3503A09";
                        break;
                    }

                case Wellknowns.Gro:
                    {
                        sGuid = "7b5dd269-62ea-481e-8be3-6e4c9d772117";
                        break;
                    }

                case DTOProductBrand.Wellknowns.Varios:
                    {
                        sGuid = "93995799-2865-46E0-A0EC-05DB6A80C7B4";
                        break;
                    }
            }

            if (sGuid.isNotEmpty())
            {
                Guid oGuid = new Guid(sGuid);
                retval = new DTOProductBrand(oGuid);
            }
            return retval;
        }

        public Wellknowns wellknownId()
        {
            String sGuid = base.Guid.ToString();
            foreach (Wellknowns wk in Enum.GetValues(typeof(Wellknowns)))
            {
                if (Wellknown(wk).Guid.Equals(base.Guid))
                {
                    return wk;
                }
            }
            return Wellknowns.None;
        }
        public static string Aliases(DTOProductBrand oBrand)
        {
            if (oBrand.Equals(Wellknown(Wellknowns.Romer)))
            {
                return "britax, romer, roemer, römer, britax romer, britax roemer, britax-roemer, britax-romer, britax_romer, britax römer";
            }
            else if (oBrand.Equals(Wellknown(Wellknowns.TommeeTippee)))
            {
                return "tommee_tippee,tommee tippee,tommeetippee,tometipe,tomitipi,tome tipe,tomi tipi";
            }
            else if (oBrand.Equals(Wellknown(Wellknowns.FourMoms)))
            {
                return "4moms,fourmoms,formoms";
            }
            else if (oBrand.Equals(Wellknown(Wellknowns.JBimbi)))
            {
                return "jbimbi";
            }
            else if (oBrand.Equals(Wellknown(Wellknowns.FisherPrice)))
            {
                return "fisher-price,fisherprice,fisher price";
            }
            else
            {
                return "";
            }
        }

        public bool Matches(string searchTerm)
        {
            return Nom.Contains(searchTerm);
        }

        public static DTOProductBrand FromNom(string nom)
        {
            DTOProductBrand retval = null;
            var wellknownIds = Enum.GetValues(typeof(Wellknowns));
            foreach (Wellknowns wellknownId in wellknownIds)
            {
                DTOProductBrand oBrand = Wellknown(wellknownId);
                string brandAliases = wellknownId.ToString();
                if (oBrand != null)
                {
                    brandAliases = Aliases(oBrand);
                }
                List<string> aliasList = brandAliases.Split(',').ToList();
                List<string> cleanAliasList = aliasList.Where(x => x.isNotEmpty()).ToList();
                string lowerNom = nom.ToLowerInvariant();
                if (cleanAliasList.Any(x => x.Trim().ToLowerInvariant() == lowerNom))
                {
                    retval = oBrand;
                    break;
                }
            }
            return retval;
        }

        public new string ThumbnailUrl(bool absoluteUrl = false)
        {
            return MmoUrl.ApiUrl("productBrand/thumbnail", base.Guid.ToString()); //falta aplicar a WebApi com passa a categoria
        }


        public DTOProductBrand clon()
        {
            List<Exception> exs = new List<Exception>();
            DTOProductBrand retval = new DTOProductBrand();
            DTOBaseGuid.CopyPropertyValues<DTOProductBrand>(this, retval, exs);
            return retval;
        }


        public static DTOProductBrand Factory(DTOEmp oEmp)
        {
            DTOProductBrand retval = new DTOProductBrand();
            retval.Emp = oEmp;
            return retval;
        }

        public string getUrl(DTOProduct.Tabs oTab = DTOProduct.Tabs.general, bool AbsoluteUrl = false, DTOLang lang = null)
        {
            var retval = MmoUrl.Factory(AbsoluteUrl, DTOProductBrand.urlSegment(this));

            if (oTab != DTOProduct.Tabs.general)
                retval = retval + "/" + DTOProduct.TabUrlSegment(oTab).Tradueix(lang);

            return retval;
        }



        public static string urlSegment(DTOProductBrand oBrand)
        {
            string retval = "";
            if (oBrand != null)
                retval = oBrand.urlSegment();
            return retval;
        }

        public string urlSegment(DTOProduct.Tabs oTab = DTOProduct.Tabs.general, DTOLang lang = null)
        {
            string retval = "";
            if (lang == null) lang = DTOLang.Default();
            if (this.Equals(DTOProductBrand.Wellknown(Wellknowns.Romer)))
                retval = "britax-roemer";
            else
                retval = MatHelperStd.UrlHelper.EncodedUrlSegment(this.Nom.Esp);

            if (oTab != DTOProduct.Tabs.general)
                retval = string.Format("{0}/{1}", retval, DTOProduct.TabUrlSegment(oTab).Tradueix(lang));

            return retval;
        }


        public string UrlSegmentSalePointsPerArea(string AreaNom)
        {
            return string.Format("/{0}/{1}", urlSegment(), AreaNom);
        }

        public string LogoUrl()
        {
            return MmoUrl.ApiUrl("productBrand/logo", base.Guid.ToString());
        }

        public static DTOGuidNom ToGuidNom(DTOProductBrand oBrand)
        {
            DTOGuidNom retval = new DTOGuidNom(oBrand.Guid, oBrand.Nom.Esp);
            return retval;
        }

        public static List<DTOGuidNom> ToGuidNoms(IEnumerable<DTOProductBrand> oBrands)
        {
            List<DTOGuidNom> retval = new List<DTOGuidNom>();
            foreach (var oBrand in oBrands)
            {
                var item = DTOProductBrand.ToGuidNom(oBrand);
                retval.Add(item);
            }
            return retval;
        }

        public SpriteHelper.Sprite DeptsSprite(List<DTODept> oDepts, DTOLang lang)
        {
            var url = MmoUrl.ApiUrl("depts/sprite", base.Guid.ToString());
            var retval = SpriteHelper.Factory(url, DTODept.IMAGEWIDTH, DTODept.IMAGEHEIGHT);
            foreach (var oDept in oDepts)
                retval.addItem(oDept.Nom.Tradueix(lang), oDept.GetUrl(lang));
            return retval;
        }

        public DTOProductBrand Clone()
        {
            DTOProductBrand retval = new DTOProductBrand(Guid);
            retval.Nom = Nom;
            return retval;
        }

        public new class Collection : List<DTOProductBrand>
        {

        }
    }
}
