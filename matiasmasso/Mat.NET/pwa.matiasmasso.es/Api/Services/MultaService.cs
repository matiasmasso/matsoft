using Api.Entities;
using DocumentFormat.OpenXml.Vml.Office;
using DTO;

namespace Api.Services
{
    public class MultaService
    {
        public static MultaModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Multa
                    .Where(x => x.Guid == guid)
                    .Join(db.CliGrals, mlt => mlt.Emisor, cli => cli.Guid, (mlt, cli) => new { mlt, cli })
                    .OrderByDescending(mlt => mlt.mlt.Fch)
                    .Select(x => new MultaModel
                    {
                        Guid = x.mlt.Guid,
                        Fch = x.mlt.Fch,
                        Emisor = x.mlt.Emisor == null ? null : new GuidNom((Guid)x.mlt.Emisor!, x.cli.FullNom),
                        Subjecte = new GuidNom(guid,null),
                        Vto = x.mlt.Vto,
                        Expedient = x.mlt.Expedient,
                        Amt = x.mlt.Eur,
                        Pagat = x.mlt.Pagat
                    })
                    .FirstOrDefault();

                if(retval != null)
                {
                    retval.Docfiles = DocfilesService.FromSrc(db, guid);
                }
                return retval;
            }
        }
    }


    public class MultasService
    {
        public static List<MultaModel> All(Guid target)
        {
            using(var db = new Entities.MaxiContext())
            {
                return db.Multa
                    .Where(x => x.Subjecte == target)
                    .Join(db.CliGrals, mlt => mlt.Emisor, cli => cli.Guid, (mlt, cli) => new { mlt, cli })
                    .OrderByDescending(mlt => mlt.mlt.Fch)
                    .Select(x => new MultaModel
                    {
                        Guid = x.mlt.Guid,
                        Fch = x.mlt.Fch,
                        Emisor = x.mlt.Emisor == null ? null : new GuidNom((Guid)x.mlt.Emisor!, x.cli.FullNom),
                        Vto = x.mlt.Vto,
                        Expedient = x.mlt.Expedient,
                        Amt = x.mlt.Eur,
                        Pagat = x.mlt.Pagat
                    })
                    .ToList();

            }
        }
    }
}

