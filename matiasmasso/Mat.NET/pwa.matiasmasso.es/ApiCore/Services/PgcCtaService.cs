using DTO;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Api.Services
{
    public class PgcCtaService
    {
        public static PgcCtaModel? FromCod(Entities.MaxiContext db, PgcCtaModel.Cods cod)
        {
            return db.PgcCta
            .AsNoTracking()
            .Where(x => x.Cod == (int)cod)
    .Select(x => new PgcCtaModel(x.Guid)
    {
        Id = x.Id,
        Nom = new LangTextModel { Esp = x.Esp, Cat = x.Cat, Eng = x.Eng },
        Act = (PgcCtaModel.Acts)x.Act,
        Cod = (PgcCtaModel.Cods)x.Cod,
        Epigraf = x.PgcClass,
        Plan = x.Plan
    }).FirstOrDefault();

        }
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
                        .AsNoTracking()
                .Select(x => new PgcCtaModel(x.Guid)
                {
                    Id = x.Id,
                    Nom = new LangTextModel { Esp = x.Esp, Cat = x.Cat, Eng = x.Eng },
                    Act = (PgcCtaModel.Acts)x.Act,
                    Cod = (PgcCtaModel.Cods)x.Cod,
                    Epigraf = x.PgcClass,
                    Plan = x.Plan
                })
                .OrderBy(x => x.Plan)
                .ThenBy(x => x.Id)
                .ToList();
        }

        public static List<PgcCtaModel.Extracte> Saldos(PgcCtaModel.Extracte request)
        {
            using (var db = new Entities.MaxiContext())
            {
                IQueryable<Entities.Ccb> query = db.Ccbs
                    .AsNoTracking()
                    .Include(x => x.Cca)
                    .Where(x => x.Cca.Ccd != 96 && x.Cca.Ccd != 98 && x.Cca.Ccd != 99);

                if (request.Contact != null)
                    query = query.Where(x => x.ContactGuid == request.Contact);

                return query
                    .GroupBy(x => new { x.Cca.Emp, x.Cca.Yea, x.CtaGuid, x.ContactGuid })
                    .Select(x => new PgcCtaModel.Extracte
                    {
                        Emp = (EmpModel.EmpIds)x.Key.Emp,
                        Cta = x.Key.CtaGuid,
                        Contact = x.Key.ContactGuid,
                        Year = x.Key.Yea,
                        Deb = x.Where(y => y.Dh == (int)CcaModel.Item.DhEnum.Deb).Sum(y => y.Eur),
                        Hab = x.Where(y => y.Dh == (int)CcaModel.Item.DhEnum.Hab).Sum(y => y.Eur)
                    })
                    .ToList();
            }
        }

    }
}
