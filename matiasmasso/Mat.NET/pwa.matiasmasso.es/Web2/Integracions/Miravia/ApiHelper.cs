using DocumentFormat.OpenXml.Wordprocessing;
using DTO.Helpers;

namespace Web.Integracions.Miravia
{
    public class ApiHelper
    {

        public static string Host = "https://api.miravia.es/rest";
        string app_key = "503150";
        string access_token = "50000201e18s4ESbq5oV1kFHLpakTW104d0477IEYCswiwRyplpXa5wiJgLChYe";
        string sign_method = "sha256";
        string requestMethod = "GET";
        string app_secret = "7spdqfxkgXNnL8DY074L8nFEla9crEFM";
        string? timestamp;


        public static string Url(string apiName, params string[] parameters)
        {
            var api = new ApiHelper();
            return api.BuildUrl(apiName, parameters);
        }

        public static Dictionary<string,string> Payload(string apiName, params string[] parameters)
        {
            var api = new ApiHelper();
            return api.BuildPayload(apiName, parameters);
        }

        string BuildUrl(string apiName, params string[] businessParameters)
        {
            timestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds().ToString();
            var parameters = GetParameters(businessParameters);
            var signature = Signature(apiName, parameters);
            var queryString = QueryString(parameters, signature);
            var retval = string.Format("{0}{1}?{2}", Host, apiName, queryString);
            return retval;
        }



        public  Dictionary<string,string> BuildPayload(string apiName, params string[] businessParameters)
        {
            timestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds().ToString();
            var parameters = GetParameters(businessParameters);
            var signature = Signature(apiName, parameters);

            var retval = new Dictionary<string, string>();
            foreach(var parameter in parameters)
            {
                retval.Add(parameter.Key, parameter.Value);
            }
            retval.Add("sign", signature);
            return retval;
        }

        string QueryString(List<KeyValuePair<string, string>> parameters, string signature)
        {
            var queryParameters = new List<KeyValuePair<string, string>>();
            queryParameters.AddRange(parameters);
            var signParameter = new KeyValuePair<string, string>("sign", signature);
            queryParameters.Add(signParameter);

            var strings = queryParameters.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value));
            var retval = string.Join("&", strings);
            return retval;
        }

        string Signature(string apiName, List<KeyValuePair<string, string>> parameters)
        {
            //Concatenate the sorted parameters and their values into a string
            var strings = parameters.Select(kvp => string.Format("{0}{1}", kvp.Key, kvp.Value));
            var concatenated = string.Join("", strings);

            //Add the API name in front of the concatenated string
            var concatenatedWithApiName = apiName + concatenated;

            //Generate signature
            var secretBytes = System.Text.Encoding.UTF8.GetBytes(app_secret);
            string base64Secret = System.Convert.ToBase64String(secretBytes);
            byte[] SignatureKeyBytes = Convert.FromBase64String(base64Secret);
            byte[] hashBytes = CryptoHelper.GetHMACSHA256(concatenatedWithApiName, SignatureKeyBytes);
            string retval = Convert.ToHexString(hashBytes);
            return retval;
        }

        List<KeyValuePair<string, string>> GetParameters(params string[] businessParameters)
        {
            var retval = new List<KeyValuePair<string, string>>();

            //add common parameters
            retval.AddRange(CommonParameters()
            .Select(x => new KeyValuePair<string, string>(x.Key, x.Value)));

            //add business parameters
            for (var i = 0; i < businessParameters.Count(); i += 2)
            {
                var parameter = new KeyValuePair<string, string>(businessParameters[i], businessParameters[i + 1]);
                retval.Add(parameter);
            }

            //sort by parameter name
            retval = retval.OrderBy(x => x.Key.ToString()).ToList();

            //retval.Add("created_after", "2023-07-01T00:00:00+02:00");
            //retval.Add("order_id", "1234");
            return retval;
        }

        Dictionary<string, string> CommonParameters()
        {
            var retval = new Dictionary<string, string>();
            retval.Add("app_key", app_key);
            retval.Add("sign_method", sign_method);
            retval.Add("access_token", access_token);
            retval.Add("timestamp", timestamp);
            return retval;
        }
    }
}
