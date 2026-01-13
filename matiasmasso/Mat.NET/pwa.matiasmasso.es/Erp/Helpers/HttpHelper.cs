using DTO;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using Erp.Shared;
using static System.Net.WebRequestMethods;
using Components;

namespace Erp.Helpers
{
    public class HttpHelper
    {
        private HttpClient Http;
        public bool IsLoaded = false;

        public HttpHelper(HttpClient http)
        {
            Http = http;
        }


        public async Task<ApiResponse<T>> GetAsync2<T>(params string[] segments)
        {
            string url = Globals.ApiUrl(segments);
            if (!string.IsNullOrEmpty(url))
            {
                HttpResponseMessage response = await Http.GetAsync(url);
                return await ResponseMessage2<T>(response, url);
            }
            else
                return ApiResponse<T>.Factory("empty url");
        }


        //public async Task<ApiResponse<T>> PostAsync2<U, T>(U payload, params string[] segments)
        //{
        //    var url = Globals.ApiUrl(segments);
        //    var inputjson = JsonConvert.SerializeObject(payload);
        //    var content = new StringContent(inputjson, System.Text.Encoding.UTF8, "application/json");
        //    return await HttpService.PostAsync<T>(http,url, content);
        //}

        private async Task<ApiResponse<T>> PostAsync2<T>(string url, HttpContent? content = null)
        {
            try
            {
                SetDefaultHeaders();
                HttpResponseMessage response = await Http.PostAsync(url, content);
                return await ResponseMessage2<T>(response, url);
            }
            catch (Exception ex)
            {
                return ApiResponse<T>.Factory(ex);
            }
        }

        private async Task<ApiResponse<T>> ResponseMessage2<T>(HttpResponseMessage response, string url)
        {
            ApiResponse<T> retval = new ApiResponse<T>();
            try
            {

                if (response.IsSuccessStatusCode)
                {
                    var contentType = response.Content?.Headers.ContentType.MediaType;
                    if (contentType == "application/json")
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        if (typeof(T) == typeof(string))
                            retval.Value = (T)(object)responseText;
                        else
                            retval.Value = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText);

                    }
                    else
                    {
                        byte[] result = await response.Content.ReadAsByteArrayAsync();
                        retval.Value = (T)Convert.ChangeType(result, typeof(T));
                    }
                    IsLoaded = true;
                }
                else
                {
                    string responseText = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(responseText))
                        retval.ProblemDetails = new ProblemDetails { Title = response.ReasonPhrase };
                    else
                        retval.ProblemDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<ProblemDetails>(responseText);
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
            catch (System.Exception ex)
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


        protected void SetDefaultHeaders()
        {
            SetAuthHeader();
            SetLangHeader();
        }

        private void SetAuthHeader()
        {
            if (Globals.Token != null)
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Globals.Token);
        }

        private void SetLangHeader()
        {
            var cc = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var currentLangId = LangDTO.FromCultureInfo(cc).ToString();
            var header = Http.DefaultRequestHeaders.FirstOrDefault(x => x.Key == "Lang");
            var httpLangId = header.Value?.FirstOrDefault() ?? "";
            if (currentLangId != httpLangId)
                Http.DefaultRequestHeaders.Add("Lang", currentLangId.ToString());
        }



    }
}
