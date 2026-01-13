using DTO;

namespace Shop4moms.Services
{
    public class ProductCategoriesService
    {
        public List<ProductCategoryModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;
        public event Action? OnChange;

        private AppStateService appstate;

        public ProductCategoriesService(AppStateService appstate)
        {
            this.appstate = appstate;
            Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                var rawdata = await appstate.GetAsync<List<ProductCategoryModel>>("ProductCategories/1") ?? new();
                var brand = ProductBrandModel.Wellknown(ProductBrandModel.Wellknowns.fourMoms)!;
                Values = rawdata.Where(x=>x.Brand == brand.Guid).ToList();
                State = DbState.StandBy;
                OnChange?.Invoke();
            }
        }

        public List<ProductCategoryModel>? GetValues(ProductBrandModel? brand, bool onlyActive)
        {
            List<ProductCategoryModel>? retval = null;
            if (brand != null)
                if (onlyActive)
                    retval = Values?
                        .Where(x => x.Brand == brand.Guid && x.Obsoleto == false && x.HasImage).ToList() ?? new(); //TO DO: filtrar noweb
                else
                    retval = Values;
            return retval;
        }

        public List<ProductCategoryModel>? GetValues(ProductDeptModel? dept, bool onlyActive)
        {
            List<ProductCategoryModel>? retval = null;
            if (dept != null)
                if (onlyActive)
                    retval = Values?
                        .Where(x => x.Dept == dept.Guid && x.Obsoleto == false && x.HasImage).ToList() ?? new(); //TO DO: filtrar noweb
                else
                    retval = Values;
            return retval;
        }

        public ProductCategoryModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

    }
}
