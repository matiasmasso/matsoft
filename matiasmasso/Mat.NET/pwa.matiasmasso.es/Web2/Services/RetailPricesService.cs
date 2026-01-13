using DTO;

namespace Web.Services
{
    public class RetailPricesService
    {
        public List<GuidDecimal>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;

        public RetailPricesService(AppStateService appstate)
        {
            this.appstate = appstate;
            Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                Values = await appstate.GetAsync<List<GuidDecimal>>("RetailPrices/current/1") ?? new();
                State = DbState.StandBy;
                OnChange?.Invoke(null);
            }
        }

        public GuidDecimal? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

    }
}
