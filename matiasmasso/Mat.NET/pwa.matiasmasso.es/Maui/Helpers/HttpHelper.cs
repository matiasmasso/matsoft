using DTO;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;

namespace Maui.Helpers
{
    public class HttpHelper
    {
        private HttpClient Http;
        public bool IsLoaded = false;

        public HttpHelper(HttpClient http)
        {
            Http = http;
        }

        public async Task<T> GetAsync<T>(params string[] segments)
        {
            var url = Globals.ApiUrl(segments);
            SetDefaultHeaders();
            HttpResponseMessage response = await Http.GetAsync(url);
            return await ResponseMessage<T>(response, url);
        }


        protected async Task<T> PostAsync<T>(params string[] segments)
        {
            var url = Globals.ApiUrl(segments);
            return await PostAsync<T>(url);
        }

        public async Task<T> PostAsync<U, T>(U payload, params string[] segments)
        {
            var url = Globals.ApiUrl(segments);
            var inputjson = JsonConvert.SerializeObject(payload);
            var content = new StringContent(inputjson, System.Text.Encoding.UTF8, "application/json");
            return await PostAsync<T>(url, content);
        }

        private async Task<T> PostAsync<T>(string url, HttpContent content = null)
        {
            SetDefaultHeaders();
            HttpResponseMessage response = await Http.PostAsync(url, content);
            return await ResponseMessage<T>(response, url);
        }


        private async Task<T> ResponseMessage<T>(HttpResponseMessage response, string url)
        {
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseText = await response.Content.ReadAsStringAsync();
                    T? retval = JsonConvert.DeserializeObject<T>(responseText);
                    IsLoaded = true;
                    return retval;
                }
                else
                {
                    //problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>() ?? new ProblemDetails();
                    //ShowError(problemDetails);
                }
            }

            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                // Its a timeout issue
                //problemDetails = new ProblemDetails
                //{
                //    Title = "TimeOut Exception on " + url,
                //    Detail = ex.Message + ' ' + ex.StackTrace
                //};
                //ShowError(problemDetails);
                //NotifyStateChanged();
            }
            catch (Exception ex)
            {
                //problemDetails = new ProblemDetails
                //{
                //    Title = "Exception on " + url,
                //    Detail = ex.Message + ' ' + ex.StackTrace
                //};
                //ShowError(problemDetails);
                //NotifyStateChanged();
            }
            return default;

        }

        private static JsonSerializerSettings JsonSettings(List<Exception> exs)
        {
            var retval = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,
                Error = (sender, args) =>
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                    }
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.AppendLine("Error de serialització:");
                    if (args != null && args.ErrorContext != null && args.ErrorContext.OriginalObject != null)
                        sb.AppendLine("en object: " + args.ErrorContext.OriginalObject.ToString());
                    exs.Add(new Exception(sb.ToString()));
                    exs.Add(new Exception(args.ErrorContext.Path));
                }
            };
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
