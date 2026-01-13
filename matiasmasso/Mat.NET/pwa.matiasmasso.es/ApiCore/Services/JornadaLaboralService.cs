using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class JornadaLaboralService
    {
    }
    public class JornadasLaboralsService
    {

        public static List<JornadaLaboralModel> All()
        {
            using(var db = new Entities.MaxiContext())
            {
                return All(db);
            }
        }

        public static List<JornadaLaboralModel> All(Entities.MaxiContext db)
        {
            return db.JornadaLaborals
                        .AsNoTracking()
                .OrderByDescending(x => x.FchFrom)
                .Select(x => new JornadaLaboralModel
                {
                    Guid = x.Guid,
                    FchFrom = x.FchFrom,
                    FchTo = x.FchTo,
                    Staff = x.Staff
                }).ToList();
        }

        public static List<JornadaLaboralModel> All(Entities.MaxiContext db, BaseGuid value)
        {
            var staff = value is UserModel ? ((UserModel)value)?.DefaultContact : value.Guid;
            return db.JornadaLaborals
                        .AsNoTracking()
                .Where(x => x.Staff == staff)
                .OrderByDescending(x => x.FchFrom)
                .Select(x => new JornadaLaboralModel
                {
                    Guid = x.Guid,
                    FchFrom = x.FchFrom,
                    FchTo = x.FchTo
                }).ToList();
        }
    }
}
