using DTO;


namespace Web.Services
{
    public class ExplicitDiscountsService
    {
        public List<ExplicitDiscountModel>? Values { get; set; }

        public event Action<Exception?>? OnChange;
        public DbState State { get; set; } = DbState.StandBy;
        private AppStateService appstate;

        public ExplicitDiscountsService(AppStateService appstate)
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
                    Values = await appstate.GetAsync<List<ExplicitDiscountModel>>("ExplicitDiscounts") ?? new();
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

        public ExplicitDiscountModel? GetValue(Guid? customer, Guid? product) => (customer == null || product == null) ? null : Values?.FirstOrDefault(x => x.Customer == customer && x.Product == product);

        public async Task<bool> UpdateAsync(ExplicitDiscountModel value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.PostAsync<ExplicitDiscountModel, bool>(value, "ExplicitDiscount");
                if (retval)
                {
                    if (!(Values?.Any(x => x.Customer == value.Customer && x.Product == value.Product) ?? false)) Values?.Add(value);
                }
            }
            return retval;
        }


        public async Task<bool> DeleteAsync(ExplicitDiscountModel? value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.GetAsync<bool>("ExplicitDiscount/delete", value.Customer.ToString(), value.Product.ToString());
                if (retval) Values?.Remove(value);
            }
            return retval;
        }
    }
}


