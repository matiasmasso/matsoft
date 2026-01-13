using DTO;


namespace Shop4moms.Services
{
    public class TemplatesService
    {
        public List<TemplateModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action? OnChange;
        private AppStateService appstate;

        public TemplatesService(AppStateService appstate)
        {
            this.appstate = appstate;
            _ = Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                Values = await appstate.GetAsync<List<TemplateModel>>("Templates") ?? new();
                State = DbState.StandBy;
                OnChange?.Invoke();
            }
        }

        public TemplateModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

    }
}

