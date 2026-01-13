using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOLocation : DTOArea
    {
        public DTOZona Zona { get; set; }
        public bool Export { get; set; }
        public DTOComarca Comarca { get; set; }
        public List<DTOZip> Zips { get; set; }
        public List<DTOContact> Contacts { get; set; }

        public DTOPortsCondicio PortsCondition { get; set; }

        public enum Wellknowns
        {
            madrid
        }

        public DTOLocation() : base()
        {
            base.Cod = Cods.Location;
            this.Zips = new List<DTOZip>();
        }

        public DTOLocation(Guid oGuid) : base(oGuid)
        {
            base.Cod = Cods.Location;
            this.Zips = new List<DTOZip>();
        }

        public static DTOLocation Factory(DTOZona zona, string nom = "")
        {
            DTOLocation retval = new DTOLocation();
            retval.Zona = zona;
            retval.Nom = nom;
            return retval;
        }

        public static DTOLocation Wellknown(DTOLocation.Wellknowns id)
        {
            DTOLocation retval = null;
            switch (id)
            {
                case DTOLocation.Wellknowns.madrid:
                    {
                        retval = new DTOLocation(new Guid("3EC9E266-C7D7-42B0-94DD-27334A5EE118"));
                        break;
                    }
            }
            return retval;
        }

        public static DTOLocation Factory(DTOZona oZona)
        {
            DTOLocation retval = new DTOLocation();
            retval.Zona = oZona;
            return retval;
        }

        public static string FullNom(DTOLocation oLocation, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            string retval = "";
            if (oLocation != null)
                retval = oLocation.FullNom(oLang);
            return retval;
        }

        public string FullNom(DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(base.Nom);
            DTOZona oZona = this.Zona;
            if (oZona != null)
            {
                DTOCountry oCountry = this.Zona.Country;
                if (DTOArea.isEsp(oCountry))
                {
                    DTOAreaProvincia oProvincia = oZona.Provincia;
                    if (oProvincia == null)
                    {
                        if (base.Nom != oZona.Nom)
                            sb.Append(" (" + oZona.Nom + ")");
                    }
                    else if (base.Nom != oProvincia.Nom)
                        sb.Append(" (" + oProvincia.Nom + ")");
                }
                else if (oCountry != null)
                {
                    if (oLang == null)
                        oLang = DTOLang.ESP();
                    sb.Append(" (" + oCountry.LangNom.Tradueix(oLang) + ")");
                }
            }

            string retval = sb.ToString();
            return retval;
        }

        public static string FullNomSegmented(DTOLocation oLocation, DTOLang oLang)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(oLocation.Nom);
            sb.Append("/");
            sb.Append(oLocation.Zona.FullNomSegmented(oLang));
            string retval = sb.ToString();
            return retval;
        }

        public static string FullNomSegmentedReversed(DTOLocation oLocation, DTOLang oLang)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(oLocation.Zona.FullNomSegmented(oLang));
            sb.Append("/");
            sb.Append(oLocation.Nom);
            string retval = sb.ToString();
            return retval;
        }

        public string CountryNom(DTOLang oLang)
        {
            string retval = "";
            if (this.Zona != null)
                retval = this.Zona.CountryNom(oLang);
            return retval;
        }

        public bool Matches(string searchKey)
        {
            return MatHelperStd.TextHelper.Match(this.Nom, searchKey);
        }


        public static DTOInvoice.ExportCods ExportCod(DTOLocation oLocation)
        {
            DTOInvoice.ExportCods retval = DTOInvoice.ExportCods.notSet;
            if (oLocation != null)
            {
                if (oLocation.Zona != null)
                {
                    switch (oLocation.Zona.ExportCod)
                    {
                        case DTOInvoice.ExportCods.intracomunitari:
                        case DTOInvoice.ExportCods.extracomunitari:
                            {
                                retval = oLocation.Zona.ExportCod;
                                break;
                            }

                        default:
                            {
                                if (oLocation.Zona.Country != null)
                                    retval = oLocation.Zona.Country.ExportCod;
                                break;
                            }
                    }
                }
            }
            return retval;
        }

        public static DTOAreaProvincia Provincia(DTOLocation oLocation)
        {
            DTOAreaProvincia retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oLocation != null)
            {
                if (oLocation.Zona != null)
                    retval = oLocation.Zona.Provincia;
            }
            return retval;
        }

        public static string ProvinciaOrZonaNom(DTOLocation oLocation)
        {
            string retval = "";
            if (oLocation != null && oLocation.Zona != null)
            {
                if (oLocation.Zona.Provincia == null)
                    retval = oLocation.Zona.Nom;
                else
                    retval = oLocation.Zona.Provincia.Nom;
            }
            return retval;
        }
    }
}
