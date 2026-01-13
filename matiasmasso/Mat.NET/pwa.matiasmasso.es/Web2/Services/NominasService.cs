using DTO;


namespace Web.Services
{
    public class NominasService:IDisposable
    {
        public List<NominaModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;
        private StaffsService staffsService;

        public NominasService(AppStateService appstate, StaffsService staffsService)
        {
            this.appstate = appstate;
            this.staffsService = staffsService;
            staffsService.OnChange += NotifyChange;
            _ = Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    Values = await appstate.GetAsync<List<NominaModel>>("Nominas") ?? new();
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

        public NominaModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public StaffModel? Staff(NominaModel? nomina) => staffsService.GetValue(nomina?.Staff);
        public async Task<bool> UpdateAsync(List<NominaModel> nominas)
        {
            var docfiles = nominas.Select(x=>x.Cca!.Docfile!).ToList();
            await appstate.PostMultipartAsync<List<NominaModel>, bool>(nominas, docfiles, "nominas");
            Values?.AddRange(nominas.Where(x=>x.IsNew));
            return true;
        }


        public async Task<bool> DeleteAsync(NominaModel? value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.GetAsync<bool>("Nomina/delete", value.Guid.ToString());
                Values?.Remove(value);
            }
            return retval;

        }

        public async Task<bool> DeleteAsync(List<NominaModel>? values)
        {
            bool retval = false;
            if (values?.Any() ?? false)
            {
                var guids = values.Select(x=>x.Guid).ToList();
                retval = await appstate.PostAsync<List<Guid>,bool>(guids, "Nominas/delete");
                Values?.RemoveAll(x=>values.Any(y=>y.Guid==x.Guid));
            }
            return retval;
        }

        void NotifyChange(Exception? ex)
        {
            OnChange?.Invoke(ex);
        }

        public void Dispose()
        {
            staffsService.OnChange -= NotifyChange;
        }
    }
}

