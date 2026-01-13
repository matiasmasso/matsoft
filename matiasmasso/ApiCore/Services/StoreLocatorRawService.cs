using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class StoreLocatorRawService
    {
        public static List<StoreLocatorRawModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.VwStoreLocatorRaws
                        .AsNoTracking()
                    .Select(x => new StoreLocatorRawModel()
                    {
                        Brand = x.Brand,
                        Category = x.Category,
                        Country = x.Country,
                        Area = x.AreaGuid,
                        Location = x.Location,
                        Client = x.Client,
                        Raffles = x.Raffles,
                        Impagat = x.Impagat,
                        Blocked = x.Blocked,
                        Obsoleto = x.Obsoleto,
                        ConsumerPriority = x.ConsumerPriority,
                        SalePointsCount = x.SalePointsCount,
                        PremiumLine = x.PremiumLine,
                        LastFch = x.LastFch,
                        CcxVal = x.CcxVal,
                        Val = x.Val,
                        ValHistoric = x.ValHistoric,

                    }).ToList();
            }
        }
    }
}
