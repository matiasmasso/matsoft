using DTO;
using Microsoft.AspNetCore.Components.Server;
using System;

namespace Web.Services
{
    public class CanonicalUrlsService
    {
        public List<CanonicalUrlModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;
        public event Action<Exception?>? OnChange;

        private AppStateService appstate;

        public CanonicalUrlsService(AppStateService appstate)
        {
            this.appstate = appstate;
            Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    Values = await appstate.GetAsync<List<CanonicalUrlModel>>("ProductUrls") ?? new();
                    State = DbState.StandBy;
                    OnChange?.Invoke(null);
                }
                catch (Exception ex)
                {
                    State = DbState.Failed;
                    OnChange?.Invoke(ex);
                }
            }
        }


        public CanonicalUrlModel? GetValue(string? landingPage) => Values?.FirstOrDefault(x => x.Url.Matches(landingPage));

        public CanonicalUrlModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Target == guid);


    }
}
