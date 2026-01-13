using DTO;

namespace Api.Services
{
    public class SpvService
    {
        public static SpvModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Spvs
                    .Where(x => x.Guid == guid)
                    .Select(x => new SpvModel(guid)
                    {
                        Id = x.Id,
                        FchAvis = x.FchAvis
                    }).FirstOrDefault();
            }
        }


        public static bool Update(SpvModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var guid = value.Guid;
                Entities.Spv? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Spv();
                    db.Spvs.Add(entity);
                    entity.Guid = guid;
                }
                else
                    entity = db.Spvs.Find(guid);

                if (entity == null) throw new Exception("Spv not found");

                entity.Id = value.Id;
                entity.FchAvis = value.FchAvis;

                db.SaveChanges();
                return true;
            }
        }


        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Spvs.Find(guid);
                if (entity != null)
                {
                    db.Spvs.Remove(entity);
                    db.SaveChanges();
                }
            }
            return true;
        }


    }

    public class SpvsService
    {
        public static List<SpvModel> All(UserModel user)
        {
            using(var db = new Entities.MaxiContext())
            {
                return db.Spvs
                    .OrderByDescending(x => x.FchAvis)
                    .Select(x => new SpvModel(x.Guid)
                    {
                        Id = x.Id,
                        FchAvis = x.FchAvis
                    }).ToList();
            }
        }
    }
}
