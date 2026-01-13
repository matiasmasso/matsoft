using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class BannerService
    {
        public static Byte[]? Image(Guid guid)
        {
            Byte[]? retval;
            using (var db = new Entities.MaxiContext())
            {
                retval = db.Banners
                    .AsNoTracking()
                    .Where(x => x.Guid.Equals(guid)).FirstOrDefault()?.Image;
            }
            return retval;
        }

    }


    public class BannersService
    {
        public static List<BannerModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Banners
                    .AsNoTracking()
                    .Select(x => new BannerModel(x.Guid)
                    {
                        Nom = x.Nom,
                        FchFrom = x.FchFrom,
                        FchTo = x.FchTo,
                        NavigateTo = x.NavigateTo,
                        Product = x.Product,
                        Lang = x.Lang == null ? null : new LangDTO(x.Lang),
                        FchCreated = x.FchCreated
                    }).ToList();
            }
        }

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
                              ImageUrl = Shared.AppState.ApiUrl(BannerDTO.ImageUrlSegment, banner.Guid.ToString())
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
