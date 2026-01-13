using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class BankBranchService
    {
        public static bool Update(BankModel.Branch value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Bn2? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Bn2();
                    db.Bn2s.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.Bn2s.Find(value.Guid);

                if (entity == null) throw new Exception("Bank branch not found");

                entity.Bank = (Guid)value.Bank!;
                entity.Agc = value.Id;
                entity.Adr = value.Adr;
                entity.Location = (Guid)value.Location!;
                entity.Swift = value.Swift;
                entity.Obsoleto = value.Obsoleto;

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Bn2s.Remove(db.Bn2s.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;
        }
    }
    public class BankBranchesService
    {
        public static List<BankModel.Branch> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Bn2s
                    .AsNoTracking()
                    .Select(x => new BankModel.Branch(x.Guid)
                    {
                        Id = x.Agc,
                        Bank = x.Bank,
                        Adr = x.Adr,
                        Location = x.Location,
                        Swift = x.Swift,
                        Obsoleto = x.Obsoleto
                    })
                    .ToList();

                return retval;
            }
        }
    }
}
