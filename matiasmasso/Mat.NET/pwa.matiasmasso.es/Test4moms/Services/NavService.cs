using DTO;
using DTO.Integracions.Shop4moms;
using DTO.Integracions.Shop4moms.Cache;
using DTO.Integracions.Shop4moms.Cache.Shop4moms;

namespace Test4moms.Services
{
    public class NavService
    {
        public static List<MenuItemModel> Factory(UserModel.Rols? rol)
        {
            var retval = new List<MenuItemModel>();
            retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Inicio", Cat = "Inici", Eng = "Home" }, Action = "/" });
            retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Cesta", Cat = "Cistella", Eng = "Basket" }, Action = "/basket" });
            retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Pagar", Cat = "Pagar", Eng = "Pay" }, Action = "/submit" });

            if (rol == UserModel.Rols.superUser)
            {
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "Sesiones", Cat = "Sessions", Eng = "Sessions" }, Action = "/sessions" });
                retval.Add(new MenuItemModel { Caption = new LangTextModel { Esp = "tpv/test" }, Action = "/tpv/test" });
            }
            return retval;
        }

        public static List<MenuItemModel> FooterMenu(Cache? cache)
        {
            var retval = new List<MenuItemModel>();
            retval.Add(new MenuItemModel
            {
                Mode = MenuItemModel.Modes.Navigation
                ,
                Caption = new LangTextModel { Esp = "¿Quién somos?", Cat = "Qui som?", Eng = "About us" }
                ,
                Url = cache?.ContentUrl(Cache.Contents.About)
            });

            retval.Add(new MenuItemModel
            {
                Mode = MenuItemModel.Modes.Navigation
                ,
                Caption = new LangTextModel { Esp = "Privacidad", Cat = "Privacitat", Eng = "Privacy policy" }
                ,
                Url = cache?.ContentUrl(Cache.Contents.Privacity)
            }); ;

            retval.Add(new MenuItemModel
            {
                Mode = MenuItemModel.Modes.Navigation
                ,
                Caption = new LangTextModel { Esp = "Aviso legal", Cat = "Avis legal", Eng = "Legal" }
                ,
                Url = cache?.ContentUrl(Cache.Contents.Legal)
            });

            retval.Add(new MenuItemModel
            {
                Mode = MenuItemModel.Modes.Navigation
                ,
                Caption = new LangTextModel { Esp = "Condiciones", Cat = "Condicions", Eng = "Terms" }
                ,
                Url = cache?.ContentUrl(Cache.Contents.Conditions)
            });

            retval.Add(new MenuItemModel
            {
                Mode = MenuItemModel.Modes.Navigation
                ,
                Caption = new LangTextModel { Esp = "Contacto", Cat = "Contacte", Eng = "Contact" }
                ,
                Url = new LangTextModel("contacto", "Contacte", "Contact")
            });

            return retval;

        }

    }
}
