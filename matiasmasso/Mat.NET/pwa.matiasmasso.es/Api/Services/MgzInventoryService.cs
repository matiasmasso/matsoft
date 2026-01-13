using DTO;

namespace Api.Services
{
    public class MgzInventoryService
    {
        public static MgzInventoryDTO Factory(Guid mgz)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new MgzInventoryDTO();
                retval.Movements = db.VwMgzInventoryIos.
                    Where(x => x.MgzGuid == mgz).
                    Select(y => new MgzInventoryDTO.DailyIO
                    {
                        Brand = y.Brand,
                        Category = (Guid)y.Category!,
                        Sku = y.ArtGuid,
                        Fch = y.Fch,
                        IO = y.Io ?? 0
                    }).ToList();

                retval.Costs = db.VwMgzInventoryCosts.
                    Where(x => x.MgzGuid == mgz).
                    Select(y => new MgzInventoryDTO.Cost
                    {
                        Sku = y.ArtGuid,
                        Fch = y.Fch,
                        AlbGuid = y.AlbGuid,
                        AlbId = y.Alb,
                        Qty = y.Qty,
                        Eur = y.Eur ?? 0,
                        Dto = (decimal?)y.Dto ?? 0
                    }).ToList();

                return retval;
            }
        }
    }
}
