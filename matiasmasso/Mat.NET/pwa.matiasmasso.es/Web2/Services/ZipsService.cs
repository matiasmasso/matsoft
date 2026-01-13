using DTO;


namespace Web.Services
{
    public class ZipsService
    {
        public List<ZipModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;

        public ZipsService(AppStateService appstate)
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
                Values = await appstate.GetAsync<List<ZipModel>>("Zips") ?? new();
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

        public async Task<bool> UpdateAsync(ZipModel value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.PostAsync<ZipModel, bool>(value, "Zip");
                if (retval)
                {
                    if (value.IsNew) Values?.Add(value);
                    value.IsNew = false;
                    Values = Values?.OrderBy(x => x.ZipCod).ToList();
                    OnChange?.Invoke(null);
                }
            }
            return retval;
        }

        public async Task<bool> DeleteAsync(ZipModel value)
        {
            var retval = await appstate.GetAsync<bool>("Zip/delete", value.Guid.ToString());
            if (retval) Values?.Remove(value);
            OnChange?.Invoke(null);
            return retval;
        }

        public ZipModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);


    }
}

