using DocumentFormat.OpenXml.Office2013.PowerPoint;
using DTO;
using DTO.Integracions.Banca;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Api.Services.Integracions.Banca
{
    public class Norma43Service
    {
        public static void Update(UserModel user, Norma43 value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var ccbFchs = MissingValues(value);
                var apunts = value.Apunts;
                var cccFchs = ccbFchs
                    .Select(x => new Entities.N43hdr
                    {
                        Guid = x.Guid,
                        Ccc = x.Ccc!,
                        Fch = (DateOnly)x.Fch!,
                        UsrCreated = user.Guid,
                        FchCreated = DateTime.Now,
                        N43ccas = apunts.Where(y => y.FullCcc == x.Ccc && y.Fch == x.Fch)
                         .Select(z => new Entities.N43cca
                         {
                             Guid = z.Guid,
                             //Parent = x.Guid,
                             N43itms = z.Registros()
                             .Select(j => new Entities.N43itm
                             {
                                 //Parent =    z.Guid,
                                 Cod = (int)j.Codi,
                                 Idx =(int)j.Codi<23 ? 0 : (int)j.Src.Truncate(2, 2).ToInteger()!,
                                 Body = j.Src
                             }).ToList()
                         }).ToList()
                    }).ToList();
                db.N43hdrs.AddRange(cccFchs);
                db.SaveChanges();
            }
        }

        public static void Assign(Guid n43Guid, Guid ccaGuid)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.N43cca? entity= db.N43ccas.Find(n43Guid);
                if (entity == null) throw new Exception("No s'ha trobat l'apunt");

                entity.Cca = ccaGuid;
                db.SaveChanges();
            }
        }


        public static List<Norma43.CccFch> MissingValues(Norma43 value)
        {
            List<Norma43.CccFch> uploadedValues = value.CccFchs();
            List<Norma43.CccFch> persistedValues = new();
            List<Norma43.CccFch> duplicatedValues = new();
            using (var db = new Entities.MaxiContext())
            {
                persistedValues = db.N43hdrs
                        .AsNoTracking()
                        .Select(x => new Norma43.CccFch
                        {
                            Ccc = x.Ccc,
                            Fch = x.Fch
                        }).ToList();

            }
            List<Norma43.CccFch> retval = uploadedValues.Except(persistedValues).ToList();
            return retval;
        }
        public static List<Norma43.Apunt> PendingApunts()
        {
            List<Norma43.Apunt> retval = new();
            using (var db = new Entities.MaxiContext())
            {
                retval = db.N43ccas
                        .AsNoTracking()
                        .Where(x => x.Cca == null)
                        .OrderBy(x=>x.ParentNavigation.Fch)
                        .Select(x => new Norma43.Apunt( x.ParentNavigation.Ccc,x.Guid, x.N43itms.Select(y => y.Body).ToList()))
                        .ToList();
            }
            return retval;
        }
    }
}
