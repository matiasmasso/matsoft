using DocumentFormat.OpenXml.Office2010.Drawing;
using DocumentFormat.OpenXml.Office2013.ExcelAc;
using DTO;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Services;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using static System.Net.WebRequestMethods;

namespace Web.Services
{
    public class ApiClientService
    {
        private HttpClient http;
        private SessionStateService session;
        private CultureService culture;
        private string? apiKey;

        private const string LOCALAPI_HOST = "localhost:7111";
        private const string REMOTEAPI_HOST = "api2.matiasmasso.es";
        public ApiClientService(HttpClient http, SessionStateService session, CultureService culture)
        {
            this.http = http;
            this.session = session;
            this.culture = culture;
        }

        public async Task<T?> GetAsync<T>(params string[] segments)
        {
            var request = RequestMessage(HttpMethod.Get, segments);
            var response = await http.SendAsync(request);
            T? retval = await Deserialize<T>(response, AbsolutePath(segments));
            return retval;
        }

        public async Task<T?> PostAsync<U,T>(U payload, params string[] segments)
        {
            var request = RequestMessage(HttpMethod.Post, segments);
            var inputjson = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            //var dataStringContent = new StringContent(inputjson, System.Text.Encoding.UTF8, "application/json");
            //request.Content.Add(dataStringContent, "Data");
            request.Content = JsonContent.Create(payload,typeof(U));
            var response = await http.SendAsync(request);

            T? retval = await Deserialize<T>(response, AbsolutePath(segments));
            return retval;
        }




        #region Utilities

        private static async Task<T?> Deserialize<T>(HttpResponseMessage response, string url)
        {
            T? retval = default;

            if (response.StatusCode == HttpStatusCode.NoContent)
                retval = default;
            else if (response.IsSuccessStatusCode)
            {
                try
                {
                    retval = await Read<T>(response);
                }
                catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
                {
                    throw new Exception("TimeOut Exception on " + url);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message + " " + url + " " + ex.InnerException?.Message ?? "");
                }
            }
            else
            {
                string responseText = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(responseText))
                    throw new Exception(response.ReasonPhrase + " " + url);
                else
                    throw new Exception(responseText + " " + url);
            }

            return retval;
        }

        private static async Task<T?> Read<T>(HttpResponseMessage response)
        {
            T? retval = default;
            if (IsSerialized(response))
                retval = await StringRead<T>(response);
            else
                retval = await BinaryRead<T>(response);
            return retval;
        }
        private static async Task<T?> BinaryRead<T>(HttpResponseMessage response)
        {
            T? retval = default;
            byte[]? result = response.Content == null ? null : await response.Content.ReadAsByteArrayAsync();
            retval = (T?)Convert.ChangeType(result, typeof(T));
            return retval; ;
        }

        private static async Task<T?> StringRead<T>(HttpResponseMessage response)
        {
            T? retval = default;
            string responseText = response.Content == null ? "" : await response.Content.ReadAsStringAsync() ?? "";
            if (typeof(T) == typeof(string))
                retval = (T)(object)responseText;
            else
                retval = JsonConvert.DeserializeObject<T>(responseText);
            return retval; ;
        }

        private static bool IsSerialized(HttpResponseMessage response)
        {
            var contentType = response.Content?.Headers?.ContentType?.MediaType;
            var retval = (contentType == "application/json" | contentType == "text/html" | contentType == "text/plain");
            return retval;
        }

        private HttpRequestMessage RequestMessage(HttpMethod httpMethod, params string[] segments)
        {
            return IsExternalApiCall(segments) ? ExternalApiRequestMessage(httpMethod, segments) : CorporateApiRequestMessage(httpMethod, segments);
        }

        private HttpRequestMessage ExternalApiRequestMessage(HttpMethod httpMethod, params string[] segments)
        {
            return new HttpRequestMessage(httpMethod, AbsolutePath(segments));
        }
        private HttpRequestMessage CorporateApiRequestMessage(HttpMethod httpMethod, params string[] segments)
        {
            var url = $"https://{ApiHost()}/{AbsolutePath(segments)}";
            var retval = new HttpRequestMessage(httpMethod, url);
            AddApiKey(retval);
            return retval;
        }

        private bool IsExternalApiCall(params string[] segments) => segments.Length > 0 && segments.First().StartsWith("http");
        private string ApiHost() => session.IsUsingLocalApi() ? LOCALAPI_HOST : REMOTEAPI_HOST;
        private string AbsolutePath(string[] segments)
        {
            char[] charsToTrim = { '/', ' ' };
            var trimmedSegments = segments
                .Select(x => x.Trim(charsToTrim))
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();
            return string.Join("/", trimmedSegments);
        }
        private void AddApiKey(HttpRequestMessage request)
        {
            if (session.User != null)
                request.Headers.Add("ApiKey", session.User.Guid.ToString());
            request.Headers.Add("Lang", culture.Lang().ToString());
        }

        #endregion

    }
}
