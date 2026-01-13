using DTO;
using iText.StyledXmlParser.Jsoup.Helper;


namespace Web.Services
{
    public class IbanStructuresService:IDisposable
    {
        public List<IbanStructureModel>? Values { get; set; }

        public event Action<Exception?>? OnChange;
        public DbState State { get; set; } = DbState.StandBy;
        private AppStateService appstate;

        public IbanStructuresService(AppStateService appstate,
            BankBranchesService bankBranchesService)
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
                    Values = await appstate.GetAsync<List<IbanStructureModel>>("IbanStructures") ?? new();
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

        public IbanStructureModel? GetValue(string? ccc)
        {
            IbanStructureModel? retval = null;
            if (!string.IsNullOrEmpty(ccc) && ccc.Length>1)
            retval = Values?.FirstOrDefault(x => x.CountryISO == ccc.Substring(0, 2));
            return retval;
        }


        public async Task<bool> UpdateAsync(IbanStructureModel value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.PostAsync<IbanStructureModel, bool>(value, "IbanStructure");
                if (retval)
                {
                    if (value.IsNew) Values?.Add(value);
                    value.IsNew = false;
                }
            }
            return retval;
        }


        public async Task<bool> DeleteAsync(IbanStructureModel? value)
        {
            bool retval = false;
            if (!string.IsNullOrEmpty(value?.CountryISO))
            {
                retval = await appstate.GetAsync<bool>("IbanStructure/delete", value.CountryISO);
                if (retval) Values?.Remove(value);
            }
            return retval;

        }

        void NotifyChange(Exception? ex)
        {
            OnChange?.Invoke(ex);
        }

        public void Dispose()
        {
        }
    }
}

