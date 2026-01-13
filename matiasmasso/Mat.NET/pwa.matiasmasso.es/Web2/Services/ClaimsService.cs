using DTO;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Web.Services
{
    public class ClaimsService
    {
        public List<ClaimModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;
        public event Action<Exception?>? OnChange;
        private AppStateService appstate;

        public ClaimsService(AppStateService appstate)
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
                    Values = await appstate.GetAsync<List<ClaimModel>>("Claims") ?? new();
                    OnChange?.Invoke(null);
                }
                catch (Exception ex)
                {
                    State = DbState.Failed;
                    OnChange?.Invoke(ex);
                }
                State = DbState.StandBy;
            }
        }

        public async Task UpdateAsync(ClaimModel value)
        {
            await appstate.PostAsync<ClaimModel, bool>(value, "Claim");
            await FetchAsync();
        }

        public async Task DeleteAsync(ClaimModel value)
        {
            await appstate.GetAsync<bool>("Claim/delete",value.Guid.ToString());
            await FetchAsync();
        }

        public ClaimModel? GetValue(Guid? guid) => Values?.FirstOrDefault(x => x.Guid == guid);

    }
}
