using DTO;

namespace Api.Services
{
    public class JornadaLaboralService
    {
    }
    public class JornadasLaboralsService
    {
        public static List<JornadaLaboralModel> All(Entities.MaxiContext db, UserModel user)
        {
            return db.JornadaLaborals
                .Where(x => x.Staff == user.DefaultContact)
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
