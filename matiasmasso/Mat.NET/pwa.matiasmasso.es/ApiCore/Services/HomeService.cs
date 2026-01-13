using DTO;

namespace Api.Services
{
    public class HomeService
    {
        public static HomeDTO Factory(UserModel? user, LangDTO? lang)
        {
            var retval = new HomeDTO();
            lang = lang ?? LangDTO.Default();
            using (var db = new Entities.MaxiContext())
            {
                retval.RotatingBanners = BannersService.Active(db, lang);
                retval.News = NoticiasService.Boxes(db, user, lang).Take(3).ToList();
                retval.LastPost = retval.News.First();
                retval.PortadaImgs = PortadaImgsService.All(db, lang);
                retval.CurrentOrNextRaffle = RaffleService.CurrentOrNext(db, lang);
            }
            return retval;
        }


    }
}
