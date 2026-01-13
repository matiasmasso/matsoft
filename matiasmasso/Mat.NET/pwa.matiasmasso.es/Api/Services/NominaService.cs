using DTO;
namespace Api.Services
{
    public class NominaService
    {
    }

    public class NominasService
    {
        public static StaffDTO? All(UserModel user)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new StaffDTO();
                if (user.DefaultContact == null) throw new Exception("User with no default contact");
                retval.Properties = StaffService.Find(db,(Guid)user.DefaultContact);
                retval.Nominas = db.Nominas
                    .Where(x => x.Staff == user.DefaultContact)
                    .OrderByDescending(z => z.Fch)
                    .Select(y => new NominaModel
                    {
                        Guid = y.CcaGuid,
                        Fch = y.Fch,
                        Devengat = y.Devengat,
                        Liquid = y.Liquid
                    }).ToList();
                return retval;
            }
        }
        public static StaffDTO? All(Guid staff)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new StaffDTO();
                retval.Properties = StaffService.Find(staff);
                retval.Nominas = db.Nominas
                    .Where(x => x.Staff == staff)
                    .OrderByDescending(z => z.Fch)
                    .Select(y => new NominaModel
                    {
                        Guid = y.CcaGuid,
                        Fch = y.Fch,
                        Devengat = y.Devengat,
                        Liquid = y.Liquid
                    }).ToList();
                return retval;
            }
        }
    }
}
