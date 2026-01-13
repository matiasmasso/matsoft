using DTO;


namespace Web.Services
{
    public class CustomerDeptsService
    {
        public List<CustomerDeptModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;

        public CustomerDeptsService(AppStateService appstate)
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
                    Values = await appstate.GetAsync<List<CustomerDeptModel>>("CustomerDepts") ?? new();
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

        public CustomerDeptModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public async Task<bool> UpdateAsync(CustomerDeptModel value)
        {
            var retval = await appstate.PostAsync<CustomerDeptModel, bool>(value, "CustomerDept");
            if (retval)
            {
                if (value.IsNew) Values?.Add(value);
                value.IsNew = false;
            }
            return retval;
        }
        public async Task<bool> DeleteAsync(CustomerDeptModel value)
        {
            var retval = await appstate.GetAsync<bool>("CustomerDept", value.Guid.ToString());
            if (retval) Values?.Remove(value);
            return retval;

        }
    }
}

