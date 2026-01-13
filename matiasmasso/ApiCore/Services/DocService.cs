using Microsoft.EntityFrameworkCore;
using DTO;

namespace Api.Services
{
    public class DocService
    {
        //replaces DocfileService using asin instead of hash

        public static DocfileModel? Media(string asin)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = (from x in db.DocFiles
                              where x.Hash == asin
                              select new DocfileModel()
                              {
                                  Document = new Media((Media.MimeCods)x.Mime, x.Doc)
                              }).FirstOrDefault();
                return retval;
            }
        }

        public static Byte[]? Thumbnail(string asin) 
        {
            var hash = DTO.Helpers.CryptoHelper.FromUrFriendlyBase64(asin);
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.DocFiles
                    .AsNoTracking()
                    .Where(x => x.Hash == asin)
                    .Select(x => x.Thumbnail)
                    .FirstOrDefault();
                return retval;
            }
        }

    }
}
