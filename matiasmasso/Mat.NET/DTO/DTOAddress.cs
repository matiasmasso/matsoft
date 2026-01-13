using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOAddress
    {
        public string ViaNom { get; set; }
        public string Num { get; set; }
        public string Pis { get; set; }

        public string Text { get; set; }
        public DTOZip Zip { get; set; }
        public GeoHelper.Coordenadas Coordenadas { get; set; }

        public DTOBaseGuid Src { get; set; }
        public Codis Codi { get; set; }

        public bool IsNew { get; set; }

        public enum Codis
        {
            NotSet,
            Fiscal,
            Correspondencia,
            Entregas,
            FraConsumidor
        }

        public static DTOAddress Factory(DTOBaseGuid oSrc, DTOAddress.Codis oCodi)
        {
            DTOAddress retval = new DTOAddress();
            {
                retval.Src = oSrc;
                retval.Codi = oCodi;
            }
            return retval;
        }

        public new bool Equals(object oCandidate)
        {
            bool retval = false;
            if (oCandidate != null)
            {
                if (oCandidate is DTOAddress)
                {
                    DTOAddress oAddress = (DTOAddress)oCandidate;
                    if (Text == oAddress.Text)
                    {
                        DTOZip oZip = oAddress.Zip;
                        retval = Zip.Equals(oZip);
                    }
                }
            }
            return retval;
        }


        public DTOInvoice.ExportCods ExportCod()
        {
            DTOInvoice.ExportCods retval = DTOInvoice.ExportCods.notSet;

            if (Zip != null)
            {
                DTOLocation oLocation = Zip.Location;
                if (oLocation != null)
                {
                    DTOZona oZona = oLocation.Zona;
                    if (oZona != null)
                        retval = oZona.ExportCod;
                }
            }
            return retval;
        }

        public static DTOInvoice.ExportCods ExportCod(DTOAddress oAddress)
        {
            DTOInvoice.ExportCods retval = DTOInvoice.ExportCods.notSet;
            if (oAddress != null)
                retval = oAddress.ExportCod();
            return retval;
        }

        public static string FullText(DTOAddress oAddress, DTOLang oLang)
        {
            return oAddress.Text.toSingleLine();
        }

        public string ToMultilineString(DTOLang oLang)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine(Text);
            sb.AppendLine(Zip.FullNom(oLang));
            var retval = sb.ToString();
            return retval;
        }

        public static string TextOrDefault(DTOAddress oAddress)
        {
            string retval = "";
            if (oAddress != null)
                retval = oAddress.Text;
            return retval;
        }


        public static string ZipCod(DTOAddress oAddress)
        {
            string retval = "";
            if (oAddress != null && oAddress.Zip != null)
                retval = oAddress.Zip.ZipCod;
            return retval;
        }

        public static string ZipyCit(DTOAddress oAddress)
        {
            string retval = "";
            if (oAddress != null && oAddress.Zip != null)
                retval = oAddress.Zip.ZipyCit();
            return retval;
        }

        public string SingleLineText()
        {
            string retval = "";
            if (!string.IsNullOrEmpty(Text))
                retval = this.Text.toSingleLine();
            return retval;
        }

        public DTOLocation Location()
        {
            DTOLocation retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (this.Zip != null)
                retval = this.Zip.Location;
            return retval;
        }

        public static DTOLocation Location(DTOAddress oAddress)
        {
            DTOLocation retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oAddress != null)
                retval = oAddress.Location();
            return retval;
        }

        public DTOZona Zona()
        {
            DTOZona retval = null/* TODO Change to default(_) if this is not a reference type */;
            var oLocation = this.Location();
            if (oLocation != null)
                retval = oLocation.Zona;
            return retval;
        }

        public bool UnEquals(DTOAddress candidate)
        {
            bool retval = !Equals(candidate);
            return retval;
        }

        public static DTOZona Zona(DTOAddress oAddress)
        {
            DTOZona retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oAddress != null)
                retval = oAddress.Zona();
            return retval;
        }

        public DTOAreaProvincia Provincia()
        {
            DTOAreaProvincia retval = null/* TODO Change to default(_) if this is not a reference type */;
            var oZona = this.Zona();
            if (oZona != null)
                retval = oZona.Provincia;
            return retval;
        }

        public static DTOAreaProvincia Provincia(DTOAddress oAddress)
        {
            DTOAreaProvincia retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oAddress != null)
                retval = oAddress.Provincia();
            return retval;
        }

        public DTOCountry Country()
        {
            DTOCountry retval = null/* TODO Change to default(_) if this is not a reference type */;
            var oZona = this.Zona();
            if (oZona != null)
                retval = oZona.Country;
            return retval;
        }

        public static DTOCountry Country(DTOAddress oAddress)
        {
            DTOCountry retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oAddress != null)
                retval = oAddress.Country();
            return retval;
        }

        public string CountryNom(DTOLang oLang)
        {
            string retval = "";
            if (this.Zip != null)
                retval = this.Zip.CountryNom(oLang);
            return retval;
        }

        public DTOLang SuggestedLang()
        {
            DTOLang retval = null/* TODO Change to default(_) if this is not a reference type */;
            DTOZona oZona = this.Zona();
            if (oZona == null)
                retval = DTOLang.ESP();
            else
                retval = oZona.SuggestedLang();
            return retval;
        }

        public static string LocationFullNom(DTOAddress oAddress, DTOLang oLang)
        {
            string retval = "";
            if (oAddress != null && oAddress.Zip != null && oAddress.Zip.Location != null)
                retval = DTOLocation.FullNom(oAddress.Zip.Location, oLang);
            return retval;
        }


        public static string ClxLocation(DTOAddress oAddress)
        {
            // per generar Contact.FullNom
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (oAddress != null)
            {
                if (oAddress.Zip != null)
                {
                    var oLocation = oAddress.Zip.Location;
                    if (oLocation != null)
                    {
                        string sCit = oLocation.Nom;
                        sb.Append(sCit);
                        var oZona = oLocation.Zona;
                        if (oZona != null)
                        {
                            var oCountry = oZona.Country;
                            if (oCountry == null)
                            {
                            }
                            else if (oCountry.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain)))
                            {
                                if (oZona.Provincia != null)
                                {
                                    string sProvincia = oZona.Provincia.Nom;
                                    if (sProvincia != "" & sProvincia != oLocation.Nom)
                                        sb.Append(string.Format(", {0}", sProvincia));
                                }
                            }
                            else
                            {
                                string sCountry = oCountry.LangNom.Esp;
                                if (!string.IsNullOrEmpty(sCountry))
                                {
                                    sb.Append(string.Format(", {0}", sCountry));
                                }

                            }
                        }
                    }
                }
            }

            var retval = sb.ToString();
            return retval;
        }

        public bool IsEmpty()
        {
            bool retval = this.Text == "" & this.ViaNom == "" & this.Num == "" & this.Zip == null;
            return retval;
        }

        public static List<string> Lines(DTOContact oContact)
        {
            List<string> retval = new List<string>();
            {
                retval.Add(string.IsNullOrEmpty(oContact.Nom) ? oContact.NomComercial : oContact.Nom);
                retval.Add(oContact.Address.Text);
                retval.Add(oContact.Address.Zip.ZipyCit());
            }
            return retval;
        }

        public static string ProvinciaOPais(DTOZip oZip)
        {
            string retval = "";
            if (oZip != null)
            {
                if (DTOArea.isEsp(oZip))
                {
                    List<Exception> exs = new List<Exception>();
                    var oProvincia = DTOZip.provincia(oZip);
                    if (oProvincia != null)
                        retval = "(" + oProvincia.Nom + ")";
                }
                else
                    retval = "(" + DTOArea.Country(oZip).LangNom.Esp + ")";
            }
            return retval;
        }

        public bool IsEsp()
        {
            bool retval = DTOArea.isEsp(this.Zip);
            return retval;
        }

        public static string GoogleText(DTOAddress oAddress)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(oAddress.ViaNom))
            {
                sb.Append(oAddress.ViaNom);
                sb.Append(",");
            }
            if (!string.IsNullOrEmpty(oAddress.Num))
            {
                sb.Append(oAddress.Num);
                sb.Append(",");
            }
            if (oAddress.Zip != null)
            {
                sb.Append(oAddress.Zip.ZipCod);
                sb.Append(",");
                sb.Append(oAddress.Location().Nom);
                sb.Append(",");
                sb.Append(oAddress.CountryNom(DTOLang.ESP()));
            }
            var retval = sb.ToString();
            return retval;
        }

        public static string GoogleNormalized(DTOAddress oSrc)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (oSrc != null && !string.IsNullOrEmpty(oSrc.Text))
            {
                string[] sAdrSegments = oSrc.Text.Split(' ');
                for (int i = 0; i < sAdrSegments.Count(); i++)
                {
                    // elimina el segon numero del carrer en cas de varios numeros

                    string segment = sAdrSegments[i];
                    if (VbUtilities.isNumeric(segment))
                    {
                        if (segment.Contains("-"))
                            segment = segment.Substring(0, segment.IndexOf("-"));
                    }
                }
                string sAdr = string.Join("+", sAdrSegments);
                string sCit = oSrc.Zip.Location.Nom.Replace(" ", "+");
                string sZona = oSrc.Zip.Location.Zona.Nom.Replace(" ", "+");
                string sCountry = oSrc.Zip.Location.Zona.Country.LangNom.Esp.Replace(" ", "+");
                sb.Append(sAdr);
                if (!string.IsNullOrEmpty(oSrc.Zip.ZipCod))
                    sb.Append("," + oSrc.Zip.ZipCod);
                sb.Append("," + sCit);
                sb.Append("," + sZona);
                sb.Append("," + sCountry);
            }
            string retval = sb.ToString();
            return retval;
        }

        public static string multiLine(DTOAddress oAddress)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (oAddress != null)
            {
                if (!string.IsNullOrEmpty(oAddress.Text))
                    sb.AppendLine(oAddress.Text.Trim());
                sb.Append(DTOZip.FullNom(oAddress.Zip));
            }
            string retval = sb.ToString();
            return retval;
        }

        public string ToHtml()
        {
            var sLines = multiLine(this);
            string retval = TextHelper.Html(sLines);
            return retval;
        }


        public static string ReverseSingleLine(DTOAddress oAddress, DTOLang oLang)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string sLocation = DTOAddress.LocationFullNom(oAddress, oLang);
            if (!string.IsNullOrEmpty(sLocation))
            {
                sb.Append(sLocation);
                if (!string.IsNullOrEmpty(oAddress.Text))
                    sb.Append(", ");
            }
            sb.Append(oAddress.Text);
            string retval = sb.ToString();
            return retval;
        }

        public static string FullText(DTOAddress oAddress)
        {
            string retval = DTOAddress.multiLine(oAddress).toSingleLine();
            return retval;
        }

        public static string FullHtml(DTOAddress oAddress)
        {
            string retval = DTOAddress.multiLine(oAddress).toMultiLineHtml();
            return retval;
        }

        public bool Equals(DTOAddress candidate)
        {
            bool retval = false;
            if (candidate != null && this.Zip != null)
                retval = (this.Text == candidate.Text && this.Zip.Equals(candidate.Zip));
            return retval;
        }



        public DTOAddress Trimmed()
        {
            Type oType = this.GetType();
            var retval = Activator.CreateInstance(this.GetType());
            try
            {
                var oProperties = oType.GetProperties();
                foreach (var oProperty in oProperties)
                {
                    if (typeof(DTOBaseGuid).IsAssignableFrom(oProperty.PropertyType))
                    {
                        try
                        {
                            DTOBaseGuid oBaseGuid = (DTOBaseGuid)oProperty.GetValue(this);
                            if (oBaseGuid != null)
                            {
                                var oTrimmedPropertyValue = Activator.CreateInstance(oProperty.PropertyType, oBaseGuid.Guid);
                                oProperty.SetValue(retval, oTrimmedPropertyValue);
                            }
                        }
                        catch (Exception)
                        {
                            oProperty.SetValue(retval, oProperty.GetValue(this));
                        }
                    }
                    else
                        oProperty.SetValue(retval, oProperty.GetValue(this));
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debugger.Break();
            }
            return (DTOAddress)retval;
        }
    }
}
