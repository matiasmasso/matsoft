using DTO;


namespace Web.Services
{
    public class LocationsService
    {
        public List<LocationModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;

        public LocationsService(AppStateService appstate)
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
                Values = await appstate.GetAsync<List<LocationModel>>("Locations") ?? new();
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


        public async Task<bool> UpdateAsync(LocationModel value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.PostAsync<LocationModel, bool>(value, "Location");
                if (retval)
                {
                    if (value.IsNew) Values?.Add(value);
                    Values = Values?.OrderBy(x => x.Nom).ToList();
                    OnChange?.Invoke(null);
                    value.IsNew = false;
                }
            }
            return retval;
        }

        public async Task<bool> DeleteAsync(LocationModel value)
        {
            var retval = await appstate.GetAsync<bool>("Location/delete", value.Guid.ToString());
            Values?.Remove(value);
            OnChange?.Invoke(null);
            return retval;
        }

        public LocationModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

    }
}

