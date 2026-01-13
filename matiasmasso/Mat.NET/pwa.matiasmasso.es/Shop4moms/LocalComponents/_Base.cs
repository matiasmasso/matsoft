using Shop4moms.Services;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using DTO;
using DTO.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Shop4moms.LocalComponents
{
    public class _Base : ComponentBase
    {
        [Inject] public AuthenticationStateProvider? authStateProvider { get; set; }
        [Inject] public HttpClient http { get; set; }
        [Inject] public CultureService Culture { get; set; }
        [Inject] public ICatalogService Catalog { get; set; }
        [Inject] public NavigationManager? navigationManager { get; set; }
        //[Inject] public StringsLocalizerService? localizerService { get; set; }

        protected CacheDTO? Cache { get => Catalog!.Cache; }
        private LangDTO? _lang { get; set; }


        protected LangDTO Lang()
        {
            _lang ??= DTO.Integracions.Shop4moms.Common.GetLang(new Uri(navigationManager!.Uri));
            return _lang;
        }

        protected void SetLang(LangDTO lang)
        {
            _lang = lang;
        }


        protected Uri? CurrentUri()
        {
            var currentUrl = navigationManager?.Uri;
            return currentUrl == null ? null : new Uri(currentUrl);
        }

        protected string CanonicalUrl(LangDTO? lang = null, LangTextModel? langText = null) => Culture.CanonicalUrl(lang ?? Lang(), langText?.Tradueix(lang ?? Lang()) ?? CurrentUri()?.AbsolutePath);

        protected string RelativeUrl(LangTextModel langText) => Culture.RelativeUrl(Lang(), langText?.Tradueix(Lang()) ?? CurrentUri()?.AbsolutePath);
        protected string RelativeUrl(LangDTO? lang = null, LangTextModel? langText = null) => Culture.RelativeUrl(lang ?? Lang(), langText?.Tradueix(Lang()) ?? CurrentUri()?.AbsolutePath);
        protected string RelativeUrl(string esp, string? cat = null, string? eng = null, string? por = null) => Culture.RelativeUrl(Lang(), Lang().Tradueix(esp, cat, eng, por));

        #region Culture

        protected string Tradueix(string? esp = null, string? cat = null, string? eng = null, string? por = null) => Lang().Tradueix(esp, cat, eng, por);

        //protected string Localizer(string stringKey, params object?[] parameters)
        //{
        //    var retval = localizerService?.Localize(Lang(), stringKey, parameters) ?? stringKey;
        //    return retval;
        //}



        #endregion



        #region Identity
        protected async Task<ClaimsPrincipal> ClaimsPrincipal()
        {
            var authState = await authStateProvider!.GetAuthenticationStateAsync();
            return authState.User;
        }

        protected async Task<bool> IsAuthenticated()
        {
            var authState = await authStateProvider!.GetAuthenticationStateAsync();
            return authState.User?.Identity?.IsAuthenticated ?? false;

        }

        protected async Task<UserModel?> GetUser()
        {
            var claimsPrincipal = await ClaimsPrincipal();
            return UserModel.FromClaimsPrincipal(claimsPrincipal);
        }

        protected async Task<IEnumerable<Claim>> Claims()
        {
            var claimsPrincipal = await ClaimsPrincipal();
            return claimsPrincipal.Claims;
        }



        #endregion

        #region httpClient
        protected async Task<T?> Get2Async<T>(params string[] segments)
        {
            var apiResponse = await HttpService.GetAsync<T>(http, segments);
            if (apiResponse.Success())
                return apiResponse.Value;
            else
                throw new Exception(apiResponse?.ProblemDetails?.Title, new Exception(apiResponse?.ProblemDetails?.Detail));
        }
        protected async Task<T?> Post2Async<U, T>(U payload, params string[] segments)
        {
            var apiResponse = await HttpService.PostAsync<U, T>(http, payload, segments);
            if (apiResponse.Success())
                return apiResponse.Value;
            else
                throw new Exception(apiResponse?.ProblemDetails?.Title, new Exception(apiResponse?.ProblemDetails?.Detail));
        }

        protected async Task<ApiResponse<T>> GetAsync<T>(params string[] segments)
        {
            return await HttpService.GetAsync<T>(http, segments);
        }
        protected async Task<ApiResponse> PostAsync<U>(U payload, params string[] segments)
        {
            return await HttpService.PostAsync<U>(http, payload, segments);
        }
        protected async Task<ApiResponse<T>> PostAsync<U, T>(U payload, params string[] segments)
        {
            return await HttpService.PostAsync<U, T>(http, payload, segments);
        }



        #endregion

        #region Navigation


        public void NavigateBack()
        {

        }
        #endregion

        #region Mail
        public async Task MailUserAsync(string subject, string body, bool copyToInfo)
        {
            var user = await GetUser();
            await MailService.SendEmailAsync(user!.EmailAddress!, subject, body,Lang(), copyToInfo);
        }
        #endregion

    }
}
