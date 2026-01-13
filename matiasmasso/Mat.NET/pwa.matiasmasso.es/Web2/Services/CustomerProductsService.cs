using DTO;

namespace Web.Services
{
    public class CustomerProductsService
    {
        public List<CustomerProductModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;

        public CustomerProductsService(AppStateService appstate)
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
                    Values = await appstate.GetAsync<List<CustomerProductModel>>("CustomerProducts") ?? new();
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

        public CustomerProductModel? GetValue(Guid? customer, Guid? sku) => (customer == null || sku == null)? null : Values?.FirstOrDefault(x => x.Customer == customer && x.Sku == sku);
    }

}
