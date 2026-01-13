using DTO;


namespace Web.Services
{
    public class CountriesService
    {
        public List<CountryModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;

        public CountriesService(AppStateService appstate)
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
                Values = await appstate.GetAsync<List<CountryModel>>("Countries") ?? new();
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

        public CountryModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);
        public List<CountryModel>? Favorites() => Values?.Where(x => CountryModel.Favorites().Any(y => y.Guid == x.Guid)).ToList();

    }
}

