using DTO;

namespace Api.Services
{
    public class LayoutService
    {

        //public static LayoutDTO Factory(UserModel? user = null, LangDTO? lang = null)
        //{
        //    var emp = (int)EmpModel.EmpIds.MatiasMasso;
        //    lang = lang == null ? LangDTO.Default() : lang;
        //    var retval = new LayoutDTO();
        //    retval.User = user;
        //    using (var db = new Entities.MaxiContext())
        //    {
        //        retval.Nav = NavsService.Custom(db, user,  emp);
        //        retval.News = NoticiasService.Boxes(db, user, lang).Take(3).ToList();
        //        retval.BlogPosts = BlogPostsService.ForLayout(db, user, lang);

        //    }
        //    retval.BottomLinks = BottomLinks();
        //    retval.AboutTitle = lang.Tradueix("Acerca de", "Qui som", "About", "Sobre nós");
        //    retval.HelpTitle = lang.Tradueix("Ayuda", "Ajuda", "Help", "Ajuda");
        //    retval.Copyright = String.Format("{0} - MATIAS MASSO, S.A. - Todos los derechos reservados", DateTime.Today.Year);
        //    return retval;
        //}

        private static List<Box> BottomLinks()
        {
            var retval = new List<Box>();
            retval.Add(Box.Factory("Aviso legal", "/AvisoLegal"));
            retval.Add(Box.Factory("Privacidad", "/Privacidad"));
            retval.Add(Box.Factory("Cookies", "/Cookies"));
            return retval;
        }


    }

}
