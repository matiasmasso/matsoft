using DTO;


namespace Web.Services
{
    public class CredencialsService
    {
        public List<CredencialModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private ApiClientService apiclient;

        public CredencialsService(ApiClientService apiclient)
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
                    Values = await apiclient.GetAsync<List<CredencialModel>>("Credencials") ?? new();
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

        public CredencialModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public async Task<bool> UpdateAsync(CredencialModel? value)
        {
            var retval = await apiclient.PostAsync<CredencialModel, bool>(value, "Credencial");
            if (retval)
            {
                if (value.IsNew) Values?.Add(value);
                value.IsNew = false;
            }
            return retval;
        }
        public async Task<bool> DeleteAsync(CredencialModel? value)
        {
            var retval = await apiclient.GetAsync<bool>("Credencial", value.Guid.ToString());
            if (retval) Values?.Remove(value);
            return retval;

        }
    }
}

