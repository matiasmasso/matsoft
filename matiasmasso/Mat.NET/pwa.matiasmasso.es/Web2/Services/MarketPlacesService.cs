using DTO;


namespace Web.Services
{
    public class MarketPlacesService
    {
        public List<MarketPlaceModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;

        public MarketPlacesService(AppStateService appstate)
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
                    Values = await appstate.GetAsync<List<MarketPlaceModel>>("MarketPlaces", ((int)appstate.EmpId).ToString()) ?? new();
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

        public MarketPlaceModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public async Task<bool> UpdateAsync(MarketPlaceModel value)
        {
            var retval = await appstate.PostAsync<MarketPlaceModel, bool>(value, "marketplace");
            if (retval)
            {
                if (value.IsNew) Values?.Add(value);
                value.IsNew = false;
                OnChange?.Invoke(null);
            }
            return retval;
        }
        public async Task<bool> DeleteAsync(MarketPlaceModel value)
        {
            var retval = await appstate.GetAsync<bool>("marketplace", value.Guid.ToString());
            if (retval)
            {
                Values?.Remove(value);
                OnChange?.Invoke(null);
            }
            return retval;

        }

    }
}


