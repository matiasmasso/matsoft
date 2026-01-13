using DTO;


namespace Web.Services
{

    public class ImmoblesService
    {
        public List<ImmobleModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private ApiClientService apiclient;
        private AppStateService appstate;

        public ImmoblesService(ApiClientService apiclient)
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
                    Values = await apiclient.GetAsync<List<ImmobleModel>>("Immobles") ?? new();
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

        public ImmobleModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public async Task<ImmobleModel?> FindAsync(Guid guid)
        {
            return await apiclient.GetAsync<ImmobleModel>("Immoble", guid.ToString()) ?? new();
        }


        public async Task<bool> UpdateAsync(ImmobleModel? value)
        {
            var retval = await apiclient.PostAsync<ImmobleModel, bool>(value, "Immoble");
            if (retval)
            {
                if (value.IsNew)
                    Values?.Add(value);
                else
                {
                    var idx = Values!.IndexOf(value);
                    Values![idx] = value;
                }
                value.IsNew = false;
            }
            return retval;
        }

        public async Task<bool> UpdateDocfileAsync(DocfileSrcModel value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.PostMultipartAsync<DocfileSrcModel, bool>(value, value.Docfile, "docfilesrc");
            }
            return retval;
        }

        public async Task<bool> DeleteAsync(ImmobleModel? value)
        {
            var retval = await apiclient.GetAsync<bool>("Immoble/delete", value.Guid.ToString());
            if (retval) Values?.Remove(value);
            return retval;

        }
    }
}

