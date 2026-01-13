using DTO;
using System.Net.Http.Headers;

namespace Web.Services
{
    public class PurchaseOrdersService : IDisposable
    {
        public List<PurchaseOrderModel>? Values { get; set; }

        public event Action<Exception?>? OnChange;
        public DbState State { get; set; } = DbState.StandBy;
        private AppStateService appstate;
        private AtlasService atlasService;
        private ProductsService productsService;
        private ExplicitDiscountsService explicitDiscountsService;

        public PurchaseOrdersService(AppStateService appstate,
            AtlasService atlasService,
            ProductsService productsService,
            ExplicitDiscountsService explicitDiscountsService)
        {
            this.appstate = appstate;
            this.atlasService = atlasService;
            this.productsService = productsService;
            this.explicitDiscountsService = explicitDiscountsService;

            atlasService.OnChange += NotifyChange;
            productsService.OnChange += NotifyChange;
            explicitDiscountsService.OnChange += NotifyChange;

        }

        #region Api


        public async Task FetchAsync(EmpModel emp, int year)
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    var empId = ((int)emp.Id).ToString();
                    Values = await appstate.GetAsync<List<PurchaseOrderModel>>("PurchaseOrders", empId, year.ToString()) ?? new();
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

        public PurchaseOrderModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);
        public ContactModel? Contact(PurchaseOrderModel? purchaseOrder) => atlasService.Contact(purchaseOrder?.Contact?.Guid);
        public async Task<bool> UpdateAsync(PurchaseOrderModel value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.PostAsync<PurchaseOrderModel, bool>(value, "PurchaseOrder");
                if (retval)
                {
                    if (value.IsNew) Values?.Add(value);
                    value.IsNew = false;
                }
            }
            return retval;
        }

        public async Task<List<PurchaseOrderModel>> UpdateAsync(List<PurchaseOrderModel> values)
        {
            List<PurchaseOrderModel> retval = new();
            if (values != null && values.Count > 0)
            {
                retval = await appstate.PostAsync<List<PurchaseOrderModel>, List<PurchaseOrderModel>>(values, "PurchaseOrders");
            }
            return retval;
        }


        public async Task<bool> DeleteAsync(PurchaseOrderModel? value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.GetAsync<bool>("PurchaseOrder/delete", value.Guid.ToString());
                if (retval) Values?.Remove(value);
            }
            return retval;
        }

        #endregion

        public async Task<PurchaseOrderModel.Resources?> GetResourcesAsync(UserModel? user)
        {
            PurchaseOrderModel.Resources? retval = null;
            if (user != null)
            {
                retval = await appstate.GetAsync<PurchaseOrderModel.Resources>("PurchaseOrder/resources", user.Guid.ToString());
            }
            return retval;
        }


        public async Task<PurchaseOrderModel.Resources?> GetResourcesAsync(ContactModel contact)
        {
            PurchaseOrderModel.Resources? retval = null;
            retval = await appstate.GetAsync<PurchaseOrderModel.Resources>("PurchaseOrder/resources/fromContact", contact.Guid.ToString());
            return retval;
        }

        public List<ProductBrandModel> AvailableBrands(PurchaseOrderModel.Resources? resources)
        {
            return productsService.Brands(true)?
                .Where(x => AvailableCategories(resources, x).Any())
                .ToList() ?? new();
        }

        public List<ProductCategoryModel> AvailableCategories(PurchaseOrderModel.Resources? resources, ProductBrandModel? brand)
        {
            return productsService.Categories(brand, true)?
                .Where(x => AvailableSkus(resources, x).Any())
                .ToList() ?? new();
        }

        public List<ProductSkuModel> AvailableSkus(PurchaseOrderModel.Resources? resources, ProductCategoryModel? category)
        {
            return productsService.Skus(category, true) ?? new();
        }

        public List<ProductSkuModel>? AvailableSkus(PurchaseOrderModel.Resources? resources)
        {
            var brands = AvailableBrands(resources);
            var categories = productsService.Categories() ?? new();
            var retval = productsService.Skus()?.Where(x => categories.Any(y => x.Category == y.Guid)).ToList();
            return retval;
        }

        public ProductBrandModel? Brand(ProductSkuModel? sku)
        {
            var category = Category(sku);
            return productsService.Brand(category?.Brand);
        }
        public ProductCategoryModel? Category(ProductSkuModel? sku) => productsService.Category(sku?.Category);


        public ProductSkuModel? Sku(PurchaseOrderModel.Item? item) => productsService.Sku(item?.Sku);
        public int Moq(ProductSkuModel? sku) => productsService.Moq(sku);

        public Decimal? Price(ProductSkuModel? sku, PurchaseOrderModel.Resources? resources)
        {
            Decimal? retval = null;
            if (sku != null)
            {
                //check if any custom price has been negotiated for this sku
                var customCost = resources?.CustomPricelist?.FirstOrDefault(x => x.Guid == sku.Guid);
                if (customCost != null)
                    retval = customCost.Value;
                else
                {
                    //set retail price as default price
                    var rrpp = productsService.RetailPrice(sku.Guid);
                    retval = rrpp;

                    var dtoOnRrpp = GetDiscountOnRrrpp(sku, resources);
                    if (rrpp != null && dtoOnRrpp != null)
                        retval = Math.Round((decimal)(rrpp * (100 - dtoOnRrpp) / 100), 2, MidpointRounding.AwayFromZero);
                }
            }
            return retval;
        }
        public Decimal? Dto(ProductSkuModel sku, PurchaseOrderModel.Resources? resources)
        {
            var ccxOrMe = atlasService.CcxOrMe(resources?.SelectedDestination);
            decimal? retval = ExplicitDiscount(sku, ccxOrMe?.Guid)?.Dto;
            return retval;
        }

        public ExplicitDiscountModel? ExplicitDiscount(ProductSkuModel sku, Guid? customer)
        {
            ExplicitDiscountModel? retval = explicitDiscountsService.GetValue(customer, sku.Guid);
            if (retval == null)
            {
                retval = explicitDiscountsService.GetValue(customer, sku.Category);
                if (retval == null)
                {
                    var category = productsService.Category(sku.Category);
                    retval = explicitDiscountsService.GetValue(customer, category?.Brand);
                }
            }
            return retval;
        }

        private Decimal? GetDiscountOnRrrpp(ProductSkuModel sku, PurchaseOrderModel.Resources? resources)
        {
            //check if any specific discount for this sku
            var value = resources?.CustomerDtosOnRrpp?.FirstOrDefault(x => x.Product == sku.Guid);
            if (value == null)
            {
                //check if any specific discount for this sku's category
                value = resources?.CustomerDtosOnRrpp?.FirstOrDefault(x => x.Product == sku.Category);
                if (value == null)
                {
                    //check if any specific discount for this category's brand
                    var category = productsService.Category(sku.Category);
                    value = resources?.CustomerDtosOnRrpp?.FirstOrDefault(x => x.Product == category?.Brand);
                    if (value == null)
                    {
                        //check if any specific discount for this customer's channel
                        var channel = atlasService.Channel(resources?.SelectedDestination);
                        value = productsService.DiscountOnChannel(channel, sku);
                    }
                }
            }
            decimal retval = value?.Dto ?? 0;
            return retval;
        }

        void NotifyChange(Exception? ex = null)
        {
            OnChange?.Invoke(ex);
        }

        public void Dispose()
        {
            atlasService.OnChange -= NotifyChange;
            productsService.OnChange -= NotifyChange;
            explicitDiscountsService.OnChange -= NotifyChange;
        }
    }
}

