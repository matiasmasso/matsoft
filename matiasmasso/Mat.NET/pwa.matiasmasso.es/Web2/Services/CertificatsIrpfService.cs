using DTO;

namespace Web.Services
{
    public class CertificatsIrpfService
    {
        public List<CertificatIrpfModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;

        public CertificatsIrpfService(AppStateService appstate)
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
                    Values = await appstate.GetAsync<List<CertificatIrpfModel>>("CertificatsIrpf") ?? new();
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

        public CertificatIrpfModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public async Task<bool> UpdateAsync(CertificatIrpfModel value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.PostAsync<CertificatIrpfModel, bool>(value, "CertificatIrpf");
                if (retval)
                {
                    if (value.IsNew) Values?.Add(value);
                    value.IsNew = false;
                }
            }
            return retval;
        }


        public async Task<bool> DeleteAsync(CertificatIrpfModel? value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.GetAsync<bool>("CertificatIrpf", value.Guid.ToString());
                if (retval) Values?.Remove(value);
            }
            return retval;

        }
    }
}


