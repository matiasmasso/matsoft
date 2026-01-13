using Api.Entities;
using DTO;

namespace Api.Services
{
    public class FacturaProveidorService
    {

        public static bool Update(FacturaProveidorModel value, UserModel user)
        {
            using (var db = new Entities.MaxiContext())
            {
                Update(db, value, user);
                db.SaveChanges();
                return true;
            }
        }

        public static void Update(MaxiContext db, FacturaProveidorModel value, UserModel user)
        {
            var ctas = PgcCtasService.GetValues(db)
                .Where(x => x.Plan == PgcPlanModel.Default().Guid).ToList();

            var cca = value.BuildCca(user, ctas);
            CcaService.Update(db, cca);

            var bookfra = BookfraModel.FactoryFromFacturaProveidor(value, cca);
            BookfraService.Update(db, bookfra);

            var pnd = value.BuildPnd(cca);
            PndService.Update(db, pnd);
        }
    }
}
