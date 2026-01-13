using System.Collections.Generic;

namespace DTO
{
    public class DTOBreadCrumb
    {
        public List<BreadCrumbItem> Items { get; set; }

        public DTOBreadCrumb(DTOLang oLang, string sHomeEsp = "Inicio", string sHomeCat = "Inici", string sHomeEng = "Home", string sHomePor = "") : base()
        {
            Items = new List<BreadCrumbItem>();
            AddItem(oLang.Tradueix(sHomeEsp, sHomeCat, sHomeEng, sHomePor), "/");
        }

        public void AddItem(string sTitle, string sUrl = "")
        {
            BreadCrumbItem oItem = new BreadCrumbItem(sTitle, sUrl);
            Items.Add(oItem);
        }

        public static DTOBreadCrumb FromIncidencia(DTOLang oLang)
        {
            DTOBreadCrumb retval = new DTOBreadCrumb(oLang, "Formularios", "Formularis", "Forms");
            retval.AddItem(oLang.Tradueix("Incidencias", "Incidències", "Incidences"), "/incidencias");
            retval.AddItem(oLang.Tradueix("Incidencia", "Incidència", "Incidence"));
            return retval;
        }

        public static DTOBreadCrumb FromCategoriaDeNoticia(DTOCategoriaDeNoticia oCategoria)
        {
            DTOBreadCrumb retval = new DTOBreadCrumb(DTOApp.Current.Lang);
            retval.AddItem("Noticias", "/News");
            retval.AddItem(oCategoria.Nom);
            return retval;
        }

        public static DTOBreadCrumb FromNoticia(DTOLang oLang)
        {
            DTOBreadCrumb retval = new DTOBreadCrumb(oLang, "Inicio", "Inici", "Home");
            retval.AddItem(oLang.Tradueix("Noticias", "Notícies", "News"), "/News");
            return retval;
        }

        public static DTOBreadCrumb FromEvento(DTOLang oLang)
        {
            DTOBreadCrumb retval = new DTOBreadCrumb(oLang, "Inicio", "Inici", "Home");
            retval.AddItem(oLang.Tradueix("Eventos", "Esdeveniments", "Events"), "/Eventos");
            return retval;
        }
    }

    public class BreadCrumbItem
    {
        public string Title { get; set; }
        public string Url { get; set; }

        public BreadCrumbItem(string sTitle, string sUrl = "") : base()
        {
            Title = sTitle;
            Url = sUrl;
        }
    }
}
