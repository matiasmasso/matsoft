using DocumentFormat.OpenXml.Drawing.Charts;
using DTO;


namespace Web.Services
{
    public class PgcClassesService:IDisposable
    {
        public List<PgcClassModel>? Values { get; set; }

        public event Action<Exception?>? OnChange;
        public DbState State { get; set; } = DbState.StandBy;
        private AppStateService appstate;
        private LangTextsService langTextsService;

        public PgcClassesService(AppStateService appstate
            , LangTextsService langTextsService)
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
                    var plan = PgcPlanModel.Wellknown(PgcPlanModel.Wellknowns.Plan2007);
                    Values = await appstate.GetAsync<List<PgcClassModel>>("PgcClasss", plan!.Guid.ToString()) ?? new();
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

        public PgcClassModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public LangTextModel? Nom(PgcClassModel value) => langTextsService.GetValue(value.Guid, LangTextModel.Srcs.PgcClass);

        public List<PgcClassModel> Tree()
        {
            List<PgcClassModel> retval = new();
            Values?.ForEach(x => x.Children = new());
            foreach (var item in Values ?? new())
            {
                if (item.Parent == null)
                    retval.Add(item);
                else
                {
                    var parent = Values?.FirstOrDefault(x => x.Guid == item.Parent);
                    if (parent != null)
                    {
                        parent.Children.Add(item);
                    }
                }
            }
            return retval;
        }

        //public BalanceModel Balance(BalanceModel.Tabs tab, List<PgcCtaSdoModel> saldos)
        //{
        //    var retval = new BalanceModel();
        //    var epigrafs = Epigrafs(tab);
        //    foreach (var epigrafe in epigrafs ?? new())
        //    {
        //        var item = new BalanceModel.Item
        //        {

        //        };
        //        if (epigrafe.Parent == null || epigrafe.Parent)
        //            retval.Items.Add(item);
        //        else
        //        {
        //            item.Parent = retval.Items.Where(x => x.Tag is PgcClassModel && ((PgcClassModel)x.Tag).Guid == epigrafe.Parent).FirstrDefault();

        //        }


        //    }
        //    return retval;
        //}
        public  List<PgcClassModel>? Epigrafs(BalanceModel.Tabs tab)
        {
            List<PgcClassModel>? retval = null;
            switch (tab)
            {
                case BalanceModel.Tabs.Assets:
                    retval = Values?.Where(x => !string.IsNullOrEmpty(x.Cod?.ToString()) && x.Cod.ToString()!.StartsWith("aA")).ToList();
                    break;
                case BalanceModel.Tabs.Liabilities:
                    retval = Values?.Where(x => !string.IsNullOrEmpty(x.Cod?.ToString()) && x.Cod.ToString()!.StartsWith("aB")).ToList();
                    break;
                case BalanceModel.Tabs.PL:
                    retval = Values?.Where(x => !string.IsNullOrEmpty(x.Cod?.ToString()) && x.Cod.ToString()!.StartsWith("B")).ToList();
                    break;
            }
            return retval;
        }

        public async Task<bool> UpdateAsync(PgcClassModel value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.PostAsync<PgcClassModel, bool>(value, "PgcClass");
                if (retval)
                {
                    if (value.IsNew) Values?.Add(value);
                    value.IsNew = false;
                }
            }
            return retval;
        }


        public async Task<bool> DeleteAsync(PgcClassModel? value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.GetAsync<bool>("PgcClass/delete", value.Guid.ToString());
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
            langTextsService.OnChange -= NotifyChange;
        }
    }
}

