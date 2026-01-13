using DTO;


namespace Web.Services
{
    public class RepsService
    {
        public List<RepModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;

        public RepsService(AppStateService appstate)
        {
            this.appstate = appstate;
            _ = Task.Run(async () => await FetchAsync());
        }

        public async Task<RepModel?> FindAsync(Guid guid)
        {
            return await appstate.GetAsync<RepModel>("rep", guid.ToString());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    Values = await appstate.GetAsync<List<RepModel>>("Reps",( (int)appstate.EmpId).ToString()) ?? new();
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

        public RepModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public async Task<bool> UpdateAsync(RepModel value)
        {
            var retval = await appstate.PostAsync<RepModel, bool>(value, "Rep");
            if (retval)
            {
                if (value.IsNew) Values?.Add(value);
                value.IsNew = false;
            }
            return retval;
        }
        public async Task<bool> DeleteAsync(RepModel value)
        {
            var retval = await appstate.GetAsync<bool>("Rep", value.Guid.ToString());
            if (retval) Values?.Remove(value);
            return retval;

        }
    }
}

