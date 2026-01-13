using Web.Pages;
using DTO;
using ImageMagick;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Net.Http.Headers;
using static Web.Services.MgzInventoryService;

namespace Web.Services
{
    public class MgzInventoryService : IDisposable
    {
        public DateTime Fch { get; set; }
        public DbState State { get; set; } = DbState.StandBy;
        public event Action<Exception?>? OnChange;
        public List<MgzInventoryModel>? Values { get; set; }
        public bool IsShowingEmptySkus { get; set; }
        //public bool isDepreciating { get; set; }
        public List<Depreciation> Depreciations { get; set; } = Depreciation.Default();

        public XItem Total { get; set; }


        AppStateService appstate;
        ProductsService productsService;
        CultureService culture;

        public MgzInventoryService(CultureService culture, AppStateService appstate, ProductsService productsService)
        {
            this.culture = culture;
            this.appstate = appstate;
            this.productsService = productsService;
            productsService.OnChange += NotifyChange;
            var lastEndOfMonth = DateTime.Today.AddDays(-DateTime.Today.Day);
            Fch = DateTime.Today; // lastEndOfMonth;

            _ = Task.Run(async () => await FetchAsync(Fch)); //lastEndOfMonth)
        }

        public async Task FetchAsync(DateTime fch)
        {
            if (State != DbState.IsLoading || this.Fch != fch)
            {
                State = DbState.IsLoading;
                this.Fch = fch;
                try
                {
                    var mgz = MgzModel.Default();
                    Values = await appstate.PostAsync<DateTime, List<MgzInventoryModel>>(fch, "MgzInventory", mgz!.Guid.ToString());
                    SetTotal();
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

        public void SetTotal()
        {
            if (Values != null) Total = new XItem
            {
                Eur = Values.Sum(x => x.Stk * x.Eur),
                DeprecValue = Values.Sum(x => Depreciate(x.LastIn, x.Stk, x.Eur))
            };
        }

        public List<XItem>? Brands()
        {
            var retval = Values?
                 .Where(x => IsShowingEmptySkus || x.Stk != 0)
                 .GroupBy(x => x.Brand)
                 .OrderBy(x => productsService.Brand(x.Key)?.Ord)
                 .ThenBy(x => productsService.Brand(x.Key)?.Nom.Tradueix(culture.Lang()))
                 .Select(x => new XItem
                 {
                     Product = productsService.Brand(x.Key),
                     Eur = x.Sum(y => y.Stk * y.Eur),
                     DeprecValue = x.Sum(y => Depreciate(y.LastIn, y.Stk, y.Eur))
                 })
                 .ToList();
            return retval;
        }

        public List<XItem>? Categories(XItem brand)
        {
            return Values?
                .Where(x => x.Brand == brand.Product?.Guid && (IsShowingEmptySkus || x.Stk != 0))
                .GroupBy(x => x.Category)
                .OrderBy(x => productsService.Category(x.Key)?.Codi)
                .ThenBy(x => productsService.Category(x.Key)?.Nom.Tradueix(culture.Lang()))
                .Select(x => new XItem
                {
                    Product = productsService.Category(x.Key),
                    Qty = x.Sum(y => y.Stk),
                    Eur = x.Sum(y => y.Stk * y.Eur),
                    DeprecValue = x.Sum(y => Depreciate(y.LastIn, y.Stk, y.Eur))
                }).ToList();
        }


        public List<XItem>? Skus(XItem category)
        {
            return Values?
                .Where(x => x.Category == category.Product?.Guid && (IsShowingEmptySkus || x.Stk != 0))
                .OrderBy(x => productsService.Sku(x.Sku)?.Nom.Tradueix(culture.Lang()))
                .Select(x => new XItem(productsService.Sku(x.Sku), x.Stk, x.Stk*x.Eur, x.LastIn, this.Fch, Depreciations))
                .ToList();
        }




        private decimal Depreciate(DateTime lastIn, int qty, decimal eur)
        {
            decimal retval = 0;
            var gap = (this.Fch - lastIn).Days;
            foreach (var depreciation in Depreciations.OrderByDescending(x => x.FromDays))
            {
                if (gap >= depreciation.FromDays)
                {
                    retval = Math.Round(qty * eur * depreciation.Percent / 100, 2);
                    break;
                }
            }
            return retval;
        }


        public async Task<MgzInventoryExtracteModel> GetExtracte(ProductSkuModel sku)
        {
            var mgz = MgzModel.Default();
            return await appstate.PostAsync<DateTime, MgzInventoryExtracteModel>(Fch, "mgzInventory/extracte", mgz!.Guid.ToString(), sku.Guid.ToString());
        }

        void NotifyChange(Exception? ex)
        {
            OnChange?.Invoke(ex);
        }

        public ProductBrandModel? Brand(Guid guid) => productsService.Brand(guid);
        public ProductCategoryModel? Category(Guid guid) => productsService.Category(guid);
        public ProductSkuModel? Sku(Guid guid) => productsService.Sku(guid);

        public void Dispose()
        {
            productsService.OnChange -= NotifyChange;

        }

        public class XItem
        {
            public ProductModel? Product { get; set; }
            public int Qty { get; set; }
            public decimal? Eur { get; set; }
            public decimal? Net() => Eur - DeprecValue;
            public DateTime? LastIn { get; set; }
            public decimal? DeprecTipus { get; set; }
            public decimal? DeprecValue { get; set; }

            public XItem() { }

            public XItem(ProductModel? product, int qty, decimal? inventory, DateTime? lastIn, DateTime fch, List<Depreciation> depreciations)
            {
                Product = product;
                Qty = qty;
                Eur = inventory;
                LastIn = lastIn;
                DeprecTipus = CalcDeprecTipus(lastIn, fch, depreciations);
                DeprecValue = Math.Round((inventory * DeprecTipus ?? 100) / 100, 2); 
            }

            public int DaysTo(DateTime fch) => (fch - LastIn)?.Days ?? 0;

            private decimal CalcDeprecTipus(DateTime? lastIn, DateTime fch, List<Depreciation> depreciations)
            {
                decimal retval = 0;
                var gap = (fch - lastIn)?.Days;
                foreach (var depreciation in depreciations.OrderByDescending(x => x.FromDays))
                {
                    if (gap >= depreciation.FromDays)
                    {
                        retval = depreciation.Percent;
                        break;
                    }
                }
                return retval;
            }


        }


        public class Depreciation
        {
            public decimal Percent { get; set; }
            public int FromDays { get; set; }

            public static List<Depreciation> Default() => new List<Depreciation>()
            {
                new Depreciation{Percent = 50, FromDays=90},
                new Depreciation{Percent = 100, FromDays=180},
            };

        }

    }
}
