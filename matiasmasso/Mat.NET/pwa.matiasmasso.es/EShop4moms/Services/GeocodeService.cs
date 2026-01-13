using Components;
using Microsoft.AspNetCore.Components;
using DTO;
using System.Net.Http.Headers;
using System.Linq;

namespace Shop4moms.Services
{
    public class GeocodeService
    {

        private HttpClient http;

        public GeocodeService(HttpClient http)
        {
            this.http = http;
        }
        public async Task<List<DTO.Integracions.Google.Geonames.Zip>> GetZipLocationsAsync(string? isopais, string? zipcod)
        {
            List<DTO.Integracions.Google.Geonames.Zip> retval = new(); ;
            if (!string.IsNullOrEmpty(isopais) && !string.IsNullOrEmpty(zipcod))
            {
                retval = await GetZipsFromDatabaseAsync(isopais, zipcod);
                if (retval.Count == 0)
                    retval = await GetZipsFromGoogleAsync(isopais, zipcod);
            }
            return retval;
        }

        public async Task<List<DTO.Integracions.Google.Geonames.Zip>> GetZipsFromDatabaseAsync(string isopais, string zipcod)
        {
            var apiResponse = await HttpService.PostAsync<string, List<DTO.Integracions.Google.Geonames.Zip>>(http, zipcod, "zips/geocode", isopais);
            return apiResponse.Value ?? new List<DTO.Integracions.Google.Geonames.Zip>();
        }

        private async Task<List<DTO.Integracions.Google.Geonames.Zip>> GetZipsFromGoogleAsync(string? isopais, string? zipcod)
        {
            List<DTO.Integracions.Google.Geonames.Zip> retval = new(); ;
            if (!string.IsNullOrEmpty(isopais) && !string.IsNullOrEmpty(zipcod))
            {
                var url = DTO.Integracions.Google.Geonames.PostalCodesUrl(isopais, zipcod);
                var apiResponse = await HttpService.GetAsync<DTO.Integracions.Google.Geonames.Request>(http, url);
                if (apiResponse.Value != null)
                {
                    retval = apiResponse.Value.PostalCodes
                        .Select(x => new DTO.Integracions.Google.Geonames.Zip
                        {
                            ZipCod = zipcod,
                            LocationNom = x.PlaceName
                        }).ToList();
                }
            }
            return retval;
        }


    }


}
