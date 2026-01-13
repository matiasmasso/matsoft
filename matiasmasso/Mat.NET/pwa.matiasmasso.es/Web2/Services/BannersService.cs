using DTO;

namespace Web.Services
{
    public class BannersService
    {
        private AppStateService appstate;
        public List<BannerModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;
        public event Action<Exception?>? OnChange;

        public BannersService(AppStateService appstate)
        {
            this.appstate = appstate;
            Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            State = DbState.IsLoading;
            try
            {
            Values = await appstate.GetAsync<List<BannerModel>>("banners") ?? new();
                State = DbState.StandBy;
                OnChange?.Invoke(null);
            }
            catch (Exception ex)
            {
                State = DbState.Failed;
                OnChange?.Invoke(ex);
            }
        }

        public List<Box> Boxes(LangDTO lang)
        {
            var retval = Values?
                .Where(x=>x.IsActive(lang))
                .Select(x => x.Box(lang))
                .ToList() ?? new();
            return retval;
        }
    }
}
