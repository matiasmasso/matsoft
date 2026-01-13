using DTO;


namespace Web.Services
{
    public class ZonasService
    {
        public List<ZonaModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;

        public ZonasService(AppStateService appstate)
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
                    Values = await appstate.GetAsync<List<ZonaModel>>("Zonas") ?? new();
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

        public async Task<bool> UpdateAsync(ZonaModel value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.PostAsync<ZonaModel, bool>(value, "Zona");
                if (retval)
                {
                    if (value.IsNew) Values?.Add(value);
                    value.IsNew = false;
                    Values = Values?.OrderBy(x => x.Nom).ToList();
                    OnChange?.Invoke(null);
                }
            }
            return retval;
        }

        public async Task<bool> DeleteAsync(ZonaModel value)
        {
            var retval = await appstate.GetAsync<bool>("Zona/delete", value.Guid.ToString());
            Values?.Remove(value);
            OnChange?.Invoke(null);
            return retval;
        }
        public ZonaModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

    }
}

