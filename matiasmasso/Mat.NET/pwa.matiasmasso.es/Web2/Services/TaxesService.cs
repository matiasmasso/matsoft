using DTO;


namespace Web.Services
{
    public class TaxesService
    {
        public List<TaxModel>? Values { get; set; }

        public event Action<Exception?>? OnChange;
        public DbState State { get; set; } = DbState.StandBy;
        private AppStateService appstate;

        public TaxesService(AppStateService appstate)
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
                    Values = await appstate.GetAsync<List<TaxModel>>("Taxes") ?? new();
                    OnChange?.Invoke(null);
                }
                catch (Exception ex)
                {
                    State = DbState.Failed;
                    OnChange?.Invoke(ex);
                }
                State = DbState.StandBy;
            }
        }

        public TaxModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public async Task<bool> UpdateAsync(TaxModel value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.PostAsync<TaxModel, bool>(value, "Tax");
                if (retval)
                {
                    if (value.IsNew) Values?.Add(value);
                    value.IsNew = false;
                }
            }
            return retval;
        }


        public async Task<bool> DeleteAsync(TaxModel? value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.GetAsync<bool>("Tax/delete", value.Guid.ToString());
                if (retval) Values?.Remove(value);
            }
            return retval;
        }
    }
}

