using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using Spa4.Components;
using Spa4.Shared;
using System.Globalization;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Localization;

namespace Spa4.Components
{

    public class _ComponentBase : ComponentBase, IErrorComponent
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public HttpClient http { get; set; }
        [Inject] public ProtectedLocalStorage ProtectedLocalStore { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }

        public bool IsErrorActive = false;
        public string ErrTitle = string.Empty;
        public string ErrMessage = string.Empty;


        protected async Task<object> DownloadFile(string filename, byte[] byteArray)
        {
            return await JSRuntime.InvokeAsync<object>(
             "downloadFile",
             filename,
             Convert.ToBase64String(byteArray)
         );
        }

        protected LangDTO Lang()
        {
            var cc = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var retval = LangDTO.FromCultureInfo(cc);
            return retval;
        }

        protected string Tradueix(string? esp = null, string? cat = null, string? eng = null, string? por = null)
        {
            return Lang().Tradueix(esp, cat, eng, por);
        }

        public void ShowError(string title, string message)
        {
            IsErrorActive = true;
            ErrTitle = title;
            ErrMessage = message;
            StateHasChanged();
        }
        protected void ShowError(ProblemDetails? problemDetails)
        {
            ShowError(problemDetails?.Title ?? "", problemDetails?.Detail ?? "");
        }

        [CascadingParameter(Name = "ErrorComponent")]
        protected IErrorComponent ErrorComponent { get; set; }
        protected ProblemDetails? problemDetails { get; set; }


        protected event Action? OnStateChange;

        protected bool IsLoaded = false;


        protected async Task WriteCookie(string cookieName, string cookieValue, int? daysDeadline = 0 )
        {
            await JSRuntime.InvokeAsync<object>("WriteCookie.WriteCookie", cookieName, cookieValue, daysDeadline);
        }

        protected async Task<string> ReadCookie(string cookieName)
        {
            return await JSRuntime.InvokeAsync<string>("ReadCookie.ReadCookie", cookieName);
        }



        protected Task<AuthenticationState> authenticationState;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask
        {
            get => authenticationState;
            set
            {
                if (authenticationState == value) return;
                authenticationState = value;
            }
        }

        protected bool IsAuthenticated()
        {
            var authState = authenticationState.Result;
            var user = authState.User;
            var retval = user?.Identity?.IsAuthenticated ?? false;
            return retval;
        }

        protected string? UserId()
        {
            var authState = authenticationState.Result;
            var user = authState.User;
            var retval = IsAuthenticated() ? user?.GetUserId() : null;
            return retval;
        }

        protected void LoadUsrLog(UsrLogModel value)
        {
            string userId = UserId() ?? "";
            if (!string.IsNullOrEmpty(userId))
                value.UsrLastEdited = new GuidNom(new Guid(userId));
            value.FchLastEdited = DateTime.Now;
        }

        //protected async Task<SessionDTO> Session()
        //{
        //    SessionDTO retval;
        //    var result = await ProtectedLocalStore.GetAsync<SessionDTO>("session");
        //    if (result.Success)
        //        retval = result!.Value!;
        //    else
        //    {
        //        retval = SessionDTO.Factory(CultureInfo.CurrentCulture.Name);
        //        await ProtectedLocalStore.SetAsync("session", retval);
        //    }
        //    return retval;
        //}

        #region "HttpClient"

        protected async Task<T> GetAsync<T>(params string[] segments)
        {
            var url = Globals.ApiUrl(segments);
            SetDefaultHeaders();
            HttpResponseMessage response = await http.GetAsync(url);
            return await ResponseMessage<T>(response, url);
        }

        protected async Task<T> PostAsync<T>(params string[] segments)
        {
            var url = Globals.ApiUrl(segments);
            return await PostAsync<T>(url);
        }

        protected async Task<T> PostAsync<U, T>(U payload, params string[] segments)
        {
            var url = Globals.ApiUrl(segments);
            var inputjson = JsonConvert.SerializeObject(payload);
            var content = new StringContent(inputjson, System.Text.Encoding.UTF8, "application/json");
            return await PostAsync<T>(url, content);
        }

        private async Task<T> PostAsync<T>(string url, HttpContent? content = null)
        {
            SetDefaultHeaders();
            HttpResponseMessage response = await http.PostAsync(url, content);
            return await ResponseMessage<T>(response, url);
        }


        private async Task<T> ResponseMessage<T>(HttpResponseMessage response, string url)
        {
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseText = await response.Content.ReadAsStringAsync();
                    T? retval = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText);
                    IsLoaded = true;
                    return retval;
                }
                else
                {
                    problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>() ?? new ProblemDetails();
                    ShowError(problemDetails);
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
                ShowError(problemDetails);
                NotifyStateChanged();
            }
            catch (Exception ex)
            {
                problemDetails = new ProblemDetails
                {
                    Title = "Exception on " + url,
                    Detail = ex.Message + ' ' + ex.StackTrace
                };
                ShowError(problemDetails);
                NotifyStateChanged();
            }
            return default(T);

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
            string? userId = UserId();
            if (userId != null)
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiKey", userId);
        }

        private void SetLangHeader()
        {
            var currentLangId = Lang().Id.ToString();
            var header = http.DefaultRequestHeaders.FirstOrDefault(x => x.Key == "Lang");
            var httpLangId = header.Value?.FirstOrDefault() ?? "";
            if (currentLangId != httpLangId)
                http.DefaultRequestHeaders.Add("Lang", currentLangId);
        }
        #endregion

        protected async Task<CacheDTO> Cache(int empId)
        {
            var retval = AppState.Caches.FirstOrDefault(x => x.EmpId == empId);
            if (retval == null)
            {
                var payload = new CacheDTO.Payload();
                payload.EmpId = empId;
                retval = await PostAsync<CacheDTO.Payload, CacheDTO>(payload, "cache");
                AppState.Caches.Add(retval);
            }
            return retval;

        }

        protected async Task<AtlasModel> Atlas()
        {
            var cache = AppState.DefaultCache();
            var atlas = await PostAsync<AtlasModel, AtlasModel>(cache.Atlas.Request(), "atlas");

            //update cache if needed
            if (atlas.Fch != cache.Atlas.Fch) cache.Atlas = atlas;
            return cache.Atlas;

        }

        protected string PageUrl(params string[] segments)
        {
            var segmentList = new List<string>() { };
            segmentList.AddRange(segments);
            return string.Join("/", segmentList);
        }

        protected void NavigateToPage(params string[] segments)
        {
            NavigationManager.NavigateTo(PageUrl(segments));
        }

        protected void NotifyStateChanged() => OnStateChange?.Invoke();

        public async Task<BrowserDimension> BrowserDimensions()
        {
            return await JSRuntime.InvokeAsync<BrowserDimension>("getDimensions");
        }

    }
}
