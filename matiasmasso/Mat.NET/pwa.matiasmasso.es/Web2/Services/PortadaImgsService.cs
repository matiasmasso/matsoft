using DTO;

namespace Web.Services
{
    public class PortadaImgsService
    {
         public List<PortadaImgModel>? Values;
       private AppStateService appstate;
        public DbState State { get; set; } = DbState.StandBy;
        public event Action<Exception?>? OnChange;

        public PortadaImgsService(AppStateService appstate)
        {
            this.appstate = appstate;
            Task task = Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            State = DbState.IsLoading;
            try
            {
            Values = await appstate.GetAsync<List<PortadaImgModel>>("portadaimgs") ?? new();
                State = DbState.StandBy;
                OnChange?.Invoke(null);
            }
            catch (Exception ex)
            {
                State = DbState.Failed;
                OnChange?.Invoke(ex);
            }
        }

        public List<Box> Boxes(LangDTO lang) => Values?.Select(x => x.Box(lang)).ToList() ?? new();
        public string PortadaImgNavigateTo(PortadaImgModel.Ids id) => Values?.FirstOrDefault(x => x.Id == id.ToString())?.NavigateTo ?? "";
        public string PortadaImgTitle(PortadaImgModel.Ids id) => Values?.FirstOrDefault(x => x.Id == id.ToString())?.Title ?? "";
        public string PortadaImgSrc(PortadaImgModel.Ids id) => Values?.FirstOrDefault(x => x.Id == id.ToString())?.Src() ?? "";

    }
}
