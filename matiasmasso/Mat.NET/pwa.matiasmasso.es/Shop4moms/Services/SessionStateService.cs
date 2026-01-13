using DTO;
using DTO.Helpers;
using Microsoft.AspNetCore.Components.Authorization;

namespace Shop4moms.Services
{
    public class SessionStateService:IDisposable
    {
        public UserModel? User
        {
            get { return _user; }
            set { 
                _user = value; }
        }

        private UserModel? _user;

        CustomAuthenticationStateProvider auth;
        private readonly CookieService _cookie;
        private AppStateService appstate;

        public event Action<UserModel?>? UserChanged;
        public event Action? ShowAllProductsOnChange;

        public bool ShowAllProducts
        {
            get => _showAllProducts;
            set
            {
                _showAllProducts = value;
                ShowAllProductsOnChange?.Invoke();
            }
        }

        private bool _showAllProducts = false;


        public SessionStateService(AuthenticationStateProvider authStateProvider, CookieService cookie, AppStateService appstate) { 
            this.appstate = appstate;
            this.auth = (CustomAuthenticationStateProvider)authStateProvider;
            this._cookie = cookie;
            Task task = Task.Run(async () => await CheckPersistedUserAsync());
            auth.AuthenticationStateChanged += AuthenticationStateChanged;
        }

        public bool IsAuthenticated() => this.User != null;

        async Task CheckPersistedUserAsync()
        {
            var hash = await _cookie.GetValue("hash");
            if (hash != null)
            {
                var user = await appstate.PostAsync<string, UserModel>(hash, "User/fromHash", ((int)EmpModel.EmpIds.MatiasMasso).ToString());
                await LoginAsync(user);
            }
        }

        async void AuthenticationStateChanged(Task<AuthenticationState> task)
        {
            User = auth.CurrentUser;
            UserChanged?.Invoke(User);

            //UserChanged.Invoke(this, new MatEventArgs<UserModel>(User));
            //UserChanged?.DynamicInvoke(this, new DTO.Helpers.MatEventArgs<UserModel>(User));
        }

        public async Task LoginAsync(UserModel user)
        {
            await auth.LoginAsync(user);
        }

        public async Task LogoutAsync()
        {
            await auth.LogoutAsync();
        }


        public void Dispose()
        {
            auth.AuthenticationStateChanged -= AuthenticationStateChanged;
        }

    }
}
