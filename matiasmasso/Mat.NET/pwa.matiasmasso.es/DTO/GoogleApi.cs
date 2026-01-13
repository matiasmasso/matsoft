using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GoogleApi
    {
        public class GeoCodeModel
        {
            public List<Result> Results { get; set; } = new();
            public string? Status { get; set; }


            public string? ZipCod() => Area(ComponentTypes.postal_code);
            public string? Location() => Area(ComponentTypes.locality);
            public string? Zona() => Area(ComponentTypes.administrative_area_level_2);
            public string? Region() => Area(ComponentTypes.administrative_area_level_1);
            public string? CountryIso() => Area(ComponentTypes.country);
            public Result? FirstResult() => Results.FirstOrDefault();
            public string? Area(ComponentTypes type)
            {
                return FirstResult()?.Address_components
                    .Where(x => x.Types.Contains(type.ToString()))
                    .Select(x => type == ComponentTypes.country ? x.Short_name : x.Long_name)
                    .FirstOrDefault();
            }

            public class Result
            {
                public string? formatted_address { get; set; }
                public List<Component> Address_components { get; set; } = new();

            }

            public class Geometry
            {
                public Coordinates? Location { get; set; }
            }

            public class Coordinates
            {
                public double? Lat { get; set; }
                public double? Lng { get; set; }
            }

            public class Component
            {
                public string? Long_name { get; set; }
                public string? Short_name { get; set; }

                public List<string> Types { get; set; } = new();
            }

            public enum ComponentTypes
            {
                political,
                postal_code,
                locality,
                administrative_area_level_2,
                administrative_area_level_1,
                country
            }

        }
    }
}
