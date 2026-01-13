using DTO;


namespace Web.Services
{
    public class ContractsService
    {
        public List<ContractModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private ApiClientService apiclient;

        public ContractsService(ApiClientService apiclient)
        {
            this.apiclient = apiclient;
            _ = Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    Values = await apiclient.GetAsync<List<ContractModel>>("Contracts") ?? new();
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

        public List<ContractModel.CodiClass>? Codis()
        {
            return Values?
                .GroupBy(x => x.Codi)
                .Select(x => x.First().Codi)
                .ToList();
        }


        public ContractModel.CodiClass? Codi(Guid? guid)=> Values?.FirstOrDefault(x => x.Codi?.Guid == guid)?.Codi;

        public ContractModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public async Task<bool> UpdateAsync(ContractModel? value)
        {
            var retval = await apiclient.PostAsync<ContractModel, bool>(value, "Contract");
            if (retval)
            {
                if (value.IsNew) Values?.Add(value);
                value.IsNew = false;
            }
            return retval;
        }
        public async Task<bool> DeleteAsync(ContractModel? value)
        {
            var retval = await apiclient.GetAsync<bool>("Contract/delete", value.Guid.ToString());
            if (retval) Values?.Remove(value);
            return retval;

        }
    }
}

