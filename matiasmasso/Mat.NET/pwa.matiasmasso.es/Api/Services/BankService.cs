using DTO;
namespace Api.Services
{
    public class BankService
    {
        public static Byte[]? Logo(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Bn1s
                    .Where(x => x.Guid == guid)
                    .Select(x => x.Logo48)
                    .FirstOrDefault();

                return retval;
            }
        }
    }
}
