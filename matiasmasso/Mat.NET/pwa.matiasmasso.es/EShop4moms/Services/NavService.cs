using Components;
using DTO;
using DTO.Integracions.Shop4moms;

namespace Shop4moms.Services
{
    public class NavService
    {
        public static List<MenuItemModel> PublicItems(UserModel.Rols? rol)
        {
            var retval = new List<MenuItemModel>();
            retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Inicio", Cat = "Inici", Eng = "Home" }, Url = new LangTextModel("/") });
            retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Cesta", Cat = "Cistella", Eng = "Basket", Por="Cesto" }, Url = new LangTextModel("/basket") });
            retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Pagar", Cat = "Pagar", Eng = "Pay" }, Url = new LangTextModel("/submit") });
            retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Contacto", Cat = "Contacte", Eng = "Contact", Por="Contato" }, Url = new LangTextModel("/contact") });

            if(rol == UserModel.Rols.guest)
            {
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Mis pedidos", Cat = "Les meves comandes", Eng = "My purchase orders", Por = "Minhas ordens" }, Url = new LangTextModel("/misPedidos") });
            }
            return retval;
        }

        public static List<MenuItemModel> BackOffice(UserModel.Rols? rol)
        {
            var retval = new List<MenuItemModel>();

            if (rol == UserModel.Rols.superUser || rol == UserModel.Rols.admin)
            {
                //retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Sesiones", Cat = "Sessions", Eng = "Sessions" }, Url = new LangTextModel("/sessions") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Baskets" }, Url = new LangTextModel("/backoffice/baskets") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "tpvs" }, Url = new LangTextModel("/backoffice/tpvLogs") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "tpv" }, Url = new LangTextModel("/tpv/config") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Redsys" }, Url = new LangTextModel("https://sis-t.redsys.es:25443/canales/") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "strings localizer" }, Url = new LangTextModel("/stringLocalizer") });
                retval.Add(new MenuItemToggleModel(new LangTextModel("api local"), Globals.UseLocalApi, new Action(() => Globals.UseLocalApi = true), new Action(() => Globals.UseLocalApi = false)));
            }
            else if (rol == UserModel.Rols.operadora)
            {
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Baskets" }, Url = new LangTextModel("/backoffice/baskets") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "tpvs" }, Url = new LangTextModel("/backoffice/tpvLogs") });
            }
            return retval;
        }

        public static List<MenuItemModel> FooterMenu(Cache? cache)
        {
            List<MenuItemModel> retval = new();
            var about = new Guid("4D7E6B0A-B563-4387-B117-E84BA98C0597");
            var privacity = new Guid("69F4C3B5-466E-45DA-B099-0BE928A9CEB0");
            var legal = new Guid("FE8A2B73-1309-4680-B225-66D785B82EE6");
            var conditions = new Guid("A442F491-CC18-4188-ADA5-CF4AA4B7F815");
            retval.Add(new MenuItemModel
            {
                Mode = MenuItemModel.Modes.Navigation,
                Caption = new LangTextModel { Esp = "¿Quién somos?", Cat = "Qui som?", Eng = "About us", Por = "Quem somos" },
                Url = cache?.Routes?.LangText(about)
            });
            retval.Add(new MenuItemModel
            {
                Mode = MenuItemModel.Modes.Navigation,
                Caption = new LangTextModel { Esp = "Aviso legal", Cat = "Avis legal", Eng = "Legal", Por="Aviso Legal" },
                Url = cache?.Routes?.LangText(legal)
            });
            retval.Add(new MenuItemModel
            {
                Mode = MenuItemModel.Modes.Navigation,
                Caption = new LangTextModel { Esp = "Privacidad", Cat = "Privacitat", Eng = "Privacy policy", Por="Privacidade" },
                Url = cache?.Routes?.LangText(privacity)
            });
            retval.Add(new MenuItemModel
            {
                Mode = MenuItemModel.Modes.Navigation,
                Caption = new LangTextModel { Esp = "Condiciones", Cat = "Condicions", Eng = "Terms", Por= "Termos e Condições" },
                Url = cache?.Routes?.LangText(conditions)
            });

            return retval;
        }

    }
}
