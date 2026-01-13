using MatHelperStd;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOContact : DTOArea
    {
        public int Id { get; set; }
        public DTOEmp Emp { get; set; }
        public DTONif.Collection Nifs { get; set; }
        public DTOAddress Address { get; set; }
        public string NomComercial { get; set; }
        public string SearchKey { get; set; }
        public new string FullNom { get; set; }
        public string Telefon { get; set; }
        public List<DTOAddress> Addresses { get; set; }
        public string Website { get; set; }
        public DTOLang Lang { get; set; }
        public DTORol Rol { get; set; }
        public DTOContact NomAnterior { get; set; }
        public DTOContact NomNou { get; set; }
        [JsonIgnore]
        public Byte[] Logo { get; set; }

        public DTOContactClass ContactClass { get; set; }
        public List<DTOContactTel> Tels { get; set; }
        public List<DTOUser> Emails { get; set; }
        public List<string> ContactPersons { get; set; }

        public DTOEan GLN { get; set; }

        public string Obs { get; set; }
        public bool Obsoleto { get; set; }


        public enum Tipus
        {
            NotSet,
            Proveidor,
            Client,
            Representant,
            Personal,
            Banc
        }

        public enum FormasJuridicas
        {
            Unknown,
            PersonaFisica,
            PersonaJuridica
        }

        public enum ContactKeys
        {
            Nom = 0,
            Poblacio = 3,
            Comercial = 4,
            SearchKey = 26
        }

        public enum SearchBy
        {
            notset,
            email,
            tel,
            adr,
            nif,
            SubContact,
            ccc
        }

        public enum Tabs
        {
            General,
            Client,
            Proveidor,
            Rep,
            Staff,
            Banc,
            Transportista
        }

        public DTOContact() : base()
        {
            base.Cod = Cods.Contact;
            Nifs = new DTONif.Collection();
            Address = new DTOAddress();
            Tels = new List<DTOContactTel>();
            Emails = new List<DTOUser>();
            ContactPersons = new List<string>();
            Rol = new DTORol(DTORol.Ids.guest);
        }

        public DTOContact(Guid oGuid) : base(oGuid)
        {
            base.Cod = Cods.Contact;
            Nifs = new DTONif.Collection();
            Address = new DTOAddress();
            Tels = new List<DTOContactTel>();
            Emails = new List<DTOUser>();
            ContactPersons = new List<string>();
            Rol = new DTORol(DTORol.Ids.guest);
        }

        public static DTOContact Factory(DTOEmp oEmp)
        {
            DTOContact retval = new DTOContact();
            {
                var withBlock = retval;
                withBlock.Emp = oEmp;
                withBlock.Lang = DTOLang.ESP();
            }
            return retval;
        }

        public static DTOContact.FormasJuridicas formaJuridica(DTOAddress oAddress, string sNif)
        {
            DTOContact.FormasJuridicas retval = DTOContact.FormasJuridicas.Unknown;
            if (DTOAddress.Country(oAddress).Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain)))
            {
                var oNif = DTONifOld.Factory(sNif, DTOAddress.Country(oAddress));
                if (oNif.Type == DTONifOld.NifTypes.Juridica)
                    retval = DTOContact.FormasJuridicas.PersonaJuridica;
                else if (oNif.Type == DTONifOld.NifTypes.Fisica)
                    retval = DTOContact.FormasJuridicas.PersonaFisica;
            }
            return retval;
        }

        public static DTOContact.FormasJuridicas formaJuridica(DTOContact oContact)
        {
            DTOContact.FormasJuridicas retval = DTOContact.FormasJuridicas.Unknown;
            DTOCountry oCountry = DTOAddress.Country(oContact.Address);
            if (oCountry != null)
            {
                if (oContact.PrimaryNifValue().isNotEmpty())
                    retval = formaJuridica(oContact.Address, oContact.PrimaryNifValue());
            }

            if (retval == DTOContact.FormasJuridicas.Unknown)
            {
                if (oCountry.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain)))
                {
                    if (oContact.Nom.EndsWith("S.A.") | oContact.Nom.EndsWith("S.L."))
                        retval = DTOContact.FormasJuridicas.PersonaJuridica;
                    else
                        retval = DTOContact.FormasJuridicas.PersonaFisica;
                }
            }
            return retval;
        }

        public static string nomAndNomComercial(DTOCustomer oCustomer)
        {
            string retval = oCustomer.nomAndNomComercial();
            if (oCustomer.Ref.isNotEmpty())
                retval = retval + " [" + oCustomer.Ref + "]";
            return retval;
        }

        public string nomAndNomComercial()
        {
            string retval = "";
            if (NomComercial == "")
                retval = base.Nom;
            else if (base.Nom.isNotEmpty())
                retval = string.Format("{0} {1}{2}{1}", base.Nom, VbUtilities.Chr(34), NomComercial);
            else
                retval = NomComercial;
            return retval;
        }

        public string NomComercialOrDefault()
        {
            string retval = NomComercial;
            if (string.IsNullOrEmpty(retval))
                retval = base.Nom;
            return retval;
        }

        public static string raoSocialONomComercial(DTOContact oContact)
        {
            string retval = "";
            if (oContact != null)
            {
                retval = oContact.Nom;
                if (retval == "")
                    retval = oContact.NomComercial;
            }
            return retval;
        }

        public static string nomComercialOrRaoSocialAndAddress(DTOContact oContact)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (oContact.NomComercial == "")
                sb.Append(oContact.Nom);
            else
                sb.Append(oContact.NomComercial);
            if (oContact.Address != null)
            {
                if (oContact.Address.Zip != null)
                {
                    if (oContact.Address.Zip.Location != null)
                        sb.Append("-" + oContact.Address.Zip.Location.Nom);
                }
                sb.Append("-" + oContact.Address.Text);
            }
            string retval = sb.ToString();
            return retval;
        }


        public static string generateFullNom(DTOContact oContact)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(oContact.nomAndNomComercial());
            sb.AppendFormat(" ({0})", DTOAddress.ClxLocation(oContact.Address));

            if (oContact.GetType() == typeof(DTOCustomer))
            {
                DTOCustomer oCustomer = (DTOCustomer)oContact;
                string sRef = oCustomer.Ref;
                if (sRef.isNotEmpty())
                {
                    if (!sb.ToString().Contains(sRef))
                        sb.Append(" [" + sRef + "]");
                }
            }

            var retval = VbUtilities.Left(sb.ToString(), 100);
            return retval;
        }

        public List<string> nomAndAddressLines()
        {
            List<string> retval = new List<string>();
            {
                var withBlock = retval;
                withBlock.Add((base.Nom == "") ? NomComercial : base.Nom);
                withBlock.Add(Address.Text);
                withBlock.Add(Address.Zip.ZipyCit());
            }
            return retval;
        }

        public string nomAndAddressHtml()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (string line in nomAndAddressLines())
                sb.AppendLine(line);
            string retval = TextHelper.Html(sb.ToString());
            return retval;
        }

        public string ZipCod()
        {
            string retval = "";
            if (this.Address != null)
            {
                if (this.Address.Zip != null)
                {
                    retval = this.Address.Zip.ZipCod;
                }
            }
            return retval;
        }

        public string CountryISO()
        {
            string retval = "";
            if (this.Address != null)
            {
                if (this.Address.Zip != null)
                {
                    if (this.Address.Zip.Location != null)
                    {
                        if (this.Address.Zip.Location.Zona != null)
                        {
                            if (this.Address.Zip.Location.Zona.Country != null)
                            {
                                retval = this.Address.Zip.Location.Zona.Country.ISO;
                            }
                        }
                    }
                }
            }
            return retval;
        }

        public Boolean IsEstrangerResident()
        {
            Boolean retval = false;
            if (this.Nifs != null)
                retval = this.Nifs.IsEstrangerResident();
            return retval;
        }

        public static DTODistributionChannel distributionChannel(DTOContact oContact)
        {
            DTODistributionChannel retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oContact.ContactClass != null)
                retval = oContact.ContactClass.DistributionChannel;
            return retval;
        }

        public string urlSegment()
        {
            return base.urlSegment("contacto");
        }

        public string url(bool AbsoluteUrl = false)
        {
            return MmoUrl.Factory(AbsoluteUrl, "contacto", base.Guid.ToString());
        }

        public static string claveRegimenEspecialOTrascendencia(DTOContact oContact)
        {
            string retval = "";
            if (oContact.Address != null)
            {
                switch (oContact.Address.ExportCod())
                {
                    case DTOInvoice.ExportCods.nacional:
                        {
                            retval = "01";
                            break;
                        }

                    case DTOInvoice.ExportCods.intracomunitari:
                        {
                            retval = "09";
                            break;
                        }

                    case DTOInvoice.ExportCods.extracomunitari:
                        {
                            retval = "13";
                            break;
                        }
                }
            }
            return retval;
        }

        public static string claveCausaExempcio(DTOContact oContact)
        {
            string retval = "";
            if (oContact.Address != null)
            {
                switch (oContact.Address.ExportCod())
                {
                    case DTOInvoice.ExportCods.nacional:
                        {
                            break;
                        }

                    case DTOInvoice.ExportCods.intracomunitari:
                        {
                            retval = "E5";
                            break;
                        }

                    case DTOInvoice.ExportCods.extracomunitari:
                        {
                            retval = "E2";
                            break;
                        }
                }
            }
            return retval;
        }

        public static DTOInvoice.ExportCods ExportCod(DTOContact oContact)
        {
            DTOInvoice.ExportCods retval = DTOAddress.ExportCod(oContact.Address);
            switch (retval)
            {
                case DTOInvoice.ExportCods.intracomunitari:
                    {
                        string sNif = oContact.PrimaryNifQualifiedValue(oContact.Lang);
                        if (sNif.isNotEmpty() && sNif.Length > 2)
                        {
                            if (sNif.StartsWith("ES"))
                                retval = DTOInvoice.ExportCods.nacional;// no residents
                        }

                        break;
                    }
            }
            return retval;
        }

        public static bool isIVASujeto(DTOContact oContact)
        {
            DTOInvoice.ExportCods oExportCod = DTOContact.ExportCod(oContact);
            bool retval = (oExportCod == DTOInvoice.ExportCods.nacional);
            return retval;
        }



        public bool isElCorteIngles()
        {
            DTOCustomer[] oMembers = new DTOCustomer[]
            {
            DTOCustomer.Wellknown(DTOCustomer.Wellknowns.elCorteIngles),
            DTOCustomer.Wellknown(DTOCustomer.Wellknowns.eciga)
        };
            bool retval = oMembers.Any(x => x.Equals(this));
            return retval;
        }

        public bool isPrenatal()
        {
            DTOCustomer.Wellknowns[] oMembers = {
            DTOCustomer.Wellknowns.prenatal,
            DTOCustomer.Wellknowns.prenatalTenerife,
            DTOCustomer.Wellknowns.prenatalPortugal
        };
            var retval = oMembers.Any(x => DTOCustomer.Wellknown(x).Equals(this));
            return retval;
        }

        public bool isSonae()
        {
            DTOCustomer[] oMembers = new DTOCustomer[]
            {
            DTOCustomer.Wellknown(DTOCustomer.Wellknowns.sonae)
        };
            bool retval = oMembers.Any(x => x.Equals(this));
            return retval;
        }


        public static DTOPgcPlan.Ctas defaultCtaCod(DTOContact oContact)
        {
            DTOPgcPlan.Ctas retval = DTOPgcPlan.Ctas.NotSet;
            if (oContact.Rol != null)
            {
                switch (oContact.Rol.id)
                {
                    case DTORol.Ids.banc:
                        {
                            retval = DTOPgcPlan.Ctas.Bancs;
                            break;
                        }

                    case DTORol.Ids.cliFull:
                    case DTORol.Ids.cliLite:
                        {
                            retval = DTOPgcPlan.Ctas.Clients;
                            break;
                        }

                    case DTORol.Ids.manufacturer:
                        {
                            retval = DTOPgcPlan.Ctas.ProveidorsEur;
                            break;
                        }
                }

            }
            return retval;
        }

        public static string resum(DTOContact oContact)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            {
                var withBlock = oContact;
                sb.AppendLine(oContact.Nom);
                sb.AppendLine(oContact.NomComercial);
                sb.AppendLine(oContact.PrimaryNifQualifiedValue(oContact.Lang));
                sb.AppendLine(DTOAddress.multiLine(oContact.Address));
            }
            string retval = sb.ToString();
            return retval;
        }

        public String PrimaryNifValue()
        {
            string retval = "";
            if (this.Nifs.PrimaryNif() != null)
                retval = this.Nifs.PrimaryNif().Value;
            return retval;
        }

        public String PrimaryNifCodNom(DTOLang lang = null)
        {
            string retval = "";
            if (this.Nifs.PrimaryNif() != null)
            {
                if (lang == null)
                    lang = DTOLang.ESP();
                retval = DTONif.CodNom(this.Nifs.PrimaryNif().Cod, lang);
            }
            return retval;
        }
        public String PrimaryNifQualifiedValue(DTOLang lang = null)
        {
            string retval = "";
            if (this.Nifs.PrimaryNif() != null)
            {
                if (lang == null)
                    lang = DTOLang.ESP();
                retval = this.Nifs.PrimaryNif().QualifiedValue(lang);
            }
            return retval;
        }


        public static string htmlNameAndAddress(DTOContact oContact)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine(oContact.Nom);
            sb.AppendLine(DTOAddress.multiLine(oContact.Address));
            string retval = sb.ToString().toMultiLineHtml();
            return retval;
        }

        public static List<DTOCountry> countries(IEnumerable<DTOContact> oContacts)
        {
            return oContacts.GroupBy(x => DTOAddress.Country(x.Address).Guid).Select(y => y.First()).Select(z => DTOAddress.Country(z.Address)).ToList();
        }

        public static List<DTOZona> zonas(IEnumerable<DTOContact> oContacts)
        {
            return oContacts.GroupBy(x => DTOAddress.Zona(x.Address).Guid).Select(y => y.First()).Select(z => DTOAddress.Zona(z.Address)).ToList();
        }

        public static List<DTOLocation> locations(IEnumerable<DTOContact> oContacts)
        {
            return oContacts.GroupBy(x => DTOAddress.Location(x.Address).Guid).Select(y => y.First()).Select(z => DTOAddress.Location(z.Address)).ToList();
        }

        public DTOGuidNom ToGuidNom()
        {
            DTOGuidNom retval = new DTOGuidNom(this.Guid);
            retval.Nom = this.FullNom == null ? this.Nom : this.FullNom;
            return retval;
        }
    }
}
