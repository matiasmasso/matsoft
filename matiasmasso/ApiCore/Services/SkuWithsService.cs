using DocumentFormat.OpenXml.InkML;
using DTO;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace Api.Services
{
    public class SkuWithsService
    {
        public static List<SkuWithModel> GetValues()
        {
            using(var db = new Entities.MaxiContext())
            {
                return GetValues(db);
            }
        }
        public static List<SkuWithModel> GetValues(Entities.MaxiContext db)
        {
            var retval = db.SkuWiths
                        .AsNoTracking()
                .GroupBy(x => x.Parent)
                .Select(x => new SkuWithModel
                {
                    Parent = x.Key,
                    Children = x.Select(y=>new GuidInt
                    {
                        Guid=y.Child,
                        Value=y.Qty
                    }).ToList()
                }).ToList();

            return retval;
        }

        public static List<GuidInt> GetChildren(Entities.MaxiContext db, Guid parent)
        {
            return db.SkuWiths
                        .AsNoTracking()
                .Where(x => x.Parent == parent)
                .Select(x => new GuidInt(x.Child, x.Qty))
        .ToList();

        }
    }
}
