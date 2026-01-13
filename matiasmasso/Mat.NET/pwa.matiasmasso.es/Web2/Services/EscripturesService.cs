using DTO;


namespace Web.Services
{
    public class EscripturasService
    {
        public List<EscripturaModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private ApiClientService apiclient;

        public EscripturasService(ApiClientService apiclient)
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
                    Values = await apiclient.GetAsync<List<EscripturaModel>>("Escripturas") ?? new();
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

        public async Task<EscripturaModel?> Find(Guid guid)
        {
            return await apiclient.GetAsync<EscripturaModel>("Escriptura", guid.ToString());
        }

        public EscripturaModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public async Task<bool> UpdateAsync(EscripturaModel? value)
        {
            var retval = await apiclient.PostAsync<EscripturaModel, bool>(value, "Escriptura");
            if (retval)
            {
                if (value.IsNew) Values?.Add(value);
                value.IsNew = false;
            }
            return retval;
        }
        public async Task<bool> DeleteAsync(EscripturaModel? value)
        {
            var retval = await apiclient.GetAsync<bool>("Escriptura", value.Guid.ToString());
            if (retval) Values?.Remove(value);
            return retval;

        }
    }
}

