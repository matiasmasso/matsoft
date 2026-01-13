using DTO;
using Components;
using Erp.Helpers;

namespace Erp.Services
{
    public class AppState
    {
        private HttpClient http;
        private CustomAuthenticationStateProvider authStateProvider;

        public List<NavDTO.Model> MenuItems = new();
        public List<NavDTO.Model> SelectedMenuItems = new();
        public NavDTO? Nav;
        public bool IsLoadingMenu = true;
        public event EventHandler<EventArgs>MenuJustLoaded;



        public bool CanEdit { get; set; } = true; // provisional; cal assignar un valor en funció de l'usuari

        public AppState(HttpClient http, CustomAuthenticationStateProvider authStateProvider)
        {
            IsLoadingCatalog = true;
            IsLoadingAtlas = true;

            this.http = http;
            this.authStateProvider = authStateProvider;

            _ = StartupTasks();

        }

        #region SessionsManagement
        public List<SessionState> Sessions { get; set; } = new();
        public SessionState? Session(Guid? sessionId) => Sessions.FirstOrDefault(x => x.Guid == sessionId);
        public SessionState AddSession()
        {
            var retval = new SessionState(http);
            Sessions.Add(retval);
            return retval;
        }
        #endregion

        public void ResetNav()
        {
            MenuItems = new List<NavDTO.Model>();
            SelectedMenuItems = new List<NavDTO.Model>();
            Nav = null;
        }

        public async Task StartupTasks()
        {
            var state = await authStateProvider.GetAuthenticationStateAsync();

            await LoadMenu();

            //preload cache with products and atlas

            await InitiateCatalogCache();
            IsLoadingCatalog = false;
            IsLoadedCatalog = true;
            CatalogJustLoaded?.Invoke(this, new EventArgs());

            await InitiateAtlasCache();
            IsLoadingAtlas = false;
            AtlasJustLoaded?.Invoke(this, new EventArgs());
        }
        public async Task LoadMenu()
        {
            IsLoadingMenu = true;

            var apiResponse = await HttpService.GetAsync<NavDTO>(http, "navs/custom", ((int)EmpModel.EmpIds.MatiasMasso).ToString());
            if (apiResponse.Success())
            {
                Nav = apiResponse.Value;
                MenuItems = Nav.Items.Where(x => x.Parent == null).OrderBy(y => y.Ord).ToList();
                IsLoadingMenu = false;
                MenuJustLoaded.Invoke(this, new EventArgs());
            }
            //else
            //{
            //    problemDetails = apiResponse.ProblemDetails;
            //}
        }

        #region "Cache"

        public bool IsLoadedCatalog { get; set; } = false;
        public bool IsLoadingCatalog { get; set; } = false;
        public bool IsLoadingAtlas { get; set; } = false;
        public event EventHandler<EventArgs> CatalogJustLoaded;
        public event EventHandler<EventArgs> AtlasJustLoaded;



        private async Task InitiateCatalogCache()
        {
            var cache = AppState.DefaultCache();
            var apiResponse = await HttpService.PostAsync<CacheDTO, CacheDTO>(http, cache.CatalogRequest(), "cache");
            if (apiResponse.Success())
            {
                var serverCache = apiResponse.Value;
                //update cache if needed
                cache.Merge(serverCache);
            }
            IsLoadingCatalog = false;
        }

        public async Task InitiateAtlasCache()
        {
            var cache = AppState.DefaultCache();
            var apiResponse = await HttpService.PostAsync<CacheDTO, CacheDTO>(http, cache.AtlasRequest(), "cache");
            if (apiResponse.Success())
            {
                var serverCache = apiResponse.Value;
                //update cache if needed
                cache.Merge(serverCache);
            }
        }



        public static CacheDTO DefaultCache() => Cache(Globals.DefaultEmp);
        public static List<CacheDTO> Caches { get; set; } = new();
        public static CacheDTO Cache(DTO.EmpModel.EmpIds empId)
        {
            var retval = Caches.FirstOrDefault(x => x.EmpId == empId);
            if (retval == null)
            {
                retval = new CacheDTO(empId);
                Caches.Add(retval);
            }
            return retval;
        }


        #endregion
    }
}
