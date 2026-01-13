using Blazor.Analytics;
using Services;
using DocumentFormat.OpenXml.Presentation;
using DTO;
using DTO.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Mail;

namespace Web.Services
{
    public class SessionStateService : IDisposable
    {
        public UserModel? User { get; set; }
        public List<EmpModel>? Emps { get; set; }

        public Object? DraggingData { get; set; }

        public void DragData(object args)
        {
            DraggingData = args;
        }

        CustomAuthenticationStateProvider auth;
        private readonly CookieService _cookie;
        private AppStateService appstate;
        private EmpsService empsService;
        private NavService navService;
        public RaffleModel.Participant? RaffleParticipant { get; set; }

        public event Action<Exception?>? OnChange;
        public event Action<UserModel?>? UserChange;

        public SessionStateService(
            AuthenticationStateProvider authStateProvider,
            CookieService cookie,
            AppStateService appstate,
            EmpsService empsService,
            NavService navService)
        {
            this.appstate = appstate;
            this.empsService = empsService;
            this.navService = navService;
            this.auth = (CustomAuthenticationStateProvider)authStateProvider;
            this._cookie = cookie;
            Task task = Task.Run(async () => await CheckPersistedUserAsync());

            auth.AuthenticationStateChanged += AuthenticationStateChanged;
            empsService.OnChange += NotifyChange;
            navService.OnChange += NotifyChange;
        }

        public bool IsLoading() => navService.State == DbState.IsLoading;
        private void NotifyChange(Exception? ex = null)
        {
            Emps = empsService.Values?.Where(x => User?.Emps.Any(y => x.Id == y) ?? false)?.ToList();
            OnChange?.Invoke(ex);
        }


        #region Auth


        public bool IsAuthenticated() => this.User != null;

        async Task CheckPersistedUserAsync()
        {
            var hash = await _cookie.GetValue("hash");
            if (hash != null)
            {
                User = await appstate.PostAsync<string, UserModel>(hash, "User/fromHash", ((int)EmpModel.EmpIds.MatiasMasso).ToString());
                await LoginAsync(User);
            }
        }

        void AuthenticationStateChanged(Task<AuthenticationState> task)
        {
            User = auth.CurrentUser;
            UserChange?.Invoke(User);
            NotifyChange();
        }

        public async Task<bool> LoginAsync(string? emailAddress, string? password)
        {
            bool retval = false;
            if (emailAddress != null && password != null)
            {
                var hash = DTO.Helpers.CryptoHelper.Hash(emailAddress, password!);
                var user = await appstate.PostAsync<string, UserModel>(hash, "User/fromHash", ((int)EmpModel.EmpIds.MatiasMasso).ToString());
                if (user != null)
                {
                    await LoginAsync(user);
                }
            }
            return retval;
        }

        public async Task LoginAsync(UserModel user)
        {
            await auth.LoginAsync(user);
        }

        public async Task LogoutAsync()
        {
            await auth.LogoutAsync();
        }

        //public List<EmpModel>? Emps() => User?.Emps.Select(x => empsService.GetValue((EmpModel.EmpIds)x!)).ToList();

        #endregion



        #region Nav

        public List<NavDTO.MenuItem> SelectedMenuItems { get; set; } = new();
        public List<NavDTO.MenuItem>? DisplayMenuItems() => ChildMenuItems(SelectedMenuItems.LastOrDefault()?.Guid);
        public void SelectMenuItem(NavDTO.MenuItem item)
        {
            if (SelectedMenuItems.Contains(item))
            {
                var idx = SelectedMenuItems.IndexOf(item);
                var removeFrom = idx;
                var removeCount = SelectedMenuItems.Count - removeFrom;
                SelectedMenuItems.RemoveRange(removeFrom, removeCount);
            }
            else
                SelectedMenuItems.Add(item);
        }
        public bool HasChildMenuItems(NavDTO.MenuItem item)
        {
            var retval = MenuItems()?.Any(x => x.Parent == item.Guid);
            return retval ?? false;
        }

        private List<NavDTO.MenuItem>? ChildMenuItems(Guid? parentGuid = null)
        {
            var retval = MenuItems()?.Where(x => x.Parent == parentGuid).OrderBy(x => x.Ord).ToList();
            return retval;
        }

        public EmpModel? Emp(EmpModel.EmpIds id) => Emps?.FirstOrDefault(x=>x.Id==id);
        public EmpModel? DefaultEmp() => Emps?.FirstOrDefault();

        //public LangTextModel? Nom(NavDTO.MenuItem item) => navService.Nom(item);
        public List<NavDTO.MenuItem>? MenuItems()
        {
            var rol = User?.Rol ?? UserModel.Rols.notSet;
            return navService.Values?
                .Where(x => 
                x.Emps.Any(y=>y == appstate.EmpId) &&
                x.Rols.Any(y=>y == rol) 
                )
                .ToList();
        }

        public bool HasLoadedLangTexts() => navService.HasLoadedLangTexts();

        public void ToggleApiSource()
        {
            appstate.UseLocalApi = !appstate.UseLocalApi;
        }


        #endregion

        public void ToggleLocalApi()
        {
            appstate.UseLocalApi = !appstate.UseLocalApi;
        }

        public bool IsUsingLocalApi() => appstate.UseLocalApi;

        public void Dispose()
        {
            auth.AuthenticationStateChanged -= AuthenticationStateChanged;
            empsService.OnChange -= NotifyChange;
            navService.OnChange -= NotifyChange;
        }
    }
}
