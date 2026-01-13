using DTO;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.JSInterop;
using Erp.Shared;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Localization;
using Erp.Resources.Languages;
using static DTO.CacheDTO.Table;
using static DTO.CacheDTO;
using Erp.Models;
using Erp.Services;
using System.Collections.Generic;
using Components;
//using Java.Lang;
//using Java.Net;

namespace Erp.Pages
{
    public class _ComponentBase: _Base
    {
        //[Inject] public NavigationManager NavigationManager { get; set; }
        //[Inject] public HttpClient http { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }
        //[Inject] public AppState AppState { get; set; }
        [Inject] public IMailService MailService { get; set; }


        public bool IsLoadingCache { get; set; } = false;
        public event EventHandler CacheJustLoaded;

        //protected ProblemDetails? problemDetails; // DEPRECATED

        protected UserModel.Rols[] nonEditors = { };
        protected UserModel.Rols[] editors = {
                UserModel.Rols.superUser,
            UserModel.Rols.admin,
            UserModel.Rols.accounts,
            UserModel.Rols.marketing,
            UserModel.Rols.salesManager,
            UserModel.Rols.comercial,
            UserModel.Rols.taller,
            UserModel.Rols.logisticManager,
            UserModel.Rols.operadora
    };

        protected override void OnInitialized()
        {
            var url = navigationManager?.Uri.ToString().Replace("https://0.0.0.0", "");
            BrowserHistory.Add(url);
            base.OnInitialized();
        }


        protected void BackToLastPage()
        {
            BrowserHistory.RemoveLast();
            var url = BrowserHistory.GetLast();
            NavigateToPage(url);

        }

        protected CacheDTO DefaultCache()
        {
            return AppState.DefaultCache();
        }

        private async Task InitiateCatalogIfNeeded()
        {
            var cache = DefaultCache();
            if (cache.Brands.Count == 0 )
            {
                //await Task.Run(() => Catalog());
                await Catalog();
                NotifyStateChanged();
            }
        }

        //gets a cache refreshed on requested tables if needed
        //this should be the standard way to access the cache to make sure the data is updated
        public async Task<CacheDTO> RequestCacheAsync(params CacheDTO.Table.TableIds[] tables)
        {
            IsLoadingCache = true;
            var retval = DefaultCache();
            var request = retval.Request(tables);
            var serverCache = await PostAsync<CacheDTO, CacheDTO>(request, "cache");

            //update client cache if needed
            retval.Merge(serverCache);
            IsLoadingCache = false;
            //CacheJustLoaded?.Invoke(this, new EventArgs());
            return retval;
        }

        //initiates/updates server cache catalog if needed for server calls
        public async Task<CacheDTO> RequestAtlasAsync(params CacheDTO.Table.TableIds[] additionalTables)
        {
            var tables = new List<TableIds>();
            tables.Add(CacheDTO.Table.TableIds.Country);
            tables.Add(CacheDTO.Table.TableIds.Regio);
            tables.Add(CacheDTO.Table.TableIds.Provincia);
            tables.Add(CacheDTO.Table.TableIds.Zona);
            tables.Add(CacheDTO.Table.TableIds.Location);
            tables.Add(CacheDTO.Table.TableIds.Zip);

            tables.AddRange(additionalTables);
            return await RequestCacheAsync(tables.ToArray());
        }

        //initiates/updates server cache catalog if needed for server calls
        public async Task<CacheDTO> RequestCatalogAsync(params CacheDTO.Table.TableIds[] additionalTables)
        {
            CacheDTO.Table.TableIds[] catalogTables = { CacheDTO.Table.TableIds.Tpa
                    , CacheDTO.Table.TableIds.Dept
                    , CacheDTO.Table.TableIds.Stp
                    , CacheDTO.Table.TableIds.Art
                    , CacheDTO.Table.TableIds.PriceListItem_Customer
                    , CacheDTO.Table.TableIds.Arc
                    , CacheDTO.Table.TableIds.Pnc
                    , CacheDTO.Table.TableIds.UrlSegment  };

            var tables = new List<TableIds>();
            tables.AddRange(catalogTables);
            tables.AddRange(additionalTables);
            return await RequestCacheAsync(tables.ToArray());
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
            var cc = System.Globalization.CultureInfo.DefaultThreadCurrentUICulture?.TwoLetterISOLanguageName;
            var retval = LangDTO.FromCultureInfo(cc);
            return retval;
        }

        protected CultureInfo? Culture() => System.Globalization.CultureInfo.DefaultThreadCurrentUICulture;

        protected string Tradueix(string? esp = null, string? cat = null, string? eng = null, string? por = null)
        {
            return Lang().Tradueix(esp, cat, eng, por);
        }


        protected event Action? OnStateChange;

        protected bool IsLoaded = false;



        #region "HttpClient"


        protected async Task<T> GetAsync<T>(params string[] segments)
        {
            string url = Url(segments);
            if (!string.IsNullOrEmpty(url))
            {
                HttpResponseMessage response = await http.GetAsync(url);
                return await ResponseMessage<T>(response, url);
            }
            else
                return default(T);
        }

        protected async Task<ApiResponse<T>> GetAsync2<T>(params string[] segments)
        {
            string url = Url(segments);
            if (!string.IsNullOrEmpty(url))
            {
                HttpResponseMessage response = await http.GetAsync(url);
                return await ResponseMessage2<T>(response, url);
            }
            else
                return ApiResponse<T>.Factory("empty url");
        }

        protected string Url(params string[] segments)
        {
            string retval = "";
            if (segments.Length > 0)
            {
                if (segments.First().StartsWith("http"))
                    retval = Globals.ExternalApiUrl(segments);
                else
                {
                    retval = Globals.ApiUrl(segments);
                    SetDefaultHeaders();
                }
            }
            return retval;
        }



        protected async Task<ApiResponse<T>> PostMultipartAsync<U, T>(U payload, DocFileModel docfile = null, params string[] segments)
        {

            using var content = new MultipartFormDataContent();
            content.Headers.ContentType.MediaType = "multipart/form-data";

            //add the file
            if (docfile?.Document != null)
            {
                var fileStreamContent = new ByteArrayContent(docfile.Document);
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(Media.ContentType((Media.MimeCods)docfile.StreamMime));
                content.Add(fileStreamContent, name: "File", fileName: "File");
            }

            //add the thumbnail
            if (docfile?.Thumbnail != null)
            {
                var fileStreamContent = new ByteArrayContent(docfile.Thumbnail);
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(Media.ContentType((Media.MimeCods)docfile.ThumbnailMime));
                content.Add(fileStreamContent, name: "Thumbnail", fileName: "Thumbnail");
            }

            //add the data
            var inputjson = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var dataStringContent = new StringContent(inputjson, System.Text.Encoding.UTF8, "application/json");
            content.Add(dataStringContent, "Data");

            var url = Globals.ApiUrl(segments);
            return await HttpService.PostAsync<T>(http,url, content);

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
            try
            {
                SetDefaultHeaders();
                HttpResponseMessage response = await http.PostAsync(url, content);
                return await ResponseMessage<T>(response, url);
            }
            catch (Exception ex)
            {
                return default(T);
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

        private async Task<T> ResponseMessage<T>(HttpResponseMessage response, string url)
        {
            var retval = default(T);
            try
            {
                string responseText = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    if (typeof(T) == typeof(string))
                        retval = (T)(object)responseText;
                    else
                        retval = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText);
                    IsLoaded = true;
                }
                //else
                //{
                //    if (string.IsNullOrEmpty(responseText))
                //        problemDetails = new ProblemDetails { Title = response.ReasonPhrase };
                //    else
                //        problemDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<ProblemDetails>(responseText);
                //}
            }

            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                // Its a timeout issue
                //problemDetails = new ProblemDetails
                //{
                //    Title = "TimeOut Exception on " + url,
                //    Detail = ex.Message + ' ' + ex.StackTrace
                //};
            }
            catch (System.Exception ex)
            {
                //problemDetails = new ProblemDetails
                //{
                //    Title = "Exception on " + url,
                //    Detail = ex.Message + ' ' + ex.StackTrace
                //};
            }
            return retval;
        }

        private static JsonSerializerSettings JsonSettings(List<System.Exception> exs)
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
                    exs.Add(new System.Exception(sb.ToString()));
                    exs.Add(new System.Exception(args.ErrorContext.Path));
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
            var retval = string.Join("/", segmentList);
            return retval;
        }

        protected void NavigateToPage(params string[] segments)
        {
            base.navigationManager?.NavigateTo(PageUrl(segments));
        }
        protected void NavigateToPageAndReload(params string[] segments)
        {
            base.navigationManager?.NavigateTo(PageUrl(segments), true);
        }

        protected void NotifyStateChanged() => OnStateChange?.Invoke();

        public BrowserDimension BrowserDimensions()
        {
            var info = DeviceDisplay.Current.MainDisplayInfo;
            return new BrowserDimension((int)info.Width, (int)info.Height);
        }

        public bool IsAuthenticated() => Globals.Token != null;

        protected async Task<CacheDTO> Cache(params Table.TableIds[] tables)
        {
            var cache = AppState.DefaultCache();
            var serverCache = await PostAsync<CacheDTO, CacheDTO>(cache.Request(tables), "cache");
            //update cache if needed
            cache.Merge(serverCache);
            return cache;

        }
        protected async Task<CacheDTO> Atlas()
        {
            var retval = AppState.DefaultCache();
            ApiResponse<CacheDTO> apiResponse = await HttpService.PostAsync<CacheDTO, CacheDTO>(http, retval.AtlasRequest(), "cache");
            if (apiResponse.Success())
            {
                var serverCache = apiResponse.Value;
                //update cache if needed
                retval.Merge(serverCache);
            }
            return retval;

            //var atlas = await PostAsync<AtlasModel, AtlasModel>(cache.Atlas.Request(), "atlas");

            ////update cache if needed
            //if (atlas.Fch != cache.Atlas.Fch) cache.Atlas = atlas;
            //return cache.Atlas;
        }

        protected async Task<CacheDTO> Catalog()
        {
            var cache = AppState.DefaultCache();
            ApiResponse<CacheDTO> apiResponse = await HttpService.PostAsync<CacheDTO, CacheDTO>(http, cache.CatalogRequest(), "cache");
            if (apiResponse.Success())
            {
                var serverCache = apiResponse.Value;
                //update cache if needed
                cache.Merge(serverCache);
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

        protected bool IsEditor() => editors.Contains(UserRol()) && !nonEditors.Contains(UserRol());
        protected bool IsStaff()
        {
            UserModel.Rols[] rols = {
                UserModel.Rols.superUser,
            UserModel.Rols.admin,
            UserModel.Rols.accounts,
            UserModel.Rols.marketing,
            UserModel.Rols.salesManager,
            UserModel.Rols.comercial,
            UserModel.Rols.employee,
            UserModel.Rols.taller,
            UserModel.Rols.logisticManager,
            UserModel.Rols.operadora
    };
            return rols.Contains(UserRol());
        }

        protected bool IsCustomer()
        {
            UserModel.Rols[] rols = {
                UserModel.Rols.cliLite,
            UserModel.Rols.cliFull
     };
            return rols.Contains(UserRol());
        }


        protected Guid? UserGuid()
        {
            var claim = UserClaim("UserId");
            return claim == null ? null : new Guid(claim.Value);
        }

        protected string? UserEmailAddress() => UserClaim("Email")?.Value;
        protected UserModel.Rols UserRol()
        {
            UserModel.Rols retval = UserModel.Rols.unregistered;
            var claim = UserClaim("Rol");
            if (claim != null)
            {
                retval = (UserModel.Rols)Convert.ToInt32(claim.Value);
            }
            return retval;
        }

        protected System.Security.Claims.Claim? UserClaim(string type)
        {
            System.Security.Claims.Claim retval = null;
            var token = Globals.Token;
            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var claims = jwtSecurityToken.Claims.ToList();
                retval = claims.FirstOrDefault(x => x.Type == type);
            }
            return retval;
        }


        protected List<EmpModel.EmpIds> UserEmps()
        {
            List<EmpModel.EmpIds> retval = null;
            var token = Globals.Token;
            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var claims = jwtSecurityToken.Claims.ToList();
                retval = claims
                    .Where(x => x.Type == "Emps")
                    .Select(x=>(EmpModel.EmpIds)Convert.ToInt32(x.Value))
                    .ToList();
            }
            return retval;
        }

        public async Task CopyToClipboard(string text)
        {
            var service = new ClipboardService(JSRuntime);
            await service.CopyToClipboard(text);
        }

        public async Task MailUserAsync(string subject, string body)
        {
            var htmlBody = EmpModel.MailBody(EmpModel.EmpIds.MatiasMasso, body);
            await MailService.SendEmailAsync(UserEmailAddress(), subject, htmlBody);
        }

        public async Task MailCTOAsync(string subject, string body)
        {
            var htmlBody = EmpModel.MailBody(EmpModel.EmpIds.MatiasMasso, body);
            await MailService.SendEmailAsync("matias@matiasmasso.es", subject, htmlBody);
        }

        public async Task MailInfoAsync(string subject, string body)
        {
            var htmlBody = EmpModel.MailBody(EmpModel.EmpIds.MatiasMasso, body);
            await MailService.SendEmailAsync("info@matiasmasso.es", subject, htmlBody);
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var htmlBody = EmpModel.MailBody(EmpModel.EmpIds.MatiasMasso, body);
            await MailService.SendEmailAsync(to, subject, htmlBody);
        }



    }
}
