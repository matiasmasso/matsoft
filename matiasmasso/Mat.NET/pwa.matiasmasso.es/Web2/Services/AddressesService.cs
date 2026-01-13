using DTO;


namespace Web.Services
{
    public class AddressesService
    {
        public List<AddressModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;

        public AddressesService(AppStateService appstate)
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
                    Values = await appstate.GetAsync<List<AddressModel>>("Addresses") ?? new();
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

        public AddressModel? GetValue(Guid? srcGuid, AddressModel.Cods? srcCod = AddressModel.Cods.Fiscal)
        {
            AddressModel? retval = null;
            if(srcGuid != null)
                retval = Values?.FirstOrDefault(x=>x.Contact == srcGuid && x.Cod == srcCod);
            return retval;
        }

    }
}

