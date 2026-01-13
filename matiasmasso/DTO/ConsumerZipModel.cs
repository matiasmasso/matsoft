using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ConsumerZipModel
    {
        public CountryModel? Country { get; set; }
        public List<CountryModel> CountryList { get; set; } = new();
        public string? ZipCod { get; set; }
        public Guid? ZipGuid { get; set; }
        public string? FormattedLocation { get; set; }
        public string? Location { get; set; }
        public string? Zona { get; set; }
        public Errs? Err { get; set; }

        private const string GoogleApiKey = "AIzaSyC3O2n2r1p1w-9JkC-f-yI7HWQfkst053I";
        public enum Errs
        {
            SysErr,
            NotFound
        }

        public bool HasValue() => !string.IsNullOrEmpty(FormattedLocation) && Err == null;

        public string? GoogleApiUrl()
        {
            string? retval = null;
            if (Country != null && ZipCod != null)
            {
                var template = @"https://maps.googleapis.com/maps/api/geocode/json?components=postal_code:{1}|country:{0}&key={2}";
                retval = string.Format(template, Country.ISO, ZipCod, GoogleApiKey);
            }
            return retval;
        }
    }
}
