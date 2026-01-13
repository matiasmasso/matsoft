using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOWebMenu
    {
        public List<DTOWebMenuGroup> Groups { get; set; }
    }

    public class DTOWebMenuGroup : DTOBaseGuid
    {
        public int Ord { get; set; }
        public DTOLangText LangText { get; set; }

        //public bool Private { get; set; }
        public List<DTOWebMenuItem> Items { get; set; }

        //public string Url { get; set; }
        public DTOLangText LangUrl { get; set; }

        public enum Wellknowns
        {
            NotSet,
            Langs,
            Products,
            Queries,
            Forms,
            Perfil,
            Developer
        }

        public DTOWebMenuGroup() : base()
        {
            Items = new List<DTOWebMenuItem>();
        }

        public DTOWebMenuGroup(Guid oGuid) : base(oGuid)
        {
            Items = new List<DTOWebMenuItem>();
        }

        public static Guid Wellknown(DTOWebMenuGroup.Wellknowns value)
        {
            Guid retval = default(Guid);
            switch (value)
            {
                case DTOWebMenuGroup.Wellknowns.Langs:
                    {
                        retval = new Guid("69D3D691-8F8E-41C9-8711-3E8A189EF113");
                        break;
                    }

                case DTOWebMenuGroup.Wellknowns.Products:
                    {
                        retval = new Guid("81F161B9-4513-4388-8CB6-329E6005ECE4");
                        break;
                    }
                case DTOWebMenuGroup.Wellknowns.Perfil:
                    {
                        retval = new Guid("F10C64A3-840C-490C-9FDE-953EE19A6B51");
                        break;
                    }
            }
            return retval;
        }

        public static string Nom(DTOWebMenuGroup oWebMenuGroup, DTOLang oLang)
        {
            string retval = oWebMenuGroup.LangText.Tradueix(oLang);
            return retval;
        }

        public static DTOWebMenuGroup LangsGroup(DTOLang oLang)
        {
            DTOWebMenuGroup retval = new DTOWebMenuGroup(DTOWebMenuGroup.Wellknown(DTOWebMenuGroup.Wellknowns.Langs));
            retval.LangText = new DTOLangText("Idioma", "Llengua", "Language", "idioma");


            DTOWebMenuGroup.AddItem(retval, "Español", "Espanyol", "Spanish", "Espanhol", "/esp", oLang.Equals(DTOLang.ESP()));
            DTOWebMenuGroup.AddItem(retval, "Catalán", "Català", "Catalan", "Catalão", "/cat", oLang.Equals(DTOLang.CAT()));
            DTOWebMenuGroup.AddItem(retval, "Inglés", "Anglés", "English", "Inglês", "/eng", oLang.Equals(DTOLang.ENG()));
            DTOWebMenuGroup.AddItem(retval, "Portugués", "Portuguès", "Portuguese", "Português", "/por", oLang.Equals(DTOLang.POR()));
            return retval;
        }


        public static DTOWebMenuGroup SorteosGroup()
        {
            DTOWebMenuGroup retval = new DTOWebMenuGroup();
            retval.LangText = new DTOLangText("Sorteos", "Sortejos", "Raffles", "Sorteios");
            retval.LangUrl = new DTOLangText("/sorteos","/sortejos","/raffles","/sorteios");
            return retval;
        }

        public static DTOWebMenuGroup NoticiasGroup()
        {
            DTOWebMenuGroup retval = new DTOWebMenuGroup();
            retval.LangText = new DTOLangText("Noticias", "Notícies", "News", "Notícias");
            retval.LangUrl = new DTOLangText("/news");
            return retval;
        }

        public static DTOWebMenuGroup SignUpMenuGroup()
        {
            DTOWebMenuGroup retval = new DTOWebMenuGroup();
            retval.LangText = new DTOLangText("Registrarse", "Registrar-se", "Sign up", "Inscrever-se");
            retval.LangUrl = new DTOLangText("/registro");
            return retval;
        }

        public static DTOWebMenuGroup LoginGroup()
        {
            DTOWebMenuGroup retval = new DTOWebMenuGroup();
            retval.LangText = new DTOLangText("Acceder", "Accedir", "Log in", "Entrar");
            retval.LangUrl = new DTOLangText("/pro");
            return retval;
        }

        public static DTOWebMenuItem AddItem(DTOWebMenuGroup oParent, string sEsp, string sCat, string sEng, string sPor, string sNavigateTo = "#", bool BlSelected = false)
        {
            DTOWebMenuItem retval = new DTOWebMenuItem();
            {
                var withBlock = retval;
                withBlock.LangText = new DTOLangText(sEsp, sCat, sEng, sPor);
                withBlock.LangUrl = new DTOLangText(sNavigateTo);
                withBlock.Actiu = BlSelected;
            }
            oParent.Items.Add(retval);
            return retval;
        }
    }

    public class DTOWebMenuItem : DTOBaseGuid
    {
        public DTOLangText LangText { get; set; }
        public string Ord { get; set; }
        //public string Url { get; set; }
        public DTOLangText LangUrl { get; set; }
        public bool Actiu { get; set; }
        public DTOWebMenuGroup Group { get; set; }
        public List<DTORol> Rols { get; set; }


        public DTOWebMenuItem() : base()
        {
            Rols = new List<DTORol>();
        }

        public DTOWebMenuItem(Guid oGuid) : base(oGuid)
        {
            Rols = new List<DTORol>();
        }

        public static string Nom(DTOWebMenuItem oWebMenuItem, DTOLang oLang)
        {
            string retval = oWebMenuItem.LangText.Tradueix(oLang);
            return retval;
        }
    }
}
