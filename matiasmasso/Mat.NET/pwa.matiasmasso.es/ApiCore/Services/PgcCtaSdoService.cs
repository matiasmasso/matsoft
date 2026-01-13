using Api.Entities;
using DocumentFormat.OpenXml.InkML;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class PgcCtaSdosService
    {
        public static List<PgcCtaSdoModel> GetValues(int emp, int year, int? mes = null)
        {              

            using (var db = new Entities.MaxiContext())
            {
                IQueryable<Ccb> query;

                if(mes == null)
                {
                    query = db.Ccbs
                        .AsNoTracking()
                        .Include(x => x.Cca)
                    .Where(x => x.Cca.Emp == emp
                        && x.Cca.Yea == year
                        && x.Cca.Ccd != (int)CcaModel.CcdEnum.TancamentComptes
                        && x.Cca.Ccd != (int)CcaModel.CcdEnum.TancamentExplotacio
                        && x.Cca.Ccd != (int)CcaModel.CcdEnum.TancamentBalanç
                        );
                } else
                {
                        DateOnly finalDeMes = new DateTime(year, (int)mes!, 1).AddMonths(1).AddDays(-1).ToDateOnly();
                    query = db.Ccbs
                        .AsNoTracking()
                        .Include(x => x.Cca)
                    .Where(x => x.Cca.Emp == emp
                        && x.Cca.Yea == year
                        && (((DateOnly)x.Cca.Fch!).CompareTo(finalDeMes) <= 0)
                        && x.Cca.Ccd != (int)CcaModel.CcdEnum.TancamentComptes
                        && x.Cca.Ccd != (int)CcaModel.CcdEnum.TancamentExplotacio
                        && x.Cca.Ccd != (int)CcaModel.CcdEnum.TancamentBalanç
                        );
                }
                var retval = query
                    .GroupBy(x => new { x.CtaGuid, x.ContactGuid })
                    .Select(x => new PgcCtaSdoModel
                    {
                        Cta = x.Key.CtaGuid,
                        Contact = x.Key.ContactGuid,
                        Deb = x.Sum(y => y.Dh == 1 ? y.Eur : 0),
                        Hab = x.Sum(y => y.Dh == 2 ? y.Eur : 0)
                    }).ToList();
                return retval;
            }
        }
        public static PgcCtaSdosDTO Fetch(Guid? contact, LangDTO lang)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new PgcCtaSdosDTO();

                retval.Contact = db.CliGrals
                        .AsNoTracking()
                    .Where(x => x.Guid == contact)
                    .Select(x => new GuidNom(x.Guid, x.FullNom))
                    .FirstOrDefault();

                retval.Items = db.VwPgcCtaSdos
                        .AsNoTracking()
                    .Where(x => x.ContactGuid == contact)
                    .OrderByDescending(x => x.Yea)
                    .ThenBy(x => x.CtaId)
                    .Select(x => new PgcCtaSdosDTO.Item
                    {
                        Year = (int)x.Yea!,
                        CtaGuid = x.CtaGuid,
                        CtaId = x.CtaId,
                        CtaNom = lang.Tradueix(x.Esp, x.Cat, x.Eng, null),
                        CtaCod = x.CtaCod,
                        CtaAct = x.CtaAct,
                        Deb = x.Deb ?? 0,
                        Hab = x.Hab ?? 0
                    }).ToList();

                return retval;
            }
        }
    }
}
