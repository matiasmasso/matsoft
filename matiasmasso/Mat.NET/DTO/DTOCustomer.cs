using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOCustomer : DTOContact
    {
        public bool Iva { get; set; }
        public bool Req { get; set; }
        public PortsCodes PortsCod { get; set; }
        public DTOPortsCondicio PortsCondicions { get; set; }
        public CashCodes CashCod { get; set; }
        public List<DTOCliProductDto> ProductDtos { get; set; }
        public List<DTOCustomerTarifaDto> TarifaDtos { get; set; }
        public bool NoWeb { get; set; }
        public bool WebAtlasPriority { get; set; }
        public bool NoRep { get; set; }
        public bool NoRaffles { get; set; }
        public bool OrdersToCentral { get; set; }
        public bool NoIncentius { get; set; }
        public DTOCustomerPlatform DeliveryPlatform { get; set; }
        public DTOCustomer Ccx { get; set; } // Invoice destination
        public string Ref { get; set; } // Label to distinguish between outlets of same organization
        public bool AlbValorat { get; set; }
        public string WarnAlbs { get; set; }
        public DTOPaymentTerms PaymentTerms { get; set; }
        public bool FpgIndependent { get; set; }

        public string HorarioEntregas { get; set; }

        public new DTOInvoice.ExportCods ExportCod { get; set; }
        public DTOIncoterm Incoterm { get; set; }
        public string SuProveedorNum { get; set; }

        public bool MostrarEANenFactura { get; set; }
        public FraPrintModes FraPrintMode { get; set; }
        public DTOContactClass Channel { get; set; }

        public DTOCustomerCluster CustomerCluster { get; set; }
        public Clusters Cluster { get; set; }
        public DTOHolding Holding { get; set; }


        public new bool IsLoaded { get; set; }

        public enum Wellknowns
        {
            notSet,
            elCorteIngles,
            eciga,
            amazon,
            sonae,
            carrefour,
            prenatal,
            prenatalPortugal,
            prenatalTenerife,
            miFarma,
            bebitus,
            javierBayon,
            tradeInn,
            consumidor,
            worten,
            promofarma
        }

        public enum CodsAlbsXFra
        {
            notSet,
            unaSolaFraMensual,
            juntarAlbs,
            juntarAlbsPetits,
            fraPerAlbara
        }

        public enum PortsCodes
        {
            notSet,
            pagats,
            deguts,
            reculliran,
            altres,
            entregatEnMa
        }



        public enum CashCodes
        {
            notSet,
            credit,
            reembols,
            transferenciaPrevia,
            visa,
            diposit
        }

        public enum CreditStatus
        {
            suficient,
            excedit,
            caducat,
            retirat,
            impagats,
            neverLogged
        }

        public enum Tarifas
        {
            notSet,
            standard,
            @virtual,
            fiftyFifty,
            pvp
        }

        public enum FraPrintModes
        {
            notSet,
            noPrint,
            printer,
            email,
            edi
        }

        public enum Clusters
        {
            notSet,
            A,
            B,
            C,
            D,
            E,
            F
        }

        public enum GroupLevels
        {
            single // Una rao social amb un sol punt de venda
    ,
            chain    // Una rao social multiples punts de venda
    ,
            holding  // multiples raons socials cada una amb els seus punts de venda
        }

        public DTOCustomer() : base()
        {
        }

        public DTOCustomer(Guid oGuid) : base(oGuid)
        {
        }



        public static DTOCustomer FromContact(DTOContact oContact)
        {
            DTOCustomer retval = null;
            if (oContact != null)
            {
                if (oContact is DTOCustomer)
                    retval = (DTOCustomer)oContact;
                else
                {
                    retval = new DTOCustomer(oContact.Guid);
                    {
                        var withBlock = retval;
                        withBlock.Emp = oContact.Emp;
                        withBlock.Nom = oContact.Nom;
                        withBlock.NomComercial = oContact.NomComercial;
                        withBlock.SearchKey = oContact.SearchKey;
                        withBlock.FullNom = oContact.FullNom;
                        withBlock.Nifs = oContact.Nifs;
                        withBlock.Address = oContact.Address;
                        withBlock.ContactClass = oContact.ContactClass;
                        withBlock.Lang = oContact.Lang;
                        withBlock.Rol = oContact.Rol;
                        withBlock.NomAnterior = oContact.NomAnterior;
                        withBlock.NomNou = oContact.NomNou;
                        withBlock.Website = oContact.Website;
                        withBlock.Logo = oContact.Logo;
                        withBlock.GLN = oContact.GLN;
                        withBlock.Telefon = oContact.Telefon;
                        withBlock.Tels = oContact.Tels;
                        withBlock.ContactPersons = oContact.ContactPersons;
                        withBlock.Obsoleto = oContact.Obsoleto;
                        withBlock.Obs = oContact.Obs;
                    }
                }
            }
            return retval;
        }

        public DTOCustomer Clone()
        {
            DTOCustomer retval = new DTOCustomer();
            var withBlock = retval;
            withBlock.Guid = Guid;
            withBlock.Emp = Emp;
            withBlock.Nom = Nom;
            withBlock.NomComercial = NomComercial;
            withBlock.SearchKey = SearchKey;
            withBlock.FullNom = FullNom;
            withBlock.Nifs = Nifs;
            withBlock.Address = Address;
            withBlock.ContactClass = ContactClass;
            withBlock.Lang = Lang;
            withBlock.Rol = Rol;
            withBlock.NomAnterior = NomAnterior;
            withBlock.NomNou = NomNou;
            withBlock.Website = Website;
            withBlock.Logo = Logo;
            withBlock.GLN = GLN;
            withBlock.Telefon = Telefon;
            withBlock.Tels = Tels;
            withBlock.ContactPersons = ContactPersons;
            withBlock.Obsoleto = Obsoleto;
            withBlock.Obs = Obs;
            return retval;
        }

        public static DTOCustomer Wellknown(DTOCustomer.Wellknowns id)
        {
            DTOCustomer retval = null;
            string sGuid = "";
            switch (id)
            {
                case Wellknowns.elCorteIngles:
                    {
                        sGuid = "1850CA50-B514-404E-BD5C-3C33B7A6D3BF";
                        break;
                    }

                case Wellknowns.eciga:
                    {
                        sGuid = "4A590843-E1E7-4550-9375-B42FCC917A24";
                        break;
                    }

                case Wellknowns.sonae:
                    {
                        sGuid = "EBCF8BC0-EE11-4875-8EB7-98A7078A6165"; // Fashion Inter.Trade, S.A.
                        break;
                    }
                case Wellknowns.amazon:
                    {
                        sGuid = "BDAC8F45-D3E7-47D7-8229-889FBA4543E1";
                        break;
                    }

                case Wellknowns.carrefour:
                    {
                        sGuid = "21DAC56A-F152-48CE-B357-6A8508520622";
                        break;
                    }

                case Wellknowns.prenatal:
                    {
                        sGuid = "44684614-0437-4FFB-B59E-D0B1392F819F";
                        break;
                    }

                case Wellknowns.prenatalPortugal:
                    {
                        sGuid = "E59C399A-A9BD-4D17-9729-ACA9FF88A7A4";
                        break;
                    }

                case Wellknowns.prenatalTenerife:
                    {
                        sGuid = "4779EE3D-5876-4065-B4FD-6D1F09D655AA";
                        break;
                    }

                case Wellknowns.miFarma:
                    {
                        sGuid = "35D515BA-585D-458A-9126-C713A5B26F58";
                        break;
                    }

                case Wellknowns.bebitus:
                    {
                        sGuid = "B6613C73-A857-401C-8F86-B6597378EA88";
                        break;
                    }

                case Wellknowns.javierBayon:
                    {
                        sGuid = "6901C741-9554-46BC-B4BA-8696929D2454";
                        break;
                    }
                case Wellknowns.tradeInn:
                    {
                        sGuid = "8E5FEB8F-3D6A-4630-978E-94EABF589EB5";
                        break;
                    }
                case Wellknowns.consumidor:
                    {
                        sGuid = "1925F462-D263-4BC9-BAEA-9186FD9AD111";
                        break;
                    }
                case Wellknowns.worten:
                    {
                        sGuid = "2BAF35DA-4D7A-411A-86F3-ABDBD6660967";
                        break;
                    }
                case Wellknowns.promofarma:
                    {
                        sGuid = "150136FD-BCAF-459F-954F-C92B5E9DF7B0";
                        break;
                    }
            }

            if (sGuid.isNotEmpty())
            {
                Guid oGuid = new Guid(sGuid);
                retval = new DTOCustomer(oGuid);
            }
            return retval;
        }

        public DTOCustomer CcxOrMe()
        {
            DTOCustomer retval = this.Ccx ?? this;
            return retval;
        }



        public static string RefOrNomComercial(DTOCustomer oCustomer)
        {
            string retval = oCustomer.Ref;
            if (retval == "")
                retval = oCustomer.NomComercial;
            if (retval == "")
                retval = oCustomer.Nom;
            return retval;
        }

        public static string NomNifAndAddress(DTOCustomer oCustomer, List<Exception> exs)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (oCustomer.Nom == "")
                exs.Add(new Exception("falta la rao social"));
            else
                sb.AppendLine(oCustomer.Nom);

            if (oCustomer.PrimaryNifValue() == "")
            {
                if (!oCustomer.IsConsumer())
                    exs.Add(new Exception("falta el Nif"));
            }
            else
                sb.AppendLine(oCustomer.PrimaryNifQualifiedValue());

            if (oCustomer.Address == null)
                exs.Add(new Exception("falta la adreça fiscal"));
            else
            {
                sb.AppendLine(oCustomer.Address.Text);
                sb.AppendLine(DTOAddress.ZipyCit(oCustomer.Address));
                DTOCountry oCountry = DTOAddress.Country(oCustomer.Address);
                if (oCountry.UnEquals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain)))
                    sb.AppendLine(oCountry.LangNom.Esp);
            }
            string retval = sb.ToString();
            return retval;
        }

        public static bool SuggestedIVA(DTOContact oContact)
        {
            DTOInvoice.ExportCods oExportCod = DTOContact.ExportCod(oContact);
            bool retval = (oExportCod == DTOInvoice.ExportCods.nacional);
            return retval;
        }

        public static bool SuggestedReq(DTOContact oContact)
        {
            bool retval = false;
            if (DTOCustomer.SuggestedIVA(oContact))
            {
                var oFormaJuridica = DTOContact.formaJuridica(oContact);
                retval = (oFormaJuridica == DTOContact.FormasJuridicas.PersonaFisica);
            }
            return retval;
        }

        public static List<DTODistributionChannel> DistributionChannels(IEnumerable<DTOContact> oCustomers)
        {
            DTODistributionChannel oNoChannel = new DTODistributionChannel(new Guid());
            oNoChannel.LangText.Esp = "(sense canal)";
            DTOContactClass oNoClass = new DTOContactClass(new Guid());
            oNoClass.Nom.Esp = "(sense classificar)";
            oNoClass.DistributionChannel = oNoChannel;
            foreach (var oContact in oCustomers.Where(x => x.ContactClass == null))
                oContact.ContactClass = oNoClass;
            return oCustomers.GroupBy(x => x.ContactClass.DistributionChannel.Guid).Select(y => y.First()).Select(z => z.ContactClass.DistributionChannel).ToList();
        }

        public static List<DTOContactClass> ContactClasses(IEnumerable<DTOContact> oCustomers)
        {
            return oCustomers.GroupBy(x => x.ContactClass.Guid).Select(y => y.First()).Select(z => z.ContactClass).ToList();
        }

        public new DTOGuidNom ToGuidNom()
        {
            DTOGuidNom retval = DTOGuidNom.Factory(this.Guid, this.Nom);
            return retval;
        }

        public string UrlTarifaExcel(DTOWebDomain domain = null, DateTime? fch = null)
        {
            if (fch == null) fch = DTO.GlobalVariables.Today();
            if (domain == null) domain = DTOWebDomain.Default();
            string sFch = (fch == null) ? DTO.GlobalVariables.Today().ToFileTime().ToString() : ((DateTime)fch).ToFileTime().ToString();
            string retval = domain.Url("doc", ((int)DTODocFile.Cods.tarifaexcel).ToString(), this.Guid.ToString(), sFch);
            return retval;
        }
        public static string UrlTarifaExcel(Guid customerGuid, DTOWebDomain domain = null, DateTime? fch = null)
        {
            DTOCustomer customer = new DTOCustomer(customerGuid);
            return customer.UrlTarifaExcel(domain, fch);
        }

        public bool IsConsumer()
        {
            bool retval = this.Guid.Equals(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.consumidor).Guid);
            return retval;
        }

        public override string ToString()
        {
            return FullNom;
        }

        public class Collection : List<DTOCustomer>
        {

        }

    }
}
