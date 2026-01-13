using DTO;


namespace Web.Services
{
    public class StoreLocatorRawService
    {
        public List<StoreLocatorRawModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;
        private AtlasService atlasService;
        private ProductsService productsService;

        public StoreLocatorRawService(AppStateService appstate,
            AtlasService atlasService,
            ProductsService productsService)
        {
            this.appstate = appstate;
            this.atlasService = atlasService;
            this.productsService = productsService;

            atlasService.OnChange += NotifyChange;
            productsService.OnChange += NotifyChange;

            _ = Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    Values = await appstate.GetAsync<List<StoreLocatorRawModel>>("StoreLocator/Raw") ?? new();
                    State = DbState.StandBy;
                    NotifyChange(null);
                }
                catch (Exception ex)
                {
                    State = DbState.Failed;
                NotifyChange(ex);
                }
            }
        }

        //public StoreLocatorRawModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        void NotifyChange(Exception ex) { OnChange?.Invoke(ex); }

        public List<GuidNom> Areas(Guid? productGuid, Guid? country, bool forRaffle = false)
        {
            var retval = new List<GuidNom>();
            if (productGuid != null && country != null)
            {
                var areaGuids = ProductValues((Guid)productGuid, forRaffle)
                    .Where(x => x.Country == country && x.Area != null)
                    .Select(x => (Guid)x.Area!)
                    .Distinct()
                    .ToList();

                retval = areaGuids?
                    .Select(x => new GuidNom(x, atlasService.ProvinciaOrZonaNom(x)))
                    .OrderBy(x => x.Nom ?? "?")
                    .ToList();
            }
            return retval;
        }

        public List<GuidNom> Locations(Guid? productGuid, Guid? area, bool forRaffle = false)
        {
            var retval = new List<GuidNom>();
            if (productGuid != null && area != null)
            {
                var locationGuids = ProductValues((Guid)productGuid, forRaffle)
                    .Where(x => x.Area == area && x.Location != null)
                    .Select(x => (Guid)x.Location!)
                    .Distinct()
                    .ToList();

                retval = locationGuids?
                    .Select(x => new GuidNom(x, atlasService.LocationNom(x)))
                    .OrderBy(x => x.Nom ?? "?")
                    .ToList();
            }
            return retval;
        }

        public List<ContactModel>? Distributors(Guid? productGuid, Guid? location, bool forRaffle = false)
        {
            List<ContactModel>? retval = null;
            if (productGuid != null && location != null)
            {
                var distributorGuids = ProductValues((Guid)productGuid, forRaffle)
                    .Where(x => x.Location == location)
                    .OrderBy(x => x.ConsumerPriority)
                    .ThenByDescending(x => x.Val)
                    .Select(x => x.Client)
                    .Distinct()
                    .ToList();

                retval = distributorGuids?
                    .Select(x => atlasService.Contact(x))
                    .ToList();
            }
            return retval;
        }

        public ContactModel? Distributor(Guid? distributorGuid) => atlasService.Contact(distributorGuid);


        public string? ZipyCit(ContactModel? distributor) => atlasService.ZipyCit(Address(distributor)?.Zip);
        public AddressModel? Address(ContactModel? distributor) => atlasService.Address(distributor?.Guid, AddressModel.Cods.Fiscal);


        public List<StoreLocatorRawModel> ProductValues(Guid productGuid, bool forRaffle = false)
        {
            List<StoreLocatorRawModel> retval = new();
            var product = productsService.GetValue(productGuid);
            if (product?.Src == ProductModel.SourceCods.Brand)
                retval = Values?.Where(x => x.Brand == productGuid).ToList() ?? new();
            else if (product?.Src == ProductModel.SourceCods.Category)
                retval = Values?.Where(x => x.Category == productGuid).ToList() ?? new();
            else if (product?.Src == ProductModel.SourceCods.Sku)
            {
                ProductSkuModel sku = (ProductSkuModel)product;
                retval = Values?.Where(x => x.Category == sku.Category).ToList() ?? new();
            }
            if (forRaffle)
                retval = retval.Where(x => x.Raffles && !x.Obsoleto && !x.Impagat && !x.Blocked).ToList();
            return retval;
        }


        public void Dispose()
        {
            atlasService.OnChange -= NotifyChange;
            productsService.OnChange += NotifyChange;
        }

    }
}

