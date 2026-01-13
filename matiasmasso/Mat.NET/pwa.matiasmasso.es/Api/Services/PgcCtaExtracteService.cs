using DTO;

namespace Api.Services
{
    public class PgcCtaExtracteService
    {
        public static PgcCtaExtracteDTO Extracte(PgcCtaExtracteDTO request)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = request;

                var cta = db.PgcCta.Find(request.Cta.Guid);
                if (cta != null)
                {
                    request.Cta.Nom = String.Format("{0} {1}", cta.Id, request.Lang.Tradueix(cta.Esp, cta.Cat, cta.Eng));
                    request.CtaAct = cta.Act;
                }

                if (request.Contact != null)
                {
                    var contact = db.CliGrals.Find(request.Contact.Guid);
                    request.Contact.Nom = contact?.FullNom ?? "?";
                }

                var query = db.VwCcas.AsQueryable();
                query = query
                    .Where(x => x.Emp == request.Emp && ((DateTime)x.Fch!).Year == request.Year && x.CtaGuid == request.Cta.Guid);
                if (request.Contact?.Guid != null)
                    query = query.Where(x => x.ContactGuid == request.Contact.Guid)
                        .OrderBy(x => x.Fch)
                        .ThenBy(x => x.Ccd)
                        .ThenBy(x=> x.Cdn);

                var entities = ((IQueryable<Entities.VwCca>)query).ToList();

                retval.Items = entities
                    .Select(x => new PgcCtaExtracteDTO.Item
                    {
                        CcbGuid = x.CcbGuid,
                        CcaGuid = x.CcaGuid,
                        CcaId = x.CcaId,
                        Concept = x.Txt,
                        DH = x.Dh,
                        Eur = x.Eur,
                        Fch = (DateTime)x.Fch!,
                        HasDoc = x.Hash != null
                    }).ToList();

                decimal saldo = 0;
                int ord = 1;
                foreach(var item in retval.Items)
                {
                    saldo += cta.Act == item.DH ? item.Eur : -item.Eur;
                    item.Saldo = saldo;
                    item.Ord = ord;
                    ord += 1;
                }
                return (PgcCtaExtracteDTO)retval;

            }
        }
    }
}
