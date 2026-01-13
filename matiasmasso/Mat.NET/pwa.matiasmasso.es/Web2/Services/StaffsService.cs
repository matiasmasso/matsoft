using DTO;


namespace Web.Services
{
    public class StaffsService
    {
        public List<StaffModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;

        public StaffsService(AppStateService appstate)
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
                    Values = await appstate.GetAsync<List<StaffModel>>("Staffs") ?? new();
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

        public StaffModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public StaffModel? GetValueByNumSegSocial(string numSegSocial, EmpModel? emp) => Values?.FirstOrDefault(x => x.Emp == emp?.Id && (x.NumSS?.Matches(numSegSocial) ?? false));

        public async Task<StaffModel?> FindAsync(Guid guid)
        {
            StaffModel? retval = null;
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    retval = await appstate.GetAsync<StaffModel>("Staff", guid.ToString()) ?? new();
                    State = DbState.StandBy;
                }
                catch (Exception ex)
                {
                    State = DbState.Failed;
                }
            }
            return retval;
        }

        public async Task<bool> UpdateAsync(StaffModel value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.PostAsync<StaffModel, bool>(value, "Staff");
                if (retval)
                {
                    if (value.IsNew) Values?.Add(value);
                    value.IsNew = false;
                }
            }
            return retval;
        }


        public async Task<bool> DeleteAsync(StaffModel? value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.GetAsync<bool>("Staff", value.Guid.ToString());
                if (retval) Values?.Remove(value);
            }
            return retval;

        }
    }
}

