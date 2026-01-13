using DTO;
namespace Api.Services
{
    public class RepliqsService
    {
        public static List<RepliqDTO> All(Guid rep)
        {
            using(var db = new Entities.MaxiContext())
            {
                return db.RepLiqs
                    .Where(x => x.RepGuid == rep)
                    .OrderByDescending(x => x.Fch)
                    .Select(x => new RepliqDTO
                    {
                        Guid = x.RepGuid,
                        Id = x.Id,
                        Fch = x.Fch,
                        BaseFras = x.BaseFras,
                        ComisioEur = x.ComisioEur,
                        IVAPct = x.Ivapct,
                        IrpfPct = x.Irpfpct,
                        CcaGuid = x.CcaGuid
                    }).ToList();
            }
        }
    }
}
