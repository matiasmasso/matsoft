using DTO;
namespace Api.Services
{
    public class BancService
    {
        public static Byte[]? Logo(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.VwIbans
                    .Where(x => x.IbanContactGuid == guid)
                    .Join(db.Bn1s, iban => iban.BankGuid, bank => bank.Guid, (iban, bank) => new { Logo = bank.Logo48 })
                    .Select(x => x.Logo)
                    .FirstOrDefault();

                return retval;
            }
        }

    }
    public class BancsService
    {
        public static List<BancDTO.Saldo> Saldos(UserModel user)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.VwBancsSdos
                    .Select(x => new BancDTO.Saldo
                    {
                        Emp = (int)x.Emp,
                        Guid = x.Banc,
                        Abr = x.Abr,
                        Ccc = x.IbanCcc,
                        Eur = x.Sdo,
                        Fch = x.Fch
                    }).ToList();
                retval = retval.Where(x => user.Emps.Any(y => x.Emp == (int)y)).ToList();

                return retval;
            }
        }

    }
}
