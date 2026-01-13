using DTO;


namespace Web.Services
{
    public class JornadasLaboralsService
    {
        public List<JornadaLaboralModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;

        public JornadasLaboralsService(AppStateService appstate)
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
                    Values = await appstate.GetAsync<List<JornadaLaboralModel>>("Staff/JornadasLaborals") ?? new();
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

        public JornadaLaboralModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public async Task<bool> UpdateAsync(JornadaLaboralModel value)
        {
            var retval = await appstate.PostAsync<JornadaLaboralModel, bool>(value, "JornadasLaborals");
            if (retval)
            {
                if (value.IsNew) Values?.Add(value);
                value.IsNew = false;
            }
            return retval;
        }
        public async Task<bool> DeleteAsync(JornadaLaboralModel value)
        {
            var retval = await appstate.GetAsync<bool>("JornadasLaborals/delete", value.Guid.ToString());
            if (retval) Values?.Remove(value);
            return retval;

        }
    }
}


