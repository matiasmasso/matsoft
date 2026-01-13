using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class BankService
    {
        public static Media? Logo(Guid guid)
        {
            //TO DO: create Mime cod field
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Bn1s
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new Media( Media.MimeCods.Jpg, x.Logo48))
                    .FirstOrDefault();

                return retval;
            }
        }

        public static bool Update(BankModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Bn1? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Bn1();
                    db.Bn1s.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.Bn1s.Find(value.Guid);

                if (entity == null) throw new Exception("Bank not found");

                entity.Bn11 = value.Id;
                entity.Nom = value.RaoSocial;
                entity.Abr = value.NomComercial;
                entity.Swift = value.Swift;
                entity.Country = (Guid)value.Country!;
                entity.Svg = value.Svg;
                entity.Obsoleto = value.Obsoleto;

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Bn1s.Remove(db.Bn1s.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;
        }
    }
    public class BanksService
    {
        public static List<BankModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Bn1s
                    .AsNoTracking()
                    .Select(x => new BankModel(x.Guid)
                    {
                        Id = x.Bn11,
                       NomComercial=x.Abr,
                       RaoSocial = x.Nom,
                       Swift = x.Swift,
                       Country = x.Country,
                       Svg = x.Svg,
                       Obsoleto = x.Obsoleto
                    })
                    .ToList();

                return retval;
            }
        }
    }
}
