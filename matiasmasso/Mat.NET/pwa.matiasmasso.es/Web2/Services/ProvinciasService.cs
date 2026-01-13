using DTO;


namespace Web.Services
{
    public class ProvinciasService
    {
        public List<ProvinciaModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;

        public ProvinciasService(AppStateService appstate)
        {
            this.appstate = appstate;
            _ = Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                Values = await appstate.GetAsync<List<ProvinciaModel>>("Provincias") ?? new();
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

        public ProvinciaModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

    }
}

