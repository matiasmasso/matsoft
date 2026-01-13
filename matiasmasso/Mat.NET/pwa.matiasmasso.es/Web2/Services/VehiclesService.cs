using DTO;


namespace Web.Services
{
    public class VehiclesService
    {
        public List<VehicleModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private ApiClientService apiclient;

        public VehiclesService(ApiClientService apiclient)
        {
            this.apiclient = apiclient;
            _ = Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    Values = await apiclient.GetAsync<List<VehicleModel>>("Vehicles") ?? new();
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

        public async Task<VehicleModel?> FindAsync(Guid guid)
        {
            VehicleModel? retval = null;
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                retval = await apiclient.GetAsync<VehicleModel>("Vehicle", guid.ToString()) ?? new();
                State = DbState.StandBy;
            }
            return retval;
        }
        public VehicleModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public async Task<bool> UpdateAsync(VehicleModel? value)
        {
            var retval = await apiclient.PostAsync<VehicleModel, bool>(value, "Vehicle");
            if (retval)
            {
                if (value.IsNew) Values?.Add(value);
                value.IsNew = false;
            }
            return retval;
        }
        public async Task DeleteAsync(VehicleModel? value)
        {
            if (value != null)
            {
                var retval = await apiclient.GetAsync<bool>("Vehicle/delete", value.Guid.ToString());
                if (retval) Values?.Remove(value);
            }
        }
    }
}

