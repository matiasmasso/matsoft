
using Shop4moms.Services;
using DTO;
using DTO.Helpers;
using DTO.Integracions.Shop4moms;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Shop4moms.Services
{
    public class NavService
    {
        private TpvService tpvService;
        private HttpClient http;
        private SessionStateService session;
        private RoutesService? routesService;

        public NavService(TpvService tpvService, 
            HttpClient http, 
            SessionStateService session,
            RoutesService routesService
            )
        {
            this.tpvService = tpvService;
            this.http = http;
            this.session = session;
            this.routesService = routesService;
        }

        public List<MenuItemModel> PublicItems(UserModel.Rols? rol)
        {
            var retval = new List<MenuItemModel>();
            retval.Add(new MenuItemModel
            {
                Caption = new LangTextModel { Esp = "Inicio", Cat = "Inici", Eng = "Home" },
                Url = new LangTextModel("/") //("/es","/ca","/en","/pt") 
            });
            retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Videos", Cat = "Videos", Eng = "Movies", Por = "Videos" }, Url = new LangTextModel("videos") });
            retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Galería", Cat = "Galería", Eng = "Gallery", Por = "Galeria" }, Url = new LangTextModel("galeria","galeria","gallery","galeria") });
            retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Cesta", Cat = "Cistella", Eng = "Basket", Por = "Cesto" }, Url = new LangTextModel("basket") });
            retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Pagar", Cat = "Pagar", Eng = "Pay" }, Url = new LangTextModel("submit") });
            retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Contacto", Cat = "Contacte", Eng = "Contact", Por = "Contato" }, Url = new LangTextModel("contacto", "contacte", "contact", "contato") });

            if (rol == UserModel.Rols.guest)
            {
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Mis pedidos", Cat = "Les meves comandes", Eng = "My purchase orders", Por = "Minhas ordens" }, Url = new LangTextModel("/es/misPedidos", "/ca/misPedidos", "/en/misPedidos", "/pt/misPedidos") });
            }
            return retval;
        }

        public List<MenuItemModel> BackOffice(UserModel.Rols? rol)
        {
            var retval = new List<MenuItemModel>();

            var clearCache = new MenuItemModel
            {
                Caption = new LangTextModel { Esp = "Limpiar cache", Cat = "Neteja cache", Eng = "Clear cache", Por = "limpar cache" },
                Mode = MenuItemModel.Modes.Action,
                Action = new Action(async () => ClearCacheAsync())
            };

            if (rol == UserModel.Rols.superUser || rol == UserModel.Rols.admin)
            {
                //retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Sesiones", Cat = "Sessions", Eng = "Sessions" }, Url = new LangTextModel("/sessions") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Nova incidencia" }, Url = new LangTextModel("/incidencia") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Baskets" }, Url = new LangTextModel("/backoffice/baskets", "/ca/backoffice/baskets", "/en/backoffice/baskets", "/pt/backoffice/baskets") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "tpvs" }, Url = new LangTextModel("/backoffice/tpvLogs", "/ca/backoffice/tpvLogs", "/en/backoffice/tpvLogs", "/pt/backoffice/tpvLogs") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Missatges" }, Url = new LangTextModel("/backoffice/msgs") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "tpv" }, Url = new LangTextModel("/tpv/config", "/ca/tpv/config", "/en/tpv/config", "/pt/tpv/config") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Redsys" }, BlankTarget = true, Url = new LangTextModel("https://sis-t.redsys.es:25443/canales/") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "strings localizer" }, Url = new LangTextModel("/es/stringLocalizer", "/ca/stringLocalizer", "/en/stringLocalizer", "/pt/stringLocalizer") });
                retval.Add(new MenuItemToggleModel(new LangTextModel("api local"), Globals.UseLocalApi, new Action(() => Globals.UseLocalApi = true), new Action(() => Globals.UseLocalApi = false)));
                retval.Add(new MenuItemToggleModel(new LangTextModel("tpv production"), tpvService.Environment == DTO.Integracions.Redsys.Common.Environments.Production, new Action(() => tpvService.Environment = DTO.Integracions.Redsys.Common.Environments.Production), new Action(() => tpvService.Environment = DTO.Integracions.Redsys.Common.Environments.Development)));
                retval.Add(new MenuItemToggleModel(new LangTextModel("showAllProducts"), session.ShowAllProducts, new Action(() => session.ShowAllProducts=true), new Action(() => session.ShowAllProducts = false)));
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Analytics" }, BlankTarget = true, Url = new LangTextModel("https://analytics.google.com") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Search Console" }, BlankTarget = true, Url = new LangTextModel("https://search.google.com/search-console") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Sitemaps" }, BlankTarget = true, Url = new LangTextModel("backoffice/sitemaps") });
                retval.Add(clearCache);
            }
            else if (rol == UserModel.Rols.marketing)
            {
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Baskets" }, Url = new LangTextModel("/es/backoffice/baskets", "/ca/backoffice/baskets", "/en/backoffice/baskets", "/pt/backoffice/baskets") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "tpvs" }, Url = new LangTextModel("/es/backoffice/tpvLogs", "/ca/backoffice/tpvLogs", "/en/backoffice/tpvLogs", "/pt/backoffice/tpvLogs") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Missatges" }, Url = new LangTextModel("/es/backoffice/msgs") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Analytics" }, BlankTarget = true, Url = new LangTextModel("https://analytics.google.com") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Search Console" }, BlankTarget = true, Url = new LangTextModel("https://search.google.com/search-console") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Sitemaps" }, BlankTarget = true, Url = new LangTextModel("backoffice/sitemaps") });
                retval.Add(new MenuItemToggleModel(new LangTextModel("showAllProducts"), session.ShowAllProducts, new Action(() => session.ShowAllProducts = true), new Action(() => session.ShowAllProducts = false)));
                retval.Add(clearCache);
            }
            else if (rol == UserModel.Rols.operadora)
            {
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Missatges" }, Url = new LangTextModel("/es/backoffice/msgs") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Baskets" }, Url = new LangTextModel("/es/backoffice/baskets", "/ca/backoffice/baskets", "/en/backoffice/baskets", "/pt/backoffice/baskets") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "tpvs" }, Url = new LangTextModel("/es/backoffice/tpvLogs", "/ca/backoffice/tpvLogs", "/en/backoffice/tpvLogs", "/pt/backoffice/tpvLogs") });
                retval.Add(new MenuItemToggleModel(new LangTextModel("showAllProducts"), session.ShowAllProducts, new Action(() => session.ShowAllProducts = true), new Action(() => session.ShowAllProducts = false)));
            }
            else if (rol == UserModel.Rols.taller || rol == UserModel.Rols.logisticManager)
            {
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Missatges" }, Url = new LangTextModel("/es/backoffice/msgs") });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Baskets" }, Url = new LangTextModel("/es/backoffice/baskets", "/ca/backoffice/baskets", "/en/backoffice/baskets", "/pt/backoffice/baskets") });
                retval.Add(new MenuItemToggleModel(new LangTextModel("showAllProducts"), session.ShowAllProducts, new Action(() => session.ShowAllProducts = true), new Action(() => session.ShowAllProducts = false)));
            }
            return retval;
        }



        async Task<bool> ClearCacheAsync()
        {
            var apiResponse = await HttpService.GetAsync<bool>(http, "shop4moms/reset");
            return true;
        }

    }
}
