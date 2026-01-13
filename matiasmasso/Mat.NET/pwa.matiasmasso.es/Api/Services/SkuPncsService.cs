using DTO;

namespace Api.Services
{
    public class SkuPncsService
    {
        public static List<SkuPncDTO> All(Entities.MaxiContext db)
        {
            return db.VwSkuPncs
                .Select(x => new SkuPncDTO
                {
                    Sku = x.SkuGuid,
                    Clients = x.Clients ?? 0,
                    ClientsAlPot = x.ClientsAlPot ?? 0,
                    ClientsEnProgramacio = x.ClientsEnProgramacio ?? 0,
                    ClientsBlockStock = x.ClientsBlockStock ?? 0,
                    Proveidors = x.Pn1 ?? 0
                })
                .ToList();
        }
    }
}
