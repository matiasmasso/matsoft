using DocumentFormat.OpenXml.Wordprocessing;
using DTO;
namespace Web.Services
{
    public class ProductSkuPncsService
    {
        public List<SkuPncModel>? Values
        {
            get
            {
                if (NeedsUpdate()) _ = Task.Run(async () => await FetchAsync());
                return _values;
            }
            set
            {
                _values = value;
            }
        }


        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;


        public ProductSkuPncsService(AppStateService appstate)
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
                    Values = await appstate.GetAsync<List<SkuPncModel>>("ProductSkus/Pncs") ?? new();
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

        public SkuPncModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Sku == guid);

        #region SetDirty

        private List<SkuPncModel>? _values;
        private DateTime? fchServer;
        private DateTime? fchClient;
        private bool NeedsUpdate() => fchServer == null || fchClient == null || fchServer > fchClient;
        public void SetDirtyIfNeeded(List<DirtyTableModel> items)
        {
            fchServer = items.FirstOrDefault(x => x.Id == DirtyTableModel.Ids.Pnc)?.Fch ?? fchServer;
        }

        #endregion


    }
}

