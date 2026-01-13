using DTO;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Shop4moms.Services;

namespace Shop4moms.Services
{
    public class SkusService
    {
        public List<ClaimModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;
        public event Action? OnChange;
        private AppStateService appstate;

        public SkusService(AppStateService appstate)
        {
            this.appstate = appstate;
            _ = Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            State = DbState.IsLoading;
            Values = await appstate.GetAsync<List<ClaimModel>>("Shop4moms/skus");
            State = DbState.StandBy;
            OnChange?.Invoke();
        }


    }
}

