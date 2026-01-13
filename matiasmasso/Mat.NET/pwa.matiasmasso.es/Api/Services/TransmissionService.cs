using DTO;

namespace Api.Services
{
    public class TransmissionService
    {
    }
    public class TransmissionsService
    {
        public static List<TransmissionModel> GetValues(int emp, int year)
        {
            using(var db = new Entities.MaxiContext())
            {
                return db.Transms
                    .Where(x => x.Emp == emp & x.Yea == year)
                    .Select(x => new TransmissionModel
                    {
                        Id = x.Transm1,
                        Fch = ((DateTimeOffset)x.Fch).DateTime
                    }).ToList();
            }

        }
    }
}
