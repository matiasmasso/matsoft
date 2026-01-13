using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.EMMA;
using DTO;
using DTO.Helpers;

namespace Web.Services
{
    public class NavService:IDisposable
    {
        //public NavDTO? Value { get; set; }
        public List<NavDTO.MenuItem>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;
        private LangTextsService langTextsService;

        public NavService(AppStateService appstate, LangTextsService langTextsService)
        {
            this.appstate = appstate;
            this.langTextsService = langTextsService;

            langTextsService.OnChange += NotifyChange; 

            _ = Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    Values = await appstate.GetAsync<List<NavDTO.MenuItem>>("Navs") ?? new();
                    State = DbState.StandBy;
                    NotifyChange();
                }
                catch (Exception ex)
                {
                    State = DbState.Failed;
                    NotifyChange(ex);
                }
            }
        }

        public async Task UpdateAsync(NavDTO.MenuItem value)
        {
            try
            {
                await appstate.PostAsync<NavDTO.MenuItem, bool>(value, "nav");
                if (value.IsNew)
                {
                    Values?.Add(value);
                    value.IsNew = false;
                    Values = Values!.OrderBy(x => x.Nom?.Tradueix(LangDTO.Esp())).ToList();
                }
                    NotifyChange(null);
            }
            catch (Exception ex)
            {
                NotifyChange(ex);
            }

        }

        public async Task DeleteAsync(NavDTO.MenuItem value)
        {
            try
            {
                if(await appstate.GetAsync<bool>("nav/delete", value.Guid.ToString()))
                {
                    Values!.Remove(value);
                    NotifyChange(null);
                }
            }
            catch (Exception ex)
            {
                NotifyChange(ex);
            }
        }



        public bool HasLoadedLangTexts() => langTextsService.State != DbState.IsLoading;

        public List<NavDTO.MenuItem>? ParentCandidates(NavDTO.MenuItem? child)
        {
            List<NavDTO.MenuItem>? retval = Values ?? new();
            if (child != null)
            {
                retval = Values?.Where(x => !FlatDescendants(child).Any(y => x.Guid == y.Guid)).ToList();
            }
            return retval;
        }

        public List<NavDTO.MenuItem> FlatDescendants(NavDTO.MenuItem? parent)
        {
            var retval = new List<NavDTO.MenuItem>();
            if (parent != null) retval.Add(parent);

            var children = Values?.Where(x => x.Parent == parent?.Guid)?.ToList();
            foreach (var child in children ?? new())
            {
                retval.AddRange(FlatDescendants(child));
            }
            return retval;

        }
        public NavDTO.MenuItem? Parent(NavDTO.MenuItem? child) => child == null ? null : Values?.FirstOrDefault(x => x.Guid == child.Parent);

        public LangTextModel? MenuItemNom(NavDTO.MenuItem? item) => item == null ? null : langTextsService.GetValue(item.Guid, LangTextModel.Srcs.MenuItem);

        void NotifyChange(Exception? ex = null)
        {

            OnChange?.Invoke(ex);
        }

        public void Dispose()
        {
            langTextsService.OnChange -= NotifyChange;
        }
    }
}
