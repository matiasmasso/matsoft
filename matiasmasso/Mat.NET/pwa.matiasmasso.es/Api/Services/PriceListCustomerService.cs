using DTO;

namespace Api.Services
{
    public class PriceListCustomerService
    {
        public static List<GuidDecimal> Current(Entities.MaxiContext db, int emp)
        {
            return db.VwRetails
                .Join(db.Arts.Where(x => x.Emp == emp), retail => retail.SkuGuid, sku => sku.Guid, (retail, sku) => new GuidDecimal
                {
                    Guid = retail.SkuGuid,
                    Value = retail.Retail ?? 0
                })
                .ToList();
        }
    }
}
