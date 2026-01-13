using DTO;


namespace Web.Services
{
    public class PgcCtasService
    {
        public List<PgcCtaModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;

        public PgcCtasService(AppStateService appstate)
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
                    Values = await appstate.GetAsync<List<PgcCtaModel>>("PgcCtas") ?? new();
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

        public List<PgcCtaModel>? CurrentPlanValues() => Values?.Where(x => x.Plan == PgcPlanModel.Wellknown(PgcPlanModel.Wellknowns.Plan2007)!.Guid).ToList();

        public PgcCtaModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);
        public PgcCtaModel? GetValue(PgcCtaModel.Cods cod) =>  Values?.FirstOrDefault(x => x.Cod == cod);

        public string? IdNom(Guid guid, LangDTO lang)
        {
            var value = GetValue(guid);
            var retval = $"{value?.Id} {value?.Nom?.Tradueix(lang)}";
            return retval;
        }
        public async Task<bool> UpdateAsync(PgcCtaModel value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.PostAsync<PgcCtaModel, bool>(value, "PgcCta");
                if (retval)
                {
                    if (value.IsNew) Values?.Add(value);
                    value.IsNew = false;
                }
            }
            return retval;
        }


        public async Task<bool> DeleteAsync(PgcCtaModel? value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.GetAsync<bool>("PgcCta/delete", value.Guid.ToString());
                if (retval) Values?.Remove(value);
            }
            return retval;

        }
    }
}

