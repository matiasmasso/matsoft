using DTO;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.JSInterop;
using Maui.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Localization;
using Maui.Resources.Languages;

namespace Maui.Pages
{
    public class _ComponentBase : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public HttpClient http { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public AppState AppState { get; set; }


        protected override async Task OnInitializedAsync()
        {
            if (CultureInfo.DefaultThreadCurrentCulture.Name != CultureInfo.CurrentCulture.Name)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.DefaultThreadCurrentCulture;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.DefaultThreadCurrentCulture;
            }
            //await InitiateCatalogIfNeeded();
        }

        private async Task InitiateCatalogIfNeeded()
        {
            var cache = AppState.DefaultCache();
            if (cache.Brands.Count == 0 && !AppState.IsLoadingCatalog)
            {
                //await Task.Run(() => Catalog());
                await Catalog();
                NotifyStateChanged();
            }
        }

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
            //var cc = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var cc = CultureInfo.DefaultThreadCurrentUICulture.TwoLetterISOLanguageName;
            var retval = LangDTO.FromCultureInfo(cc);
            return retval;
        }

        protected string Tradueix(string? esp = null, string? cat = null, string? eng = null, string? por = null)
        {
            return Lang().Tradueix(esp, cat, eng, por);
        }


        protected event Action? OnStateChange;

        protected bool IsLoaded = false;



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
                    T? retval = default(T);
                    string responseText = await response.Content.ReadAsStringAsync();
                    if (typeof(T) == typeof(string))
                        retval = (T)(object)responseText;
                    else
                        retval = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText);
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
                NotifyStateChanged();
            }
            catch (Exception ex)
            {
                //problemDetails = new ProblemDetails
                //{
                //    Title = "Exception on " + url,
                //    Detail = ex.Message + ' ' + ex.StackTrace
                //};
                //ShowError(problemDetails);
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
            if (Globals.Token != null)
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Globals.Token);
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
        protected void NavigateToPageAndReload(params string[] segments)
        {
            NavigationManager.NavigateTo(PageUrl(segments), true);
        }

        protected void NotifyStateChanged() => OnStateChange?.Invoke();

        public BrowserDimension BrowserDimensions()
        {
            var info = DeviceDisplay.Current.MainDisplayInfo;
            return new BrowserDimension((int)info.Width, (int)info.Height);
        }

        public bool IsAuthenticated() => Globals.Token != null;

        protected async Task<AtlasModel> Atlas()
        {
            var cache = AppState.DefaultCache();
            var atlas = await PostAsync<AtlasModel, AtlasModel>(cache.Atlas.Request(), "atlas");

            //update cache if needed
            if (atlas.Fch != cache.Atlas.Fch) cache.Atlas = atlas;
            return cache.Atlas;

        }
        protected async Task<CacheDTO> Catalog()
        {
            var cache = AppState.DefaultCache();
            if (!AppState.IsLoadingCatalog)
            {
                AppState.IsLoadingCatalog = true;
                var serverCache = await PostAsync<CacheDTO, CacheDTO>(cache.CatalogRequest(), "cache");

                //update cache if needed
                cache.Merge(serverCache);
                AppState.IsLoadingCatalog = false;
            }
            return cache;
        }

        protected async Task UpdateCacheIfNeeded(CacheDTO request)
        {
            var refreshedCache = await PostAsync<CacheDTO, CacheDTO>(request, "cache");
        }

        protected void LoadUsrLog(UsrLogModel value)
        {
            value.UsrLastEdited = new GuidNom((Guid)UserGuid());
            value.FchLastEdited = DateTime.Now;
        }

        protected static Guid? UserGuid()
        {
            Guid? retval = null;
            var token = Globals.Token;
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var claims = jwtSecurityToken.Claims.ToList();
            var userIdClaim = claims.FirstOrDefault(x => x.Type == "UserId");
            if (userIdClaim != null)
                retval = new Guid(userIdClaim.Value);
            return retval;
        }

    }
}
