using DTO;
namespace Web.Services
{
    public class ProductSkuStocksService
    {
        public List<SkuStockModel>? Values {
            get {  
                if(NeedsUpdate()) _ = Task.Run(async () => await FetchAsync());
                return _values; 
            }
            set { 
                _values = value; 
            }
        }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;


        public ProductSkuStocksService(AppStateService appstate)
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
                    Values = await appstate.GetAsync<List<SkuStockModel>>("ProductSkus/Stocks") ?? new();
                    fchClient = DateTime.Now;
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

        public SkuStockModel? GetValue(Guid? guid, Guid? mgz) => guid == null ? null : Values?.FirstOrDefault(x => x.Sku == guid && x.Mgz == mgz);

        #region SetDirty

        private List<SkuStockModel>? _values;
        private DateTime? fchServer;
        private DateTime? fchClient;
        private bool NeedsUpdate() => fchServer == null || fchClient == null || fchServer > fchClient;
        public void SetDirtyIfNeeded(List<DirtyTableModel> items)
        {
            fchServer = items.FirstOrDefault(x => x.Id == DirtyTableModel.Ids.Arc)?.Fch ?? fchServer;
        }

        #endregion

    }
}

