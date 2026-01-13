using DTO;


namespace Web.Services
{
    public class CorrespondenciasService
    {
        public List<CorrespondenciaModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private ApiClientService apiclient;

        public CorrespondenciasService(ApiClientService apiclient)
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
                    Values = await apiclient.GetAsync<List<CorrespondenciaModel>>("Correspondencias") ?? new();
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

        public CorrespondenciaModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public async Task<bool> UpdateAsync(CorrespondenciaModel? value)
        {
            var retval = await apiclient.PostAsync<CorrespondenciaModel, bool>(value, "Correspondencia");
            if (retval)
            {
                if (value.IsNew) Values?.Add(value);
                value.IsNew = false;
            }
            return retval;
        }
        public async Task<bool> DeleteAsync(CorrespondenciaModel? value)
        {
            var retval = await apiclient.GetAsync<bool>("Correspondencia", value.Guid.ToString());
            if (retval) Values?.Remove(value);
            return retval;

        }
    }
}

