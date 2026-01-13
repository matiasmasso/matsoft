using DTO;


namespace Web.Services
{
    public class InvoicesSentService:IDisposable
    {
        public event Action<Exception?>? OnChange;

        private AppStateService appstate;
        private AtlasService atlas;
        private ProductsService productsService;

        public InvoicesSentService(AppStateService appstate, AtlasService atlas, ProductsService productsService)
        {
            this.appstate = appstate;
            this.atlas = atlas;
            this.productsService = productsService;

            atlas.OnChange += NotifyChange;
            productsService.OnChange += NotifyChange;
        }

        public async Task<InvoiceSentModel?> FindAsync(Guid guid)
        {
            return await appstate.GetAsync<InvoiceSentModel>("InvoiceSent", guid.ToString());
        }

        public async Task<List<InvoiceSentModel>> FetchAsync(EmpModel emp, int year)
        {
            return await appstate.GetAsync<List<InvoiceSentModel>>("InvoicesSent", ((int)emp.Id).ToString(), year.ToString()) ?? new();
        }

        public ContactModel? Contact(InvoiceSentModel? value) => atlas.Contact(value?.Contact);
        public ProductSkuModel? Sku(InvoiceSentModel.Item item) => productsService.Sku(item.Sku);
        private void NotifyChange(Exception? ex)
        {
            OnChange?.Invoke(ex);
        }

        public void Dispose()
        {
            atlas.OnChange -= NotifyChange;
            productsService.OnChange -= NotifyChange;
        }

    }
}


