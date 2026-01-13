using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DTO
{
    public class DTOProductSku : DTOProduct
    {
        public class Compact
        {
            public Guid Guid { get; set; }
            public int Id { get; set; }
            public DTOLangText.Compact NomLlarg { get; set; }
            public bool Obsoleto { get; set; }
        }
        public new DTOProductCategory Category { get; set; }
        public int Id { get; set; }

        public string RefCustomer { get; set; } // TO DEPRECATE to CustomerProduct
        public DTOCustomerProduct CustomerProduct { get; set; }
        public string RefProveidor { get; set; }
        public DTOEan Ean13 { get; set; }
        public DTOEan PackageEan { get; set; }
        public DTOCnap Cnap { get; set; }
        public string NomProveidor { get; set; }
        public bool Hereda { get; set; }


        public int InnerPack { get; set; }
        public bool ForzarInnerPack { get; set; }
        public bool HeredaDimensions { get; set; }

        [JsonIgnore]
        public Byte[] Image { get; set; }
        public DateTime ImageFch { get; set; }
        public bool ImageExists { get; set; }
        public bool LastProduction { get; set; }
        public DateTime Availability { get; set; }

        public DTOAmt Price { get; set; }
        public decimal CustomerDto { get; set; }
        public decimal SupplierDtoOnInvoice { get; set; }
        public DTOAmt Cost { get; set; }
        public DateTime LastPurchaseDate { get; set; }

        public DTOCustomerTarifaDto DtoSobreRRPP { get; set; }
        public DTOAmt Rrpp { get; set; }
        public decimal Pmc { get; set; } // Preu mig de cost en euros

        public DTOTax.Codis IvaCod { get; set; } = DTOTax.Codis.iva_Standard;

        public int Stock { get; set; }
        public int SecurityStock { get; set; }
        public bool NoStk { get; set; }
        public bool NoWeb { get; set; }
        public bool NoPro { get; set; }
        public bool NoEcom { get; set; } // prevents the product from being listed on ecommerce platforms or marketplaces
        public int Clients { get; set; } // totes les unitats pendents de servir a clients
        public int ClientsAlPot { get; set; } // unitats de comandes en standby a la espera indefinida de confirmació
        public int ClientsEnProgramacio { get; set; } // unitats en programació a mes de una setmana vista
        public int ClientsBlockStock { get; set; } // unitats de comandes amb stock reservat
        public int Proveidors { get; set; }
        public int Previsions { get; set; }

        public int DimensionL { get; set; }
        public int DimensionW { get; set; }
        public int DimensionH { get; set; }
        public decimal KgBrut { get; set; }
        public decimal VolumeM3 { get; set; }

        public DTOCountry MadeIn { get; set; }

        public new DTOCodiMercancia CodiMercancia { get; set; }

        public string Url { get; set; }
        //public bool @virtual { get; set; }
        // Property PackItems As List(Of DTOProductPackItem)

        public List<DTOSkuWith> SkuWiths { get; set; }

        public List<string> Keys { get; set; } = new List<string>();

        public bool Outlet { get; set; }
        public decimal OutletDto { get; set; }
        public int OutletQty { get; set; }
        public DateTime FchLastEdited { get; set; }

        public CodisExclusio CodExclusio { get; set; } = CodisExclusio.Inclos;

        // 000=====================================
        public bool NoTarifa { get; set; }
        public DateTime HideUntil { get; set; }
        public bool NoDimensions { get; set; }
        public int OuterPack { get; set; }
        public decimal KgNet { get; set; }
        public bool IsBundle { get; set; }
        public decimal BundleDto { get; set; }
        public List<DTOSkuBundle> BundleSkus { get; set; }
        public DTOUsrLog UsrLog { get; set; }

        public DateTime FchObsoleto { get; set; }
        public DateTime ObsoletoConfirmed { get; set; }
        public DTOProductSku Substitute { get; set; }

        public const int IMAGEWIDTH = 700;
        public const int IMAGEHEIGHT = 800;
        public const int THUMBNAILWIDTH = 150;
        public const int THUMBNAILHEIGHT = THUMBNAILWIDTH * IMAGEHEIGHT / IMAGEWIDTH;


        public enum CodisExclusio
        {
            Inclos,
            Canal,
            Exclusives,
            PremiumLine,
            Obsolet,
            OutOfCatalog
        }

        public enum Wellknowns
        {
            none,
            mamaRoo,
            kidfix,
            ManoDeObraSinCargo,
            ManoDeObra,
            MaterialEmpleado,
            Embalaje,
            Ports,
            Transport,
            LogisticCharges,
            UnknownSku,
            ReferenciaEspecial
        }

        public enum BundleCods
        {
            none,
            parent,
            child
        }



        public DTOProductSku() : base()
        {
            base.SourceCod = SourceCods.Sku;
            NomLlarg = new DTOLangText(base.Guid, DTOLangText.Srcs.SkuNomLlarg);
            BundleSkus = new List<DTOSkuBundle>();
        }

        public DTOProductSku(Guid oGuid) : base(oGuid)
        {
            base.SourceCod = SourceCods.Sku;
            NomLlarg = new DTOLangText(base.Guid, DTOLangText.Srcs.SkuNomLlarg);
            BundleSkus = new List<DTOSkuBundle>();
        }


        public static DTOProductSku Factory(DTOUser oUser, DTOProductCategory oCategory)
        {
            DTOProductSku retval = new DTOProductSku();
            {
                var withBlock = retval;
                withBlock.Category = oCategory;
                withBlock.IvaCod = DTOTax.Codis.iva_Standard;
                withBlock.UsrLog = DTOUsrLog.Factory(oUser);
            }
            return retval;
        }

        public static DTOProductSku Wellknown(DTOProductSku.Wellknowns owellknown)
        {
            DTOProductSku retval = null;
            switch (owellknown)
            {
                case DTOProductSku.Wellknowns.mamaRoo:
                    {
                        retval = new DTOProductSku(new Guid("135FE602-D139-4013-8F2B-ACA69D7BC5FF"));
                        break;
                    }

                case DTOProductSku.Wellknowns.kidfix:
                    {
                        retval = new DTOProductSku(new Guid("C0CAEF04-7D25-42FF-B1CB-7277E2EA1258"));
                        break;
                    }

                case DTOProductSku.Wellknowns.ManoDeObraSinCargo:
                    {
                        retval = new DTOProductSku(new Guid("F6445139-4BD3-426F-90AF-95C0CE13D413"));
                        break;
                    }

                case DTOProductSku.Wellknowns.ManoDeObra:
                    {
                        retval = new DTOProductSku(new Guid("21D9ADAD-06F2-408C-824F-C4622F6EE362"));
                        break;
                    }

                case DTOProductSku.Wellknowns.MaterialEmpleado:
                    {
                        retval = new DTOProductSku(new Guid("08B96F83-1C07-43F8-B2F5-3D8AA26F1C1F"));
                        break;
                    }

                case DTOProductSku.Wellknowns.Embalaje:
                    {
                        retval = new DTOProductSku(new Guid("B2C01147-8048-4430-A9B7-74FF3E4EC0C3"));
                        break;
                    }

                case DTOProductSku.Wellknowns.Ports:
                    {
                        retval = new DTOProductSku(new Guid("50b4dc15-a957-498a-bd95-c47bc75394f2"));
                        break;
                    }

                case DTOProductSku.Wellknowns.Transport:
                    {
                        retval = new DTOProductSku(new Guid("210EE215-A1D4-44F0-A134-7E372F5F5A26"));
                        break;
                    }

                case DTOProductSku.Wellknowns.LogisticCharges:
                    {
                        retval = new DTOProductSku(new Guid("63456678-9AF3-4330-8CCC-6A7927A79BE1"));
                        break;
                    }

                case DTOProductSku.Wellknowns.UnknownSku:
                    {
                        retval = new DTOProductSku(new Guid("2FFE0A89-82ED-4A79-8D7A-CFC53693AEF3"));
                        break;
                    }

                case Wellknowns.ReferenciaEspecial:
                    {
                        retval = new DTOProductSku(new Guid("72BE21F1-4B37-4C4C-BEBC-72C417401D28"));
                        break;
                    }
            }
            return retval;
        }


        public string NomProveidorNoRef()
        {
            string retval = NomProveidor;
            if (!string.IsNullOrEmpty(RefProveidor))
            {
                if (retval.isNotEmpty() && retval.StartsWith(RefProveidor))
                    retval = retval.Replace(RefProveidor, "").Trim();
            }
            return retval;
        }


        public bool IsChildOf(DTOProduct oProduct)
        {
            bool retval = base.Guid.Equals(oProduct.Guid);
            if (retval == false && this.Category != null)
            {
                retval = this.Category.Equals(oProduct);
                if (retval == false && this.Category.Brand != null)
                {
                    retval = this.Category.Brand.Equals(oProduct);
                }
            }
            return retval;
        }

        public static Color BackColor(DTOProductSku oSku)
        {
            Color retval ;
            if (oSku.Stock > 0)
            {
                if (oSku.Stock > oSku.Clients)
                    retval = Color.LightGreen;
                else
                    retval = Color.Yellow;
            }
            else if (oSku.obsoleto)
                retval = Color.LightGray;
            else
                retval = Color.LightSalmon;
            return retval;
        }

        public static Color BackColor(int stock, int clients)
        {
            Color retval;
            if (stock > 0)
            {
                if (stock > clients)
                    retval = Color.LightGreen;
                else
                    retval = Color.Yellow;
            }
            else
                retval = Color.LightSalmon;
            return retval;
        }

        public bool WarnSupplier()
        {
            bool retval = false;
            if (Stock < Clients)
            {
                retval = true;
            }
            return retval;
        }

        public static int Moq(DTOProductSku oSku)
        {
            int retval = 1;
            if (oSku != null)
            {

                if (oSku.HeredaDimensions && oSku.Category != null)
                {
                    if (oSku.Category.ForzarInnerPack)
                        retval = oSku.Category.InnerPack;
                }
                else if (oSku.ForzarInnerPack)
                {
                    if (oSku.InnerPack > 0)
                        retval = oSku.InnerPack;
                }
            }
            return retval;
        }

        public static DTOCliProductDto GetCliProductDto(DTOProductSku oSku, List<DTOCliProductDto> oDtos)
        {
            // If oSku.Category Is Nothing Then Stop

            DTOCliProductDto retval = null;
            if (oSku != null & oDtos.Count > 0)
            {
                retval = oDtos.Find(x => x.Product.Equals(oSku));
                if (retval == null & oSku.Category != null)
                {
                    retval = oDtos.Find(x => x.Product.Equals(oSku.Category));
                    if (retval == null & oSku.Category.Brand != null)
                        retval = oDtos.Find(x => x.Product.Equals(oSku.Category.Brand));
                }
            }
            return retval;
        }


        public DTOAmt GetCustomerCost(List<DTOPricelistItemCustomer> oCustomCosts, List<DTOCustomerTarifaDto> oDtos)
        {
            DTOAmt retval = null;
            //prova si existeix tarifa personalitzada d'aquest client per aquest article
            DTOPricelistItemCustomer oCustomCost = oCustomCosts.Find(x => x.Sku.Guid.Equals(base.Guid));
            if (oCustomCost == null)
            {
                //si no n'hi ha, busca a la tarifa general i dedueix el descompte que pugui tenir
                Decimal DcCost = 0;
                if (this.Rrpp != null)
                {
                    Decimal rrpp = this.Rrpp.Eur;
                    this.DtoSobreRRPP = null;
                    DTOCustomerTarifaDto oDto = getCustomerDto(oDtos, this);
                    if (oDto == null)
                        DcCost = rrpp;
                    else
                    {
                        this.DtoSobreRRPP = oDto;
                        DcCost = Math.Round(rrpp * (100 - this.DtoSobreRRPP.Dto) / 100, 2, MidpointRounding.AwayFromZero);
                    }


                }
                retval = DTOAmt.Factory(DcCost);
            }
            else

            {
                retval = oCustomCost.Retail;
            }
            return retval;
        }


        public int stockAvailable()
        {
            int retval = Stock - (Clients - ClientsAlPot - ClientsEnProgramacio);
            if (retval < 0)
                retval = 0;
            return retval;
        }

        public static string Ean(DTOProductSku oSku)
        {
            string retval = "";
            if (oSku != null)
            {
                if (oSku.Ean13 != null)
                    retval = oSku.Ean13.Value;
            }
            return retval;
        }


        public static string urlSegment(DTOProductSku oSku)
        {
            string retval = "";
            if (oSku != null)
            {
                if (!string.IsNullOrEmpty(oSku.nomCurtOrNom()))
                    retval = MatHelperStd.UrlHelper.EncodedUrlSegment(oSku.nomCurtOrNom().ToLower());
            }
            return retval;
        }

        public string defaultUrl()
        {
            string retval = "";
            if (Category != null && Category.Brand != null)
                retval = MmoUrl.Factory(true, Category.Brand.urlSegment(), Category.urlSegment(), urlSegment(this));
            return retval;
        }

        public string urlOrDefault()
        {
            string retval = string.IsNullOrEmpty(Url) ? defaultUrl() : Url;
            return retval;
        }

        public string imageUrl(bool AbsoluteUrl = false)
        {
            return MmoUrl.image(Defaults.ImgTypes.art, base.Guid, AbsoluteUrl);
        }

        public string thumbnailUrl()
        {
            return MmoUrl.ApiUrl("productSku/thumbnail", base.Guid.ToString());
        }


        public string nomCurtOrNom()
        {
            string retval = Nom.Esp;
            if (retval == "")
                retval = NomLlarg.Esp;
            return retval;
        }

        public static string nomPrvOrMyd(DTOProductSku oSku)
        {
            string retval = oSku.NomProveidor;
            if (retval == "")
                retval = oSku.NomLlarg.Esp;
            return retval;
        }

        public DTOLangText RefYNomLlarg()
        {
            var retval = NomLlarg.Clone();
            if (!string.IsNullOrEmpty(retval.Esp) && !string.IsNullOrEmpty(RefProveidor) && !retval.Esp.Contains(RefProveidor))
                retval.Esp = string.Format("{0} {1}", RefProveidor, NomLlarg.Esp);
            if (!string.IsNullOrEmpty(retval.Cat) && !string.IsNullOrEmpty(RefProveidor) && !retval.Cat.Contains(RefProveidor))
                retval.Cat = string.Format("{0} {1}", RefProveidor, NomLlarg.Cat);
            if (!string.IsNullOrEmpty(retval.Eng) && !string.IsNullOrEmpty(RefProveidor) && !retval.Eng.Contains(RefProveidor))
                retval.Eng = string.Format("{0} {1}", RefProveidor, NomLlarg.Eng);
            if (!string.IsNullOrEmpty(retval.Por) && !string.IsNullOrEmpty(RefProveidor) && !retval.Por.Contains(RefProveidor))
                retval.Por = string.Format("{0} {1}", RefProveidor, NomLlarg.Por);
            return retval;
        }

        public static string refYNomPrv(DTOProductSku oSku)
        {
            string sNom = oSku.NomProveidor;
            string sRef = oSku.RefProveidor;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (sRef.isNotEmpty())
            {
                if (!sNom.Contains(sRef))
                {
                    sb.Append(sRef);
                    sb.Append(" ");
                }
            }
            sb.Append(sNom);
            if (sb.Length == 0)
                sb.Append(DTOProductSku.nomPrvOrMyd(oSku));
            string retval = sb.ToString();
            return retval;
        }

        public static string refPrvOrSkuid(DTOProductSku oSku)
        {
            string retval = oSku.RefProveidor;
            if (string.IsNullOrEmpty(retval))
                retval = oSku.Id.ToString();
            return retval;
        }


        public DTOCliProductDto CliProductDto(List<DTOCliProductDto> oDtos)
        {
            // If oSku.Category Is Nothing Then Stop

            DTOCliProductDto retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oDtos.Count > 0)
            {
                retval = oDtos.Find(x => x.Product.Guid.Equals(base.Guid));
                if (retval == null & Category != null)
                {
                    retval = oDtos.Find(x => x.Product.Equals(Category));
                    if (retval == null & Category.Brand != null)
                        retval = oDtos.Find(x => x.Product.Equals(Category.Brand));
                }
            }
            return retval;
        }

        public string brandNom()
        {
            string retval = "";
            if (Category != null && Category.Brand != null)
                retval = Category.Brand.Nom.Esp;
            return retval;
        }

        public string categoryNom()
        {
            string retval = "";
            if (Category != null)
                retval = Category.Nom.Esp;
            return retval;
        }


        public string refYNomPrv()
        {
            string sNom = NomProveidor;
            string sRef = RefProveidor;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (sRef.isNotEmpty())
            {
                if (!sNom.Contains(sRef))
                {
                    sb.Append(sRef);
                    sb.Append(" ");
                }
            }
            sb.Append(sNom);
            if (sb.Length == 0)
                sb.Append(nomPrvOrMyd());
            string retval = sb.ToString();
            return retval;
        }

        public static string FullNom(DTOProductSku oSku)
        {
            string retval = oSku.NomLlarg.Esp;
            if (retval == "")
            {
                if (oSku.Category != null)
                {
                    var sCategory = oSku.Category.Nom.Esp;
                    if (oSku.Category.Brand != null)
                    {
                        string sBrand = oSku.Category.Brand.Nom.Esp;
                        retval = string.Format("{0} {1} {2}", sBrand, sCategory, oSku.Nom.Esp);
                    }
                }
            }
            if (retval == "")
                retval = oSku.Nom.Esp;
            if (retval == "")
                retval = DTOProductSku.refYNomPrv(oSku);
            return retval;
        }


        public string nomPrvAndRefOrMyd()
        {
            string retval = NomProveidor;
            if (string.IsNullOrEmpty(retval))
                retval = NomLlarg.Esp;
            else if (!string.IsNullOrEmpty(RefProveidor))
                retval = RefProveidor + " " + retval;
            return retval;
        }

        public string nomPrvOrMyd()
        {
            string retval = NomProveidor;
            if (string.IsNullOrEmpty(retval))
                retval = NomLlarg.Esp;
            return retval;
        }

        public string ManufacturerAvailabilityText(DTOLang lang)
        {
            string retval = lang.Tradueix("en producción", "en producció", "in production");
            if (this.obsoleto)
            {
                retval = lang.Tradueix("obsoleto", "obsolet", "outdated");
            }
            else if (this.LastProduction)
            {
                retval = lang.Tradueix("últimas unidades", "últimes unitats", "last units");

            }
            else if (this.Availability > DTO.GlobalVariables.Today())
            {
                retval = lang.Tradueix("no disponible hasta", "no disponible fins", "not available until") + " " + this.Availability.ToString("dd/MM/yy");
            }

            return retval;
        }

        public int innerPackOrInherited()
        {
            int retval = InnerPack;
            if (HeredaDimensions & Category != null)
                retval = Category.InnerPack;
            if (retval < 1)
                retval = 1;
            return retval;
        }

        public int Moq()
        {
            int retval = 1;
            if (selfOrInheritedForzarInnerPack(this))
            {
                retval = innerPackOrInherited();
            }
            return retval;
        }

        public static int InnerPackOrInherited(DTOProductSku oSku)
        {
            int retval = 1;
            if (oSku != null)
                retval = oSku.innerPackOrInherited();
            return retval;
        }

        public static bool selfOrInheritedForzarInnerPack(DTOProductSku oSku)
        {
            bool retval;
            if (oSku.HeredaDimensions)
                retval = oSku.Category.ForzarInnerPack;
            else
                retval = oSku.ForzarInnerPack;
            return retval;
        }


        public static int OuterPackOrInherited(DTOProductSku oSku)
        {
            int retval = 0;
            if (oSku != null)
                retval = oSku.OuterPackOrInherited();
            return retval;
        }

        public int OuterPackOrInherited()
        {
            int retval = InnerPack; // temporary *********************************************
            if (HeredaDimensions & Category != null)
                retval = Category.InnerPack;
            if (retval < 1)
                retval = 1;
            return retval;
        }

        public DTOCountry madeInOrInherited()
        {
            DTOCountry retval = MadeIn;
            if (retval == null & Category != null)
            {
                retval = Category.MadeIn;
                if (retval == null & Category.Brand != null)
                    retval = Category.Brand.MadeIn;
            }
            return retval;
        }
        public string madeInOrInheritedISO()
        {
            DTOCountry oCountry = MadeIn;
            if (oCountry == null & Category != null)
            {
                oCountry = Category.MadeIn;
                if (oCountry == null & Category.Brand != null)
                    oCountry = Category.Brand.MadeIn;
            }
            string retval = "";
            if (oCountry != null)
                retval = oCountry.ISO;
            return retval;
        }

        public static string madeInOrInheritedISO(DTOProductSku oSku)
        {
            string retval = "";
            if (oSku != null)
                retval = oSku.madeInOrInheritedISO();
            return retval;
        }

        public int DimensionLOrInherited()
        {
            int retval = HeredaDimensions ? Category.DimensionL : DimensionL;
            return retval;
        }

        public int DimensionHOrInherited()
        {
            int retval = HeredaDimensions ? Category.DimensionH : DimensionH;
            return retval;
        }

        public int DimensionWOrInherited()
        {
            int retval = HeredaDimensions ? Category.DimensionW : DimensionW;
            return retval;
        }
        public decimal VolumeM3OrInherited()
        {
            decimal retval;
            retval = VolumeM3;
            if (HeredaDimensions)
            {
                if (Category != null)
                {
                    DTOProductCategory oCategory = Category;
                    retval = oCategory.VolumeM3;
                    if (retval == 0)
                        retval = (decimal)(oCategory.DimensionL * oCategory.DimensionW * oCategory.DimensionH) / (decimal)1000000000;
                    if (oCategory.InnerPack > 1)
                        retval = retval / (decimal)oCategory.InnerPack;
                }
            }
            else
            {
                retval = VolumeM3;
                if (retval == 0)
                    retval = ((decimal)DimensionL * (decimal)DimensionW * (decimal)DimensionH) / 1000000000;
                if (InnerPack > 1)
                    retval = retval / InnerPack;
            }

            return retval;
        }

        public static decimal volumeM3OrInherited(DTOProductSku oSku)
        {
            decimal retval = 0;
            if (oSku != null)
                retval = oSku.VolumeM3OrInherited();
            return retval;
        }

        public static decimal DimensionLOrInherited(DTOProductSku oSku)
        {
            decimal retval = 0;
            if (oSku != null)
                retval = oSku.DimensionLOrInherited();
            return retval;
        }

        public static decimal DimensionWOrInherited(DTOProductSku oSku)
        {
            decimal retval = 0;
            if (oSku != null)
                retval = oSku.DimensionWOrInherited();
            return retval;
        }

        public static decimal DimensionHOrInherited(DTOProductSku oSku)
        {
            decimal retval = 0;
            if (oSku != null)
                retval = oSku.DimensionHOrInherited();
            return retval;
        }


        public decimal weightKgOrInherited()
        {
            decimal retval = KgBrut;
            if (HeredaDimensions & Category != null)
                retval = Category.KgBrut;
            return retval;
        }

        public static decimal weightKgOrInherited(DTOProductSku oSku)
        {
            decimal retval = 0;
            if (oSku != null)
                retval = oSku.weightKgOrInherited();
            return retval;
        }


        public static decimal kgNetOrInheritedOrBrut(DTOProductSku oSku)
        {
            decimal retval = DTOProductSku.kgNetOrInherited(oSku);
            if (retval == 0)
                retval = oSku.weightKgOrInherited();
            return retval;
        }

        public static decimal kgNetOrInherited(DTOProductSku oSku)
        {
            decimal retval = oSku.HeredaDimensions ? oSku.Category.KgNet : oSku.KgNet;
            return retval;
        }

        public static DTOCountry madeInOrInherited(DTOProductSku oSku)
        {
            DTOCountry retval = oSku.MadeIn;
            if (retval == null & oSku.Category != null)
            {
                retval = oSku.Category.MadeIn;
                if (retval == null & oSku.Category.Brand != null)
                    retval = oSku.Category.Brand.MadeIn;
            }
            return retval;
        }



        public bool includeCategoryWithAccessoryNom()
        {
            bool retval = false;
            if (Category != null)
            {
                if (Category.Codi == DTOProductCategory.Codis.accessories)
                {
                    if (!Category.Nom.Esp.ToLower().StartsWith("acces"))
                        retval = true;
                }
            }
            return retval;
        }

        public DTOCodiMercancia codiMercanciaOrInherited()
        {
            DTOCodiMercancia retval;
            if (CodiMercancia == null || !VbUtilities.isNumeric(CodiMercancia.Id))
                retval = Category.CodiMercancia;
            else
                retval = CodiMercancia;
            return retval;
        }

        public string CodiMercanciaNomOrInherited()
        {
            string retval = "";
            var oCodiMercancia = codiMercanciaOrInherited();
            if (oCodiMercancia != null)
                retval = oCodiMercancia.Dsc;
            return retval;
        }

        public string CodiMercanciaIdOrInherited()
        {
            string retval = "";
            var oCodiMercancia = codiMercanciaOrInherited();
            if (oCodiMercancia != null)
                retval = oCodiMercancia.Id;
            return retval;
        }

        public static DTOCodiMercancia CodiMercanciaOrInherited(DTOProductSku oSku)
        {
            DTOCodiMercancia retval;
            if (oSku.CodiMercancia == null || !VbUtilities.isNumeric(oSku.CodiMercancia.Id))
                retval = DTOProductCategory.CodiMercanciaOrInherited(oSku.Category);
            else
                retval = oSku.CodiMercancia;
            return retval;
        }

        public static bool IsTruncatedStock(DTOProductSku oSku, DTORol oRol)
        {
            bool retval = false;
            int iDisplayableClients = oSku.Clients - oSku.ClientsAlPot - oSku.ClientsEnProgramacio;
            int iAvailableStock = oSku.Stock > iDisplayableClients ? oSku.Stock - iDisplayableClients : 0;
            switch (oRol.id)
            {
                case DTORol.Ids.superUser:
                case DTORol.Ids.salesManager:
                case DTORol.Ids.rep:
                case DTORol.Ids.comercial:
                case DTORol.Ids.manufacturer:
                    {
                        break;
                    }

                default:
                    {
                        retval = (iAvailableStock > 10);
                        break;
                    }
            }
            return retval;
        }

        public bool IsAvailable()
        {
            bool retval = false;
            if (base.Guid.Equals(DTOProductSku.Wellknown(Wellknowns.ReferenciaEspecial).Guid))
            {
            }
            else if (this.Stock > 0)
                retval = this.Stock > this.Clients;
            return retval;
        }

        public static int StockAvailable(DTOProductSku oSku)
        {
            int retval = oSku.Stock - (oSku.Clients - oSku.ClientsAlPot - oSku.ClientsEnProgramacio);
            if (retval < 0)
                retval = 0;
            return retval;
        }


        public static string TruncatedStockValue(DTOProductSku oSku, DTORol oRol)
        {
            string retval;
            int iStk = TruncatedStock(oSku, oRol);

            if (DTOProductSku.IsTruncatedStock(oSku, oRol))
                retval = string.Format("{0}+", iStk);
            else
                retval = iStk.ToString();

            return retval;
        }

        public static int TruncatedStock(DTOProductSku oSku, DTORol oRol)
        {
            int retval;
            int iDisplayableClients = oSku.Clients - oSku.ClientsAlPot - oSku.ClientsEnProgramacio;
            int iAvailableStock = oSku.Stock > iDisplayableClients ? oSku.Stock - iDisplayableClients : 0;

            switch (oRol.id)
            {
                case DTORol.Ids.superUser:
                case DTORol.Ids.salesManager:
                case DTORol.Ids.rep:
                case DTORol.Ids.comercial:
                case DTORol.Ids.manufacturer:
                    {
                        retval = iAvailableStock;
                        break;
                    }

                default:
                    {
                        retval = iAvailableStock > 10 ? 10 : iAvailableStock;
                        break;
                    }
            }
            return retval;
        }


        public static List<DTOIncentiu> Incentius(DTOProduct oProduct, List<DTOIncentiu> oSource)
        {
            ;/* Cannot convert LocalDeclarationStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertInitializer(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.SplitVariableDeclarations(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 
        Dim retval As List(Of DTOIncentiu) = (From i In oSource
                                              From p In i.Products
                                              Where p.Equals(oProduct)
                                              Select i).ToList

 */
            //TODO
            return new List<DTOIncentiu>();
        }

        public static bool IsHidden(DTOProductSku oSku)
        {
            // Amaga-ho al consumidor i a les comandes de professional fins a la data HideUntil mes llunyana
            DateTime SkuHideUntil = oSku.HideUntil;
            DateTime CategoryHideUntil = oSku.Category.HideUntil;
            bool retval = SkuHideUntil > DTO.GlobalVariables.Today();
            if (CategoryHideUntil > DTO.GlobalVariables.Today())
                retval = true;
            return retval;
        }

        public static string SelfOrInheritedCnapFullNom(DTOProductSku oSku, DTOLang oLang)
        {
            string retval = "";
            if (oSku != null)
            {
                if (oSku.Cnap == null)
                    retval = DTOProductCategory.SelfOrInheritedCnapFullNom(oSku.Category, oLang);
                else
                    retval = oSku.Cnap.FullNom(oLang);
            }
            return retval;
        }

        public static List<DTOProductCategory> Categories(IEnumerable<DTOProductSku> oSkus)
        {
            List<DTOProductCategory> retval = new List<DTOProductCategory>();
            if (oSkus != null)
                retval = oSkus.GroupBy(x => x.Category.Guid).Select(y => y.First()).Select(z => z.Category).ToList();
            return retval;
        }

        public static DTOAmt GetCustomerCost(DTOProductSku oSku, List<DTOCustomerTarifaDto> oDtos, DateTime DtFch = default)
        {
            DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;

            if (oSku.Rrpp != null)
            {
                if (DtFch == default)
                    DtFch = DTO.GlobalVariables.Today();
                decimal DcDto = DTOCustomerTarifaDto.ProductDto(oDtos, oSku); // oSku.Category.Brand)
                decimal DcCost = Math.Round(oSku.Rrpp.Eur * (100 - DcDto) / 100, 2, MidpointRounding.AwayFromZero);
                retval = DTOAmt.Factory(DcCost);
            }
            return retval;
        }

        public static DTOAmt getCustomerCost(DTOProductSku oSku, List<DTOPricelistItemCustomer> oCustomCosts, List<DTOCustomerTarifaDto> oDtos)
        {
            DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;

            // prova si existeix tarifa personalitzada d'aquest client per aquest article
            DTOPricelistItemCustomer oCustomCost = oCustomCosts.Find(x => x.Sku.Equals(oSku));
            if (oCustomCost == null)
            {
                // si no n'hi ha, busca a la tarifa general i dedueix el descompte que pugui tenir

                decimal DcCost = 0;

                if (oSku.Rrpp != null)
                {
                    decimal RRPP = oSku.Rrpp.Eur;
                    oSku.DtoSobreRRPP = null;
                    DTOCustomerTarifaDto oDto = getCustomerDto(oDtos, oSku);
                    if (oDto == null)
                        DcCost = RRPP;
                    else
                    {
                        oSku.DtoSobreRRPP = oDto;
                        DcCost = Math.Round(RRPP * (100 - oSku.DtoSobreRRPP.Dto) / 100, 2, MidpointRounding.AwayFromZero);
                    }
                }

                retval = DTOAmt.Factory(DcCost);
            }
            else
                retval = oCustomCost.Retail;

            return retval;
        }

        public static DTOCustomerTarifaDto getCustomerDto(List<DTOCustomerTarifaDto> oDtos, DTOProductSku oSku)
        {
            DTOCustomerTarifaDto retval = oDtos.Find(x => oSku.Equals(x.Product));
            if (retval == null)
            {
                retval = oDtos.Find(x => oSku.Category.Equals(x.Product));
                if (retval == null)
                {
                    retval = oDtos.Find(x => oSku.Category.Brand.Equals(x.Product));
                    if (retval == null)
                        retval = oDtos.Find(x => x.Product == null);
                }
            }
            return retval;
        }

        public static DTOGuidNom toGuidNom(DTOProductSku oSku)
        {
            DTOGuidNom retval = new DTOGuidNom(oSku.Guid, DTOProductSku.FullNom(oSku));
            return retval;
        }

        public static List<DTOGuidNom> toGuidNoms(IEnumerable<DTOProductSku> oSkus)
        {
            List<DTOGuidNom> retval = new List<DTOGuidNom>();
            foreach (var oSku in oSkus)
            {
                var item = DTOProductSku.toGuidNom(oSku);
                retval.Add(item);
            }
            return retval;
        }

        public bool Matches(string searchkey)
        {
            bool retval = false;
            searchkey = searchkey.ToLower();
            var searchterms = searchkey.Split('+');
            if(searchterms.Length == 1)
            {
            if (Nom != null & !string.IsNullOrEmpty(Nom.Esp) & Nom.Esp.ToLower().Contains(searchkey))
                retval = true;
            else if (NomLlarg != null & !string.IsNullOrEmpty(NomLlarg.Esp) && NomLlarg.Esp.ToLower().Contains(searchkey))
                retval = true;
            else if (!string.IsNullOrEmpty(RefProveidor) && RefProveidor.ToLower().Contains(searchkey))
                retval = true;
            else if (!string.IsNullOrEmpty(NomProveidor) && NomProveidor.ToLower().Contains(searchkey))
                retval = true;
            else if (Id.ToString() == searchkey)
                retval = true;
            else if (Ean13 != null)
                if (Ean13.Value != null)
                    if (Ean13.Value == searchkey)
                        retval = true;

            } else
            {
                retval = true;
                if (NomLlarg == null || string.IsNullOrEmpty(NomLlarg.Esp)) {
                    retval = false;
                } 
                else { 
                foreach (string searchterm in searchterms)
                    {
                        if(!NomLlarg.Esp.ToLower().Contains(searchterm))
                        {
                            retval = false;
                            break;
                        }
                    }
                }
            }
            return retval;
        }

        public static string SpriteUrl(DTOProductCategory oCategory, int itemWidth)
        {
            return MmoUrl.ApiUrl("ProductSkus/sprite", oCategory.Guid.ToString(), itemWidth.ToString());
        }


        public class Switch : DTOBaseGuid
        {
            public DTOProductSku skuFrom { get; set; }
            public DTOProductSku skuTo { get; set; }
            public DateTime fch { get; set; }
        }

        public Model ViewModel(DTOLang lang)
        {
            Model retval = new Model
            {
                Guid = this.Guid,
                Id = this.Id,
                Brand = new DTOBasicCatalog.Brand(this.Category.Brand.Guid, this.Category.Brand.Nom.Tradueix(lang)),
                Category = new DTOBasicCatalog.Category(this.Category.Guid, this.Category.Nom.Tradueix(lang)),
                NomEsp = this.Nom.Tradueix(lang),
                NomLlarg = this.NomLlarg.Tradueix(lang),
                Ean = DTOProductSku.Ean(this),
                Ref = this.RefProveidor,
                LastProduction = this.LastProduction,
                Availability = this.Availability,
                Obsoleto = this.obsoleto,
                FchObsoleto = this.FchObsoleto.ToString("o"),
                ReadOnly = true,
                ImageUrl = this.imageUrl(false)
            };
            return retval;
        }

        public new class Collection : List<DTOProductSku>
        {

        }

        public class Treenode
        {
            public Guid Guid { get; set; }
            public int Id { get; set; }
            public DTOLangText.Compact Nom { get; set; }
            public DTOLangText.Compact NomLlarg { get; set; }
            public string NomProveidor { get; set; }
            public string RefProveidor { get; set; }
            public string RefCustomer { get; set; }
            public bool HeredaDimensions { get; set; }
            public int DimensionW { get; set; }
            public int DimensionL { get; set; }
            public int DimensionH { get; set; }
            public Decimal KgBrut { get; set; }
            public Decimal KgNet { get; set; }
            public int InnerPack { get; set; }
            public bool ForzarInnerPack { get; set; }
            public DTOEan Ean13 { get; set; }
            public DTOEan PackageEan { get; set; }
            public DTOAmt.Compact Price { get; set; }
            public DTOAmt.Compact Rrpp { get; set; }
            public Decimal CustomerDto { get; set; }
            public bool ImageExists { get; set; }
            public bool Obsoleto { get; set; }
            public DTOCodiMercancia CodiMercancia { get; set; }
            public DTOCountry.Compact MadeIn { get; set; }
            public int Stock { get; set; }
            public int Clients { get; set; }

            public DTOUrl UrlCanonicas { get; set; }

            public static Treenode Factory(Guid guid)
            {
                Treenode retval = new Treenode
                {
                    Guid = guid
                };
                return retval;
            }

            public DTOProductSku ToProductSku(DTOProductCategory category = null)
            {
                var retval = new DTOProductSku(this.Guid)
                {
                    Category = category,
                    Id = this.Id,
                    Nom = this.Nom.toLangText(),
                    NomLlarg = this.NomLlarg.toLangText(),
                    NomProveidor = this.NomProveidor,
                    RefProveidor = this.RefProveidor,
                    RefCustomer = this.RefCustomer,
                    HeredaDimensions = this.HeredaDimensions,
                    DimensionW = this.DimensionW,
                    DimensionL = this.DimensionL,
                    DimensionH = this.DimensionH,
                    KgBrut = this.KgBrut,
                    KgNet = this.KgNet,
                    InnerPack = this.InnerPack,
                    ForzarInnerPack = this.ForzarInnerPack,
                    Ean13 = this.Ean13,
                    PackageEan = this.PackageEan,
                    UrlCanonicas = this.UrlCanonicas
                };
                if (this.Price != null)
                {
                    retval.Price = this.Price.ToAmt();
                }
                if (this.Rrpp != null)
                {
                    retval.Rrpp = this.Rrpp.ToAmt();
                }

                retval.CustomerDto = this.CustomerDto;
                retval.ImageExists = this.ImageExists;
                retval.CodiMercancia = this.CodiMercancia;
                retval.RefCustomer = this.RefCustomer;
                if (this.MadeIn != null)
                {
                    retval.MadeIn = new DTOCountry(this.MadeIn.Guid);
                }
                retval.Stock = this.Stock;
                retval.Clients = this.Clients;
                return retval;
            }

            public override string ToString()
            {
                string retval = "ProductSku.Treenode Guid:" + this.Guid.ToString();
                if (this.NomLlarg != null)
                    retval += " " + this.NomLlarg.Esp;
                else if (this.Nom != null)
                    retval += " " + this.Nom.Esp;

                return retval;
            }
        }

        public class Model
        {
            public Guid Guid { get; set; }
            public int Id { get; set; }
            public DTOBasicCatalog.Brand Brand { get; set; }
            public DTOBasicCatalog.Category Category { get; set; }
            public string NomEsp { get; set; }
            public string NomLlarg { get; set; }
            public string NomProveidor { get; set; }
            public string Ean { get; set; }
            public string Ref { get; set; }
            public bool LastProduction { get; set; }
            public DateTime Availability { get; set; }

            public string FchObsoleto { get; set; }
            public bool Obsoleto { get; set; }

            public string ImageUrl { get; set; }
            public bool ReadOnly { get; set; }


            public string ManufacturerAvailabilityText(DTOLang lang)
            {
                string retval = lang.Tradueix("en producción", "en producció", "in production");
                if (this.Obsoleto)
                {
                    retval = lang.Tradueix("obsoleto", "obsolet", "outdated");
                    if (FchObsoleto != null)
                    {
                        retval = string.Format("{0} {1} {2}", retval, lang.Tradueix("desde", "des del", "from"), FchObsoleto);
                    }
                }
                else if (this.LastProduction)
                {
                    retval = lang.Tradueix("últimas unidades", "últimes unitats", "last units");

                }
                else if (this.Availability > DTO.GlobalVariables.Today())
                {
                    retval = lang.Tradueix("no disponible hasta", "no disponible fins", "not available until") + " " + this.Availability.ToString("dd/MM/yy");
                }

                return retval;
            }
        }
    }
}
