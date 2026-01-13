using DTO;

namespace Web.Services
{
    public class StoreLocatorService
    {
        public List<TemplateModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action? OnChange;
        private AppStateService appstate;

        public StoreLocatorService(AppStateService appstate)
        {
            this.appstate = appstate;
            _ = Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                Values = await appstate.GetAsync<List<TemplateModel>>("StoreLocator") ?? new();
                State = DbState.StandBy;
                OnChange?.Invoke();
            }
        }

        //public TemplateModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

    }
}


