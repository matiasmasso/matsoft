using DTO;
using System.Linq;
using System.Linq.Expressions;

namespace Api.Services
{
    public class IncidenciaService
    {
        public static IncidenciaModel Get(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = (from x in db.Incidencies
                              where x.Guid.Equals(guid)
                              select new IncidenciaModel(x.Guid)
                              {
                                  Id = x.Id,
                                  Fch = x.Fch
                              }).FirstOrDefault();
                return retval;
            }
        }

        public async static Task<bool> Update(IncidenciaModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Incidency? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Incidency();
                    await db.Incidencies.AddAsync(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.Incidencies.Find(value.Guid);

                if (entity == null) throw new Exception("Incidencia not found");

                entity.Id = value.Id;
                entity.Fch = value.Fch;

                // Save changes in database
                db.SaveChanges();
                return true;
            }
        }

        public  static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Incidencies.Remove(db.Incidencies.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


    }
    public class IncidenciasService
    {
        public static List<IncidenciaModel> All(UserModel user, LangDTO lang, int empId, int? year = null)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new List<IncidenciaModel>();
                switch ((UserModel.Rols)user.Rol!)
                {
                    case UserModel.Rols.cliFull:
                    case UserModel.Rols.cliLite:
                        retval = db.Incidencies
                            .Join(db.EmailClis, inc=>inc.ContactGuid, ec=> ec.ContactGuid, (inc, ec)=> new { inc, ec })
                            .Where(x => x.ec.EmailGuid == user.Guid )
                            .OrderByDescending(x => x.inc.Id)
                            .Select(x => new IncidenciaModel
                            {
                                Guid = x.inc.Guid,
                                Id = x.inc.Id,
                                Fch = x.inc.Fch,
                                Customer = x.inc.ContactGuid,
                                Product = x.inc.ProductGuid,
                                CodiTancament = x.inc.CodiTancament == null ? null : new GuidNom(x.inc.CodiTancamentNavigation!.Guid, lang.Tradueix(x.inc.CodiTancamentNavigation.Esp, x.inc.CodiTancamentNavigation.Cat, x.inc.CodiTancamentNavigation.Eng, x.inc.CodiTancamentNavigation.Por))
                            })
                            .ToList();
                        break;
                    default:
                        retval = db.Incidencies
                            .Where(x => x.Emp == empId && x.CodiTancament == null)
                            .OrderByDescending(x => x.Id)
                            .Select(x => new IncidenciaModel
                            {
                                Guid = x.Guid,
                                Id = x.Id,
                                Fch = x.Fch,
                                Customer = x.ContactGuid,
                                Product = x.ProductGuid,
                                CodiTancament = x.CodiTancament == null ? null : new GuidNom(x.CodiTancamentNavigation!.Guid, lang.Tradueix(x.CodiTancamentNavigation.Esp, x.CodiTancamentNavigation.Cat, x.CodiTancamentNavigation.Eng, x.CodiTancamentNavigation.Por))
                            })
                            .ToList();
                        break;
                }
                return retval;
            }
        }
    }
}
