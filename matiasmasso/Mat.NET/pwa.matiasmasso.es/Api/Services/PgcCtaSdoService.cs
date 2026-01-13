using DTO;
namespace Api.Services
{
    public class PgcCtaSdosService
    {
        public static PgcCtaSdosDTO Fetch(Guid? contact, LangDTO lang)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new PgcCtaSdosDTO();

                retval.Contact = db.CliGrals
                    .Where(x => x.Guid == contact)
                    .Select(x => new GuidNom(x.Guid, x.FullNom))
                    .FirstOrDefault();

                retval.Items = db.VwPgcCtaSdos
                    .Where(x=>x.ContactGuid == contact)
                    .OrderByDescending(x=> x.Yea)
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
                        Hab = x.Hab ?? 0,
                    }).ToList();

                return retval;
            }
        }
    }
}
