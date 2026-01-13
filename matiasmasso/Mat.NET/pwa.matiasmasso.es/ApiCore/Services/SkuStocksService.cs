using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class SkuStocksService
    {
        public static List<SkuStockModel> GetValues()
        {
            using(var db = new Entities.MaxiContext())
            {
                return All(db);
            }
        }
        public static List<SkuStockModel> All(Entities.MaxiContext db)
        {
            return db.VwSkuStocks
                        .AsNoTracking()
                .Where(x => x.MgzGuid != null)
                .Select(x => new SkuStockModel
                {
                    Sku = x.SkuGuid,
                    Mgz = (Guid)x.MgzGuid!,
                    Stock = x.Stock ?? 0
                })
                .ToList();
        }
    }
}
