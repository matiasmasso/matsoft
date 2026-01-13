using DTO;

namespace Api.Services
{
    public class PgcCtaService
    {
    }
    public class PgcCtasService
    {
        public static List<PgcCtaModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return GetValues(db);
            }
        }
        public static List<PgcCtaModel> GetValues(Entities.MaxiContext db)
        {
            return db.PgcCta
                .Select(x => new PgcCtaModel(x.Guid)
                {
                    Id = x.Id,
                    Nom = new LangTextModel { Esp = x.Esp, Cat = x.Cat, Eng = x.Eng },
                    Act = x.Act,
                    Cod = x.Cod
                })
                .ToList();
        }
    }
}
