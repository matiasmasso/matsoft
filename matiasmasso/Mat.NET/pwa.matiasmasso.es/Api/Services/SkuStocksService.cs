using DTO;
namespace Api.Services
{
    public class SkuStocksService
    {
        public static List<SkuStockDTO> All(Entities.MaxiContext db)
        {
            return db.VwSkuStocks
                .Where(x => x.MgzGuid != null)
                .Select(x => new SkuStockDTO
                {
                    Sku = x.SkuGuid,
                    Mgz = (Guid)x.MgzGuid!,
                    Stock = x.Stock ?? 0
                })
                .ToList();
        }
    }
}
