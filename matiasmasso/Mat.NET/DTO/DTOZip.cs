using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOZip : DTOArea
    {
        public string ZipCod { get; set; }
        public DTOLocation Location { get; set; }
        public List<DTOContact> Contacts { get; set; }

        public DTOZip() : base()
        {
            base.Cod = Cods.Zip;
        }

        public DTOZip(Guid oGuid) : base(oGuid)
        {
            base.Cod = Cods.Zip;
        }

        public static DTOZip Factory(DTOLocation oLocation, string sZipCod)
        {
            DTOZip retval = new DTOZip();
            {
                var withBlock = retval;
                withBlock.Location = oLocation;
                withBlock.ZipCod = sZipCod;
            }
            return retval;
        }

        public string ZipyCit()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (this.ZipCod.isNotEmpty())
                sb.Append(this.ZipCod + " ");
            if (this.Location != null)
                sb.Append(this.Location.Nom);
            string retval = sb.ToString();
            return retval;
        }

        public static string ZipyCit(DTOZip oZip)
        {
            string retval = "";
            if (oZip != null)
                retval = oZip.ZipyCit();
            return retval;
        }

        public string FullNom(DTOLang oLang)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (this.ZipCod.isNotEmpty())
                sb.Append(this.ZipCod);
            if (this.Location != null)
            {
                sb.Append(" ");
                sb.Append(this.Location.FullNom(oLang));
            }

            string retval = sb.ToString();
            return retval;
        }

        public static string FullNom(DTOZip oZip, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            string retval = "";
            if (oZip != null)
                retval = oZip.FullNom(oLang);
            return retval;
        }

        public static string FullNomSegmented(DTOZip oZip, DTOLang oLang)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(oZip.ZipyCit());
            sb.Append("/");
            sb.Append(oZip.Location.Zona.FullNomSegmented(oLang));
            string retval = sb.ToString();
            return retval;
        }

        public static string FullNomSegmentedReversed(DTOZip oZip, DTOLang oLang)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(DTOLocation.FullNomSegmented(oZip.Location, oLang));
            sb.Append("/");
            sb.Append(oZip.ZipCod);
            string retval = sb.ToString();
            return retval;
        }

        public bool Matches(string searchKey)
        {
            return MatHelperStd.TextHelper.Match(this.ZipCod, searchKey);
        }

        public static bool Validate(DTOCountry oCountry, string sZipCod)
        {
            bool retval = true;
            if (oCountry.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain)))
            {
                var sPattern = "^[0-9]{5}$";
                retval = TextHelper.RegexMatch(sZipCod, sPattern);
            }
            else if (oCountry.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Portugal)))
            {
                var sPattern = "^[0-9]{4}-[0-9]{3}$";
                retval = TextHelper.RegexMatch(sZipCod, sPattern);
            }
            return retval;
        }

        public string CountryNom(DTOLang oLang)
        {
            string retval = "";
            if (this.Location != null)
                retval = this.Location.CountryNom(oLang);
            return retval;
        }

        public DTOInvoice.ExportCods ExportCod()
        {
            return ExportCod(this);
        }

        public static DTOInvoice.ExportCods ExportCod(DTOZip oZip)
        {
            DTOInvoice.ExportCods retval = DTOInvoice.ExportCods.notSet;
            if (oZip != null)
            {
                if (oZip.Location != null)
                    retval = DTOLocation.ExportCod(oZip.Location);
            }
            return retval;
        }

        public static DTOAreaProvincia Provincia(DTOZip oZip)
        {
            DTOAreaProvincia retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oZip != null)
            {
                if (oZip.Location != null)
                    retval = DTOLocation.Provincia(oZip.Location);
            }
            return retval;
        }



        public static List<DTOCountry> countries(IEnumerable<DTOZip> oZips)
        {
            var retval = oZips.GroupBy(x => x.Location.Zona.Country.Guid).Select(y => y.First()).Select(z => z.Location.Zona.Country).ToList();
            return retval;
        }

        public static List<DTOZona> Zonas(IEnumerable<DTOZip> oZips, DTOCountry FromCountry = null/* TODO Change to default(_) if this is not a reference type */)
        {
            var retval = oZips.GroupBy(x => x.Location.Zona.Guid).Select(y => y.First()).Select(z => z.Location.Zona).ToList();
            if (FromCountry != null)
                retval = retval.Where(x => x.Country.Equals(FromCountry)).ToList();
            return retval;
        }

        public static List<DTOLocation> Locations(IEnumerable<DTOZip> oZips, DTOZona FromZona = null/* TODO Change to default(_) if this is not a reference type */)
        {
            var retval = oZips.GroupBy(x => x.Location.Guid).Select(y => y.First()).Select(z => z.Location).ToList();
            if (FromZona != null)
                retval = retval.Where(x => x.Zona.Equals(FromZona)).ToList();
            return retval;
        }

        public static List<DTOZip> Zips(IEnumerable<DTOZip> oZips, DTOLocation FromLocation)
        {
            var retval = oZips.Where(x => x.Location.Equals(FromLocation)).ToList();
            return retval;
        }

        public class Collection : List<DTOZip>
        {

        }
    }
}
