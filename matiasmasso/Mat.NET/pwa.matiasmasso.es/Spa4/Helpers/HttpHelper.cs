using DTO;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net.Http.Headers;

namespace Spa4.Helpers
{
    public class HttpHelper
    {
        private HttpClient Http;
        private ProblemDetails? problemDetails;
        private Task<AuthenticationState> AuthenticationState;

        public HttpHelper(HttpClient http, Task<AuthenticationState> authenticationState)
        {
            Http = http;
            AuthenticationState = authenticationState;
        }

        public async Task<T> GetAsync<T>(params string[] segments)
        {
            var url = Globals.ApiUrl(segments);
            SetDefaultHeaders();
            HttpResponseMessage response = await Http.GetAsync(url);
            return await ResponseMessage<T>(response, url);
        }

        private void SetDefaultHeaders()
        {
            SetAuthHeader();
            SetLangHeader();
        }

        private void SetAuthHeader()
        {
            string? userId = UserId();
            if (userId != null)
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiKey", userId);
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

        private async Task<T> ResponseMessage<T>(HttpResponseMessage response, string url)
        {
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseText = await response.Content.ReadAsStringAsync();
                    T? retval = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText);
                    return retval;
                }
                else
                {
                    problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>() ?? new ProblemDetails();
                }
            }

            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                // Its a timeout issue
                problemDetails = new ProblemDetails
                {
                    Title = "TimeOut Exception on " + url,
                    Detail = ex.Message + ' ' + ex.StackTrace
                };
            }
            catch (Exception ex)
            {
                problemDetails = new ProblemDetails
                {
                    Title = "Exception on " + url,
                    Detail = ex.Message + ' ' + ex.StackTrace
                };
            }
            return default(T);

        }

        private bool IsAuthenticated()
        {
            var authState = AuthenticationState.Result;
            var user = authState.User;
            var retval = user?.Identity?.IsAuthenticated ?? false;
            return retval;
        }

        private string? UserId()
        {
            var authState = AuthenticationState.Result;
            var user = authState.User;
            var retval = IsAuthenticated() ? user?.GetUserId() : null;
            return retval;
        }


    }
}
