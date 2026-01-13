using DTO;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;

namespace Test4moms.Services
{
    public class HttpHelper
    {

        public static async Task<ApiResponse<T>> GetAsync<T>(HttpClient http, params string[] segments)
        {
            string url = Globals.ApiUrl(segments);
            if (!string.IsNullOrEmpty(url))
            {
                HttpResponseMessage response = await http.GetAsync(url);
                return await ResponseMessage<T>(response, url);
            }
            else
                return ApiResponse<T>.Factory("empty url");
        }


        public static async Task<ApiResponse<T>> PostAsync<U, T>(HttpClient http, U payload, params string[] segments)
        {
            var url = segments.Count() == 1 && segments.First().StartsWith("http") ? segments.First() : Globals.ApiUrl(segments);
            var inputjson = JsonConvert.SerializeObject(payload);
            var content = new StringContent(inputjson, System.Text.Encoding.UTF8, "application/json");
            return await PostAsync<T>(http, url, content);
        }

        private static async Task<ApiResponse<T>> PostAsync<T>(HttpClient http, string url, HttpContent? content = null)
        {
            try
            {
                SetDefaultHeaders(http);
                HttpResponseMessage response = await http.PostAsync(url, content);
                return await ResponseMessage<T>(response, url);
            }
            catch (Exception ex)
            {
                return ApiResponse<T>.Factory(ex);
            }
        }

        private static async Task<ApiResponse<T>> ResponseMessage<T>(HttpResponseMessage response, string url)
        {
            ApiResponse<T> retval = new ApiResponse<T>();
            try
            {

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    retval.Value = default;
                }
                else if (response.IsSuccessStatusCode)
                {
                    var contentType = response.Content?.Headers?.ContentType?.MediaType;
                    if (contentType == "application/json" | contentType == "text/html")
                    {
                        string responseText = await response.Content?.ReadAsStringAsync() ?? "";
                        if (typeof(T) == typeof(string))
                            retval.Value = (T)(object)responseText;
                        else
                            retval.Value = JsonConvert.DeserializeObject<T>(responseText);

                    }
                    else
                    {
                        byte[] result = await response.Content.ReadAsByteArrayAsync();
                        retval.Value = (T)Convert.ChangeType(result, typeof(T));
                    }
                }
                else
                {
                    string responseText = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(responseText))
                        retval.ProblemDetails = new ProblemDetails { Title = response.ReasonPhrase };
                    else
                        retval.ProblemDetails = JsonConvert.DeserializeObject<ProblemDetails>(responseText);
                }
            }

            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                // Its a timeout issue
                var problemDetails = new ProblemDetails
                {
                    Title = "TimeOut Exception on " + url,
                    Detail = ex.Message + ' ' + ex.StackTrace
                };
                retval.ProblemDetails = problemDetails;
            }
            catch (Exception ex)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Exception on " + url,
                    Detail = ex.Message + ' ' + ex.StackTrace
                };
                retval.ProblemDetails = problemDetails;
            }
            return retval;
        }


        protected static void SetDefaultHeaders(HttpClient http)
        {
            SetAuthHeader(http);
            SetLangHeader(http);
        }

        private static void SetAuthHeader(HttpClient http)
        {
            if (Globals.Token != null)
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Globals.Token);
        }

        private static void SetLangHeader(HttpClient http)
        {
            var cc = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var currentLangId = LangDTO.FromCultureInfo(cc).ToString();
            var header = http.DefaultRequestHeaders.FirstOrDefault(x => x.Key == "Lang");
            var httpLangId = header.Value?.FirstOrDefault() ?? "";
            if (currentLangId != httpLangId)
                http.DefaultRequestHeaders.Add("Lang", currentLangId.ToString());
        }

    }

}
