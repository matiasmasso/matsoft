
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
namespace Web.Services
{
    public class NoticiasService:IDisposable
    {
        public List<NoticiaModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;
        public event Action<Exception?>? OnChange;
        private AppStateService appstate;
        private LangTextsService langTextsService;
        private ProductPluginsService productPluginsService;

        public NoticiasService(AppStateService appstate, 
            LangTextsService langTextsService,
            ProductPluginsService productPluginsService)
        {
            this.appstate = appstate;
            this.langTextsService = langTextsService;
            this.productPluginsService = productPluginsService;

            langTextsService.OnChange += NotifyChange;
            productPluginsService.OnChange += NotifyChange;

            Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            State = DbState.IsLoading;
            try
            {
            Values = await appstate.GetAsync<List<NoticiaModel>>("noticias") ?? new();
                State = DbState.StandBy;
                OnChange?.Invoke(null);
            }
            catch (Exception ex)
            {
                State = DbState.Failed;
                OnChange?.Invoke(ex);
            }
        }

        public NoticiaModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);


        public List<NoticiaModel> OddNews()
        {
            var retval = new List<NoticiaModel>();
            for (var i = 0; i < Values?.Count; i++)
            {
                retval.Add(Values![i]);
                i += 1;
            }
            return retval;
        }

        public List<NoticiaModel> PairNews()
        {
            var retval = new List<NoticiaModel>();
            for (var i = 1; i < Values?.Count; i++)
            {
                retval.Add(Values[i]);
                i += 1;
            }
            return retval;
        }

        public List<NoticiaModel>? UserValues(UserModel? user, LangDTO lang, Guid? hideThis = null)
        {
            //TO DO: Customize news upon user rol
            var retval = Values?
                .Where(
                    x=>x.Visible 
                && (x.FchFrom == null || x.FchFrom >= DateTime.Now) 
                && (x.FchTo == null || x.FchTo >= DateTime.Now)
            )?
            .ToList();

            if (retval != null && hideThis != null )
            {
                var removeThis = retval.FirstOrDefault(x => x.Guid == hideThis);
                if(removeThis != null) retval.Remove(removeThis);
            }
            return retval;
        }
        void NotifyChange(Exception? ex=null)
        {
            OnChange?.Invoke(ex);
        }

        public List<Box> Boxes(LangDTO lang) => Values?.Where(x => x.IsActive()).Select(x => x.Box(lang)).ToList() ?? new();

        public NoticiaModel? FromLandingPage(string? segment, LangDTO lang)
        {
            var trimmedSegment = segment ?? string.Empty;
            if(segment?.EndsWith(".html", StringComparison.CurrentCultureIgnoreCase) ?? false)
                trimmedSegment = segment.Substring(0, segment.Length - 5);
            var urls = langTextsService.GetValuesFromContent(LangTextModel.Srcs.ContentUrl, lang, trimmedSegment)?.ToList();
            var url = urls?.FirstOrDefault();
            NoticiaModel? retval = url==null ? null : GetValue(url.Guid);
            return retval;
        }

        public string Content(Guid? guid, LangDTO lang)
        {
            string? retval = null;
            if (guid != null)
            {
                var langtextContent = langTextsService.GetValue(guid, LangTextModel.Srcs.ContentText);
                var html = langtextContent?.Tradueix(lang)?.Html();
                retval = productPluginsService.ExpandPlugins(html, lang);
            }
            return retval ?? "";
        }

        public void Dispose()
        {
            langTextsService.OnChange -= NotifyChange;
            productPluginsService.OnChange -= NotifyChange;
        }
    }
}
