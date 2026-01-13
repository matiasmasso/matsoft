using DTO;
using System.Security.Claims;
using Web;

namespace Web.Services
{
    public class AuthenticationService
    {
        public event Action<ClaimsPrincipal>? UserChanged;
        private ClaimsPrincipal? claimsPrincipal;
        public UserModel? CurrentUser;
        public ClaimsPrincipal ClaimsPrincipal
        {
            get => claimsPrincipal ?? GetAnonymous();
            set => claimsPrincipal = value;
        }
        private CookieService cookieService;

        public AuthenticationService(CookieService cookieService)
        {
            this.cookieService = cookieService;
        }


        public async Task LoginAsync(UserModel user, bool persist = false)
        {
            if (persist) await PersistHash(user.Hash);

            CurrentUser = user;
            var identity = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Name, user.EmailAddress ?? "?"),
                    new Claim(ClaimTypes.Email, user.EmailAddress ?? ""),
                    new Claim(ClaimTypes.UserData, user.Guid.ToString())
                }, "Custom Authentication");

            claimsPrincipal = new ClaimsPrincipal(identity);
            if (UserChanged is not null)
                UserChanged(claimsPrincipal);
        }


        public async Task LogoutAsync()
        {
            await ClearPersistedHash();
            claimsPrincipal = GetAnonymous();
            if (UserChanged is not null)
                UserChanged(claimsPrincipal);
        }

        public async Task<string?> PersistedHash() => await cookieService.GetValue("hash");
        public async Task ClearPersistedHash() => await PersistHash();
        public async Task PersistHash(string? hash = "") => await cookieService.SetValue("hash", hash);

        private ClaimsPrincipal GetAnonymous()
        {
            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Sid, "0"),
            new Claim(ClaimTypes.Name, "Anonymous"),
            new Claim(ClaimTypes.Role, "Anonymous")
        }, null);
            return new ClaimsPrincipal(identity);
            //var userGuid = CurrentUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.UserData)?.Value;
        }

    }
}

