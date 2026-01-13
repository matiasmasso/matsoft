using System.Collections.Generic;

namespace DTO
{
    public class Google
    {
        public class Geonames
        {
            public const string username = "matiasmasso";
            //public const string rootUrl = "https://api.geonames.org/";
            public const string rootUrl = "https://secure.geonames.org/";

            public static string postalCodesUrl(string sCountryIso, string sZipCod)
            {
                var retval = string.Format("{0}postalCodeLookupJSON?postalcode={2}&country={3}&username={1}", rootUrl, username, sZipCod, sCountryIso);
                return retval;
            }

            public class request
            {
                public List<Geonames.postalCodeClass> postalCodes { get; set; }
            }




            // https://download.geonames.org/export/zip/
            // The data format is tab-delimited text in utf8 encoding, with the following fields :

            // country code      : iso country code, 2 characters
            // postal code       : varchar(20)
            // place name        : varchar(180)
            // admin name1       : 1. order subdivision (state) varchar(100)
            // admin code1       : 1. order subdivision (state) varchar(20)
            // admin name2       : 2. order subdivision (county/province) varchar(100)
            // admin code2       : 2. order subdivision (county/province) varchar(20)
            // admin name3       : 3. order subdivision (community) varchar(100)
            // admin code3       : 3. order subdivision (community) varchar(20)
            // latitude          : estimated latitude (wgs84)
            // longitude         : estimated longitude (wgs84)
            // accuracy          : accuracy of lat/lng from 1=estimated, 4=geonameid, 6=centroid of addresses or shape

            public static List<Geonames.Zip> ReadFromExcel(MatHelper.Excel.Sheet oSheet)
            {
                List<Geonames.Zip> retval = new List<Geonames.Zip>();
                foreach (var oRow in oSheet.Rows)
                {
                    Zip item = new Zip();
                    {
                        var withBlock = item;
                        withBlock.CountryISO = (string)oRow.Cells[0].Content;
                        withBlock.ZipCod = (string)oRow.Cells[1].Content;
                        withBlock.LocationNom = (string)oRow.Cells[2].Content;
                        withBlock.AddArea(oRow, 3, 4);
                        withBlock.AddArea(oRow, 5, 6);
                        withBlock.AddArea(oRow, 7, 8);
                        withBlock.longitud = (decimal)oRow.Cells[9].Content;
                        withBlock.latitud = (decimal)oRow.Cells[10].Content;
                        withBlock.Accuracy = (Zip.Accuracies)oRow.Cells[11].Content;
                    }
                    retval.Add(item);
                }
                return retval;
            }

            public class Zip
            {
                public string CountryISO { get; set; }
                public string ZipCod { get; set; }
                public string LocationNom { get; set; }
                public List<Area> Areas { get; set; }
                public decimal latitud { get; set; }
                public decimal longitud { get; set; }
                public Accuracies Accuracy { get; set; }

                public enum Accuracies
                {
                    estimated = 1,
                    geonameid = 4,
                    shape = 6
                }

                public Zip() : base()
                {
                    Areas = new List<Area>();
                }

                public void AddArea(MatHelper.Excel.Row oRow, int NomCellIdx, int CodCellIdx)
                {
                    Area oArea = new Area();
                    {
                        var withBlock = oArea;
                        withBlock.Level = Areas.Count + 1;
                        withBlock.Nom = oRow.Cells[NomCellIdx].Content.ToString();
                        withBlock.Cod = oRow.Cells[CodCellIdx].Content.ToString();
                    }
                    Areas.Add(oArea);
                }
            }

            public class Area
            {
                public string Nom { get; set; }
                public string Cod { get; set; }
                public int Level { get; set; }
            }

            public class postalCodeClass
            {
                public string countryCode { get; set; }
                public string postalCode { get; set; }
                public string placeName { get; set; }

                public string adminCode1 { get; set; }
                public string adminName1 { get; set; }
                public string adminCode2 { get; set; }
                public string adminName2 { get; set; }
                public string adminCode3 { get; set; }
                public string adminName3 { get; set; }
                public decimal lat { get; set; }
                public decimal lng { get; set; }
            }
        }
    }
}
