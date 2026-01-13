using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class PriceListCustomerService
    {
        public static List<GuidDecimal> Current(EmpModel.EmpIds emp)
        {
            using(var db = new Entities.MaxiContext())
            {
                return Current(db, emp);
            }
        }
        public static List<GuidDecimal> Current(Entities.MaxiContext db, EmpModel.EmpIds emp)
        {
            return db.VwRetails
                         .AsNoTracking()
               .Join(db.Arts.Where(x => x.Emp == (int)emp), retail => retail.SkuGuid, sku => sku.Guid, (retail, sku) => new GuidDecimal
               {
                   Guid = retail.SkuGuid,
                   Value = retail.Retail ?? 0
               })
                .ToList();
        }
        public static List<GuidDecimal> CurrentFromCustomer(Entities.MaxiContext db, Guid customer)
        {
            var fch = DateOnly.FromDateTime(DateTime.Now);
            return db.PriceListItemCustomers
                .Include(x=>x.PriceListNavigation)
                         .AsNoTracking()
               .Where(x=>x.PriceListNavigation.Customer == customer
               && x.PriceListNavigation.Fch <= fch
               && (x.PriceListNavigation.FchEnd == null || x.PriceListNavigation.FchEnd <= fch)
               ).Select(x=> new GuidDecimal
               {
                   Guid = x.Art,
                   Value = x.Retail ?? 0
               })
                .ToList();
        }
    }
}
