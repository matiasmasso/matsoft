using DTO;


namespace Shop4moms.Services
{
    public class RoutesService
    {
        public List<RouteModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action? OnChange;
        private AppStateService appstate;

        public RoutesService(AppStateService appstate)
        {
            this.appstate = appstate;
            _ = Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                Values = await appstate.GetAsync<List<RouteModel>>("Shop4moms/routes") ?? new();
                State = DbState.StandBy;
                OnChange?.Invoke();
            }
        }

        public RouteModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);
        public RouteModel? GetValue(string segment) => Values?.FirstOrDefault(x => x.Segment == segment);


        public LangTextModel? LangText(Guid? guid)
        {
            var routes = guid == null ? new() : Values?.FindAll(x => x.Guid == guid);
            return new LangTextModel
            {
                Guid = guid,
                Esp = routes?.FirstOrDefault(x => x.Lang!.IsEsp())?.Segment,
                Cat = routes?.FirstOrDefault(x => x.Lang!.IsCat())?.Segment,
                Eng = routes?.FirstOrDefault(x => x.Lang!.IsEng())?.Segment,
                Por = routes?.FirstOrDefault(x => x.Lang!.IsPor())?.Segment
            };
        }

        public List<MenuItemModel> FooterMenu()
        {
            List<MenuItemModel> retval = new();

            if(Values != null)
            {
                var about = new Guid("4D7E6B0A-B563-4387-B117-E84BA98C0597");
                var privacity = new Guid("69F4C3B5-466E-45DA-B099-0BE928A9CEB0");
                var legal = new Guid("FE8A2B73-1309-4680-B225-66D785B82EE6");
                var conditions = new Guid("A442F491-CC18-4188-ADA5-CF4AA4B7F815");
                retval.Add(new MenuItemModel
                {
                    Mode = MenuItemModel.Modes.Navigation,
                    Caption = new LangTextModel { Esp = "¿Quién somos?", Cat = "Qui som?", Eng = "About us", Por = "Quem somos" },
                    Url = LangText(about)

                });
                retval.Add(new MenuItemModel
                {
                    Mode = MenuItemModel.Modes.Navigation,
                    Caption = new LangTextModel { Esp = "Aviso legal", Cat = "Avis legal", Eng = "Legal", Por = "Aviso Legal" },
                    Url = LangText(legal)
                });
                retval.Add(new MenuItemModel
                {
                    Mode = MenuItemModel.Modes.Navigation,
                    Caption = new LangTextModel { Esp = "Privacidad", Cat = "Privacitat", Eng = "Privacy policy", Por = "Privacidade" },
                    Url = LangText(privacity)
                });
                retval.Add(new MenuItemModel
                {
                    Mode = MenuItemModel.Modes.Navigation,
                    Caption = new LangTextModel { Esp = "Condiciones", Cat = "Condicions", Eng = "Terms", Por = "Termos e Condições" },
                    Url = LangText(conditions)
                });
            }

            return retval;
        }



    }
}

