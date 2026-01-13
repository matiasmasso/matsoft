using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class SkuPncsService
    {
        public static List<SkuPncModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return All(db);
            }
        }
        public static List<SkuPncModel> All(Entities.MaxiContext db)
        {
            return db.VwSkuPncs
                        .AsNoTracking()
                .Select(x => new SkuPncModel
                {
                    Sku = x.SkuGuid,
                    Clients = x.Clients ?? 0,
                    AlPot = x.ClientsAlPot ?? 0,
                    EnProgramacio = x.ClientsEnProgramacio ?? 0,
                    BlockStock = x.ClientsBlockStock ?? 0,
                    Proveidors = x.Pn1 ?? 0
                })
                .ToList();
        }
    }
}
