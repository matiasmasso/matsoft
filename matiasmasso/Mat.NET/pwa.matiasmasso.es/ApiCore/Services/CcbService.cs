using DTO;
namespace Api.Services
{
    public class CcbService
    {
        public static bool Update(CcaModel.Item value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Ccb? entity = db.Ccbs.Find(value.Guid);

                if (entity == null) throw new Exception("Ccb not found");

                entity.CtaGuid = (Guid)value.Cta!;

                db.SaveChanges();
                return true;
            }
        }
    }
}
