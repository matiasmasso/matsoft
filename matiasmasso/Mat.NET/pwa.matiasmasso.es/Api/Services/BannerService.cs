using DTO;

namespace Api.Services
{
    public class BannerService
    {
        public static Byte[]? Image(Guid guid)
        {
            Byte[]? retval;
            using (var db = new Entities.MaxiContext())
            {
                retval = db.Banners.Where(x => x.Guid.Equals(guid)).FirstOrDefault()?.Image;
            }
            return retval;
        }

    }


    public class BannersService
    {
        public static List<Box> Active(Entities.MaxiContext db, LangDTO lang)
        {
            var retval = new List<Box>();
            try
            {

                retval = (from banner in db.Banners
                          where (banner.Lang == null || banner.Lang == lang.ToString())
                          && (banner.FchFrom <= DateTime.Now)
                          && (banner.FchTo == null || banner.FchTo >= DateTime.Now)
                          orderby banner.FchFrom descending
                          select new Box(banner.Guid)
                          {
                              Caption = banner.Nom,
                              Url = banner.NavigateTo ?? "",
                              ImageUrl = Models.AppState.ApiUrl(BannerDTO.ImageUrlSegment, banner.Guid.ToString())
                          }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error a BannersService.Active " + ex.Message);
            }
            return retval;
        }
    }
}
