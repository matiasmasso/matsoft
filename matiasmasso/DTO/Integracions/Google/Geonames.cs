using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Google
{
    public class Geonames
    {
        public const string Username = "matiasmasso";
        public const string RootUrl = "https://secure.geonames.org/";


        /// <summary>
        /// url to call Google Api for an object Google.Geonames.Request
        /// </summary>
        /// <param name="sCountryIso"></param>
        /// <param name="sZipCod"></param>
        /// <returns>url string</returns>
        public static string PostalCodesUrl(string sCountryIso, string sZipCod)
        {
            var retval = string.Format("{0}postalCodeLookupJSON?postalcode={2}&country={3}&username={1}", RootUrl, Username, sZipCod, sCountryIso);
            return retval;
        }

        public class Request
        {
            public List<Geonames.PostalCodeClass> PostalCodes { get; set; } = new();
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


        public class Zip
        {
            public Guid? Guid { get; set; } //omited by Google, used only to match our database
            public string? CountryISO { get; set; }
            public string? ZipCod { get; set; }
            public string? LocationNom { get; set; }
            public List<Area> Areas { get; set; } = new();
            public decimal Latitud { get; set; }
            public decimal Longitud { get; set; }
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

        }

        public class Area
        {
            public string? Nom { get; set; }
            public string? Cod { get; set; }
            public int Level { get; set; }
        }

        public class PostalCodeClass
        {
            public string? CountryCode { get; set; }
            public string? PostalCode { get; set; }
            public string? PlaceName { get; set; }

            public string? AdminCode1 { get; set; }
            public string? AdminName1 { get; set; }
            public string? AdminCode2 { get; set; }
            public string? AdminName2 { get; set; }
            public string? AdminCode3 { get; set; }
            public string? AdminName3 { get; set; }
            public decimal lat { get; set; }
            public decimal lng { get; set; }
        }
    }

}
